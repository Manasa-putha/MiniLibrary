using library.Data;
using library.Models;
using library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        public BorrowController(Context context, EmailService emailService, JwtService jwtService)
        {
            Context = context;
            EmailService = emailService;
            JwtService = jwtService;
        }

        public Context Context { get; }
        public EmailService EmailService { get; }
        public JwtService JwtService { get; }

        [HttpPost("OrderBook")]
        public ActionResult OrderBook(int userId, int bookId)
        {
            var user = Context.Users.Find(userId);
            var book = Context.Books.Find(bookId);
            var canOrder = Context.Orders.Count(o => o.UserId == userId && !o.Returned) < 10;

            if (user == null || book == null)
            {
                return BadRequest("Invalid user or book ID");
            }
            if (user.TokensAvailable < 1)
            {
                return BadRequest("Not enough tokens to borrow the book");
            }
            if (canOrder)
            {
                Context.Orders.Add(new()
                {
                    UserId = userId,
                    BookId = bookId,
                    OrderDate = DateTime.Now,
                    ReturnDate = null,
                    Returned = false,
                    FinePaid = 0
                });

                //  var book = Context.Books.Find(bookId);
                if (book != null)
                {
                    book.Ordered = true;
                    user.TokensAvailable -= 1;  // Deduct one token from the user who is borrowing the book
                    var lentUser = Context.Users.Find(book.userId); // Assuming the book has a property indicating the owner (lent user)
                    if (lentUser != null)
                    { 
                        lentUser.TokensAvailable += 1;  // Add one token to the user who lent the book
                    }
                }
                Context.SaveChanges();
                return Ok("ordered");
            }

            return Ok("cannot order");
        }


        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            var user = Context.Users.Find(userId);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return Ok(user); // Return the user data as JSON
        }

        //   [Authorize]
        [HttpGet("GetOrdersOFUser")]
        public ActionResult GetOrdersOFUser(int userId)
        {
            var orders = Context.Orders
                .Include(o => o.Book)
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .ToList();
            if (orders.Any())
            {
                return Ok(orders);
            }
            else
            {
                return NotFound();
            }
        }
        //[Authorize]
        [HttpGet("GetOrders")]
        public ActionResult GetOrders()
        {
            var orders = Context.Orders.Include(o => o.User).Include(o => o.Book).ToList();
            if (orders.Any())
            {
                return Ok(orders);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("SendEmailForPendingReturns")]
        public ActionResult SendEmailForPendingReturns()
        {
            var orders = Context.Orders
                            .Include(o => o.Book)
                            .Include(o => o.User)
                            .Where(o => !o.Returned)
                            .ToList();

            var emailsWithFine = orders.Where(o => DateTime.Now > o.OrderDate.AddDays(10)).ToList();
            emailsWithFine.ForEach(x => x.FinePaid = (DateTime.Now - x.OrderDate.AddDays(10)).Days * 50);

            var firstFineEmails = emailsWithFine.Where(x => x.FinePaid == 50).ToList();
            firstFineEmails.ForEach(x =>
            {
            });

            var regularFineEmails = emailsWithFine.Where(x => x.FinePaid > 50 && x.FinePaid <= 500).ToList();
            regularFineEmails.ForEach(x =>
            {

            });

            var overdueFineEmails = emailsWithFine.Where(x => x.FinePaid > 500).ToList();
            overdueFineEmails.ForEach(x =>
            {

            });

            return Ok("sent");
        }

        [Authorize]
        [HttpGet("BlockFineOverdueUsers")]
        public ActionResult BlockFineOverdueUsers()
        {
            var orders = Context.Orders
                            .Include(o => o.Book)
                            .Include(o => o.User)
                            .Where(o => !o.Returned)
                            .ToList();

            var emailsWithFine = orders.Where(o => DateTime.Now > o.OrderDate.AddDays(10)).ToList();
            emailsWithFine.ForEach(x => x.FinePaid = (DateTime.Now - x.OrderDate.AddDays(10)).Days * 50);

            var users = emailsWithFine.Where(x => x.FinePaid > 500).Select(x => x.User!).Distinct().ToList();

            if (users is not null && users.Any())
            {
                foreach (var user in users)
                {
                    user.AccountStatus = AccountStatus.BLOCKED;
                }
                Context.SaveChanges();

                return Ok("blocked");
            }
            else
            {
                return Ok("not blocked");
            }
        }

      //  [Authorize]
        [HttpGet("Unblock")]
        public ActionResult Unblock(int userId)
        {
            var user = Context.Users.Find(userId);
            if (user is not null)
            {
                user.AccountStatus = AccountStatus.ACTIVE;
                Context.SaveChanges();
                return Ok("unblocked");
            }

            return Ok("not unblocked");
        }
    }
}


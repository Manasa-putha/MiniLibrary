import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject, map, Observable } from 'rxjs';
import { Book, BookCategory, Order, User, UserType } from '../../models/models';
import { UserStateService } from './user-state.service';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  apiUrl1: string = 'https://localhost:7038/api/Auth/';
  apiUrl2: string = 'https://localhost:7038/api/Book/';
  apiUrl3: string = 'https://localhost:7038/api/Borrow/';
  userStatus: Subject<string> = new Subject();
  private userInfo: User | null = null;
  constructor(private http: HttpClient, private jwt: JwtHelperService, private userStateService: UserStateService) {}

  register(user: any) {
    return this.http.post(this.apiUrl1 + 'Register', user, {
      responseType: 'text',
    });
  }

  login(info: any) {
    let params = new HttpParams()
      .append('email', info.email)
      .append('password', info.password);

    return this.http.get(this.apiUrl1 + 'Login', {
      params: params,
      responseType: 'text',
    });
  }

  isLoggedIn(): boolean {
    if (
      localStorage.getItem('access_token') != null &&
      !this.jwt.isTokenExpired()
    )
      return true;
    return false;
  }

  getUserInfo(): User | null {
    if (!this.isLoggedIn()) return null;
    var decodedToken = this.jwt.decodeToken();
    var user: User = {
      id: decodedToken.id,
      name: decodedToken.name,
      email: decodedToken.email,
      mobileNumber: decodedToken.mobileNumber,
      userType: UserType[decodedToken.userType as keyof typeof UserType],
      accountStatus: decodedToken.accountStatus,
      createdOn: decodedToken.createdOn,
      password: '',
      tokensAvailable: decodedToken.tokensAvailable,
    };
    return user;
  }

  setUserInfo(user: User) {
    this.userInfo = user;
    this.userStateService.updateTokens(user.tokensAvailable);
  }

  logOut() {
    localStorage.removeItem('access_token');
    this.userInfo = null;
    this.userStatus.next('loggedOff');
    this.userStateService.updateTokens(0);
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('access_token');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }
  getBooks() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('access_token')}`
    });
    return this.http.get<Book[]>(this.apiUrl2 + 'GetBooks', { headers });
  }

  borrowBook(book: Book) {
    let userId = this.getUserInfo()!.id;
    let params = new HttpParams()
      .append('userId', userId)
      .append('bookId', book.id);

    return this.http.post(this.apiUrl3 + 'OrderBook', null, {
      params: params,
      responseType: 'text', 
      
    });
  }

  getOrdersOfUser(userId: number) {
    let params = new HttpParams().append('userId', userId);
    return this.http
      .get<any>(this.apiUrl3 + 'GetOrdersOfUser', {
        params: params,
      })
      .pipe(
        map((orders) => {
          let newOrders = orders.map((order: any) => {
            let newOrder: Order = {
              id: order.id,
              userId: order.userId,
              userName: order.user.firstName + ' ' + order.user.lastName,
              bookId: order.bookId,
              bookName: order.book.bookName,
              borrowDate: order.borrowDate,
              returned: order.returned,
              returnDate: order.returnDate,
              finePaid: order.finePaid,
            };
            return newOrder;
          });
          return newOrders;
        })
      );
  }

  getFine(order: Order) {
    let today = new Date();
    let orderDate = new Date(Date.parse(order.borrowDate));
    orderDate.setDate(orderDate.getDate() + 10);
    if (orderDate.getTime() < today.getTime()) {
      var diff = today.getTime() - orderDate.getTime();
      let days = Math.floor(diff / (1000 * 86400));
      return days * 50;
    }
    return 0;
  }

  addNewCategory(category: BookCategory) {
    return this.http.post(this.apiUrl2 + 'AddCategory', category, {
      responseType: 'text',
    });
  }

  getCategories() {
    return this.http.get<BookCategory[]>(this.apiUrl2 + 'GetCategories');
  }

  addBook(book: Book) {
    return this.http.post(this.apiUrl2 + 'AddBook', book, {
      responseType: 'text',
    });
  }

  deleteBook(id: number) {
    return this.http.delete(this.apiUrl2 + 'DeleteBook', {
      params: new HttpParams().append('id', id),
      responseType: 'text',
    });
  }

  returnBook(userId: string, bookId: string, fine: number) {
    return this.http.get(this.apiUrl2 + 'ReturnBook', {
      params: new HttpParams()
        .append('userId', userId)
        .append('bookId', bookId)
        .append('fine', fine),
      responseType: 'text',
    });
  }

  getUsers() {
    return this.http.get<User[]>(this.apiUrl1 + 'GetUsers');
  }

  approveRequest(userId: number) {
    return this.http.get(this.apiUrl1 + 'ApproveRequest', {
      params: new HttpParams().append('userId', userId),
      responseType: 'text',
    });
  }

  getOrders() {
    return this.http.get<any>(this.apiUrl3 + 'GetOrders').pipe(
      map((orders) => {
        let newOrders = orders.map((order: any) => {
          let newOrder: Order = {
            id: order.id,
            userId: order.userId,
            userName: order.user.firstName + ' ' + order.user.lastName,
            bookId: order.bookId,
            bookName: order.book.bookName,
            borrowDate: order.borrowDate,
            returned: order.returned,
            returnDate: order.returnDate,
            finePaid: order.finePaid,
          };
          return newOrder;
        });
        return newOrders;
      })
    );
  }

  sendEmail() {
    return this.http.get(this.apiUrl3 + 'SendEmailForPendingReturns', {
      responseType: 'text',
    });
  }

  blockUsers() {
    return this.http.get(this.apiUrl3 + 'BlockFineOverdueUsers', {
      responseType: 'text',
    });
  }

  unblock(userId: number) {
    return this.http.get(this.apiUrl3 + "Unblock", {
      params: new HttpParams().append("userId", userId),
      responseType: "text",
    });
  }
  getUserById(): Observable<User> {
    let userId = this.getUserInfo()!.id;
    return this.http.get<User>(`${this.apiUrl3}GetUserById`, {
      params: new HttpParams().append('userId', userId.toString())
    }).pipe(
      map(user => {
        this.setUserInfo(user);
        return user;
      })
    );
  }
}

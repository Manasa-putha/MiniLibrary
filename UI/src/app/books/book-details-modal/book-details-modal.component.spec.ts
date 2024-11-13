import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Book } from '../../models/models';
import { ApiService } from '../../shared/services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserOrdersComponent } from '../../users/user-orders/user-orders.component';

@Component({
  selector: 'book-details-modal',
  templateUrl: './book-details-modal.component.html',
  styleUrls: ['./book-details-modal.component.scss']
})
export class BookDetailsModalComponent {
  constructor(
    public dialogRef: MatDialogRef<BookDetailsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Book,
    private apiService: ApiService,
    private snackBar: MatSnackBar
  ) {}

  orderBook(book: Book) {
    this.apiService.borrowBook(book).subscribe({
      next: (res) => {
        if (res === 'ordered') {
          book.ordered = true;
          console.log(book.ordered);
          // Update tokensAvailable in real-time after borrowing a book
        const user = this.apiService.getUserInfo();
        console.log(this.apiService.getUserInfo());
        if (user) {
          user.tokensAvailable --;
          //this.apiService.tokensAvailable$.next(user.tokensAvailable);
          console.log(user.tokensAvailable);
          
          let today = new Date();
          let returnDate = new Date();
          returnDate.setDate(today.getDate() + 10);

          this.snackBar.open(
            book.bookName +
              ' has been ordered! You will have to return on ' +
              returnDate.toDateString(),
            'OK'
          );
          this.dialogRef.close();
        }
        }

 else {
          this.snackBar.open(
            'You already have 3 orders pending to return.',
            'OK'
          );
        }
      },
      error: (err) => {
        console.error('Error ordering book:', err);
        this.snackBar.open('Failed to order the book. Please try again.', 'OK');
      }
    });
  }
}

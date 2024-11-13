import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Book } from '../../models/models';
import { ApiService } from '../../shared/services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserStateService } from '../../shared/services/user-state.service';
import { Router } from '@angular/router';

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
    private snackBar: MatSnackBar,
    private router: Router,
    private userStateService: UserStateService 
  ) {}
  
  orderBook(book: Book) {
    this.apiService.borrowBook(book).subscribe({
      next: (res: string) => {
        if (res === 'ordered') { 
          book.ordered = true;  
          let today = new Date();
          let returnDate = new Date();
          returnDate.setDate(today.getDate() + 10);
  
          this.snackBar.open(
            book.bookName +
              ' has been ordered! You will have to return on ' +
              returnDate.toDateString(),
            'OK'
          );
          this.apiService.getUserById().subscribe();
          this.dialogRef.close();
          this.router.navigate(['/home']);
        } else {
          this.snackBar.open(
            'You already have 3 orders pending to return.',
            'OK'
          );
          
          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        console.error('Error ordering book:', err);
        this.snackBar.open('Failed to order the book. Please try again.', 'OK');
      }
    });
  }
  
}

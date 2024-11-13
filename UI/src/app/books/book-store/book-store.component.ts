import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Book, BookCategory, BooksByCategory } from '../../models/models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiService } from '../../shared/services/api.service';
import { BookDetailsModalComponent } from '../book-details-modal/book-details-modal.component';


@Component({
  selector: 'book-store',
  templateUrl: './book-store.component.html',
  styleUrls: ['./book-store.component.scss'],
})
export class BookStoreComponent {
  displayedColumns: string[] = ['bookName', 'author','bookCategory.subCategory'];
  books: Book[] = [];
  bookCategory: BookCategory[] = []; 
  //'bookCategory.subCategory'
  booksToDisplay: BooksByCategory[] = [
    {
      bookCategoryId: 1,
      category: 'C',
      subCategory: 'S',
      books: [
        {
          id: 1,
          bookName: 'T',
          author: 'A',
          price: 100,
          ordered: false,
          bookCategoryId: 1,
          bookCategory: { id: 1, category: '', subCategory: '' },
          userId:1,
          description:'D',
          rating:5,
          
        },
      ],
    },
  ];

  constructor(private apiService: ApiService, private snackBar: MatSnackBar, public dialog: MatDialog) {
    apiService.getBooks().subscribe({
      next: (res: Book[]) => {
        this.books = [];
        res.forEach((b) => this.books.push(b));

        this.updateList();
      },
    });
  }

  updateList() {
    this.booksToDisplay = [];
    console.log(this.books);
    for (let book of this.books) {
      let categoryExists = false;
      let categoryBook: BooksByCategory | null;
      for (let bookToDisplay of this.booksToDisplay) {
        if (bookToDisplay.bookCategoryId == book.bookCategoryId) {
          categoryExists = true;
          categoryBook = bookToDisplay;
        }
      }

      if (categoryExists) {
        categoryBook!.books.push(book);
      } else {
        this.booksToDisplay.push({
          bookCategoryId: book.bookCategoryId,
          category: book.bookCategory.category,
          subCategory: book.bookCategory.subCategory,
          books: [book],
        });
      }
    }
  }

  searchBooks(value: string) {
    this.updateList();
    value = value.toLowerCase();
    this.booksToDisplay = this.booksToDisplay.filter((bookToDisplay) => {
      bookToDisplay.books = bookToDisplay.books.filter((book) => {
        const bookNameMatch = book.bookName.toLowerCase().includes(value);
        const authorMatch = book.author.toLowerCase().includes(value);
        const genreMatch = book.bookCategory.subCategory.toLowerCase().includes(value);
        return bookNameMatch || authorMatch || genreMatch;
      });
      return bookToDisplay.books.length > 0;
    });
  }

  getBookCount() {
    let count = 0;
    this.booksToDisplay.forEach((b) => (count += b.books.length));
    return count;
  }
  openBookDetailsModal(book: Book) {
    const dialogRef = this.dialog.open(BookDetailsModalComponent, {
      width: '650px',
      data: book,
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}

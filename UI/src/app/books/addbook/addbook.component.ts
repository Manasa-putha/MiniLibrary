import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Book, BookCategory } from '../../models/models';
import { ApiService } from '../../shared/services/api.service';
import { Router } from '@angular/router';
export interface CategoryOption {
  displayValue: string;
  value: number;
}
@Component({
  selector: 'addbook',
  templateUrl: './addbook.component.html',
  styleUrl: './addbook.component.scss'
})
export class AddbookComponent {
  newCategory: FormGroup;
  newBook: FormGroup;
  deleteBook: FormControl;
  categoryOptions: CategoryOption[] = [];

  constructor(
    fb: FormBuilder,
    private apiService: ApiService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.newCategory = fb.group({
      category: fb.control('', [Validators.required]),
      subCategory: fb.control('', [Validators.required]),
    });

    this.newBook = fb.group({
      bookName: fb.control('', [Validators.required]),
      author: fb.control('', [Validators.required]),
      price: fb.control(0, [Validators.required]),
      category: fb.control(-1, [Validators.required]),
      description: fb.control('',[Validators.required]),
    });

    apiService.getCategories().subscribe({
      next: (res: BookCategory[]) => {
        res.forEach((c) => {
          this.categoryOptions.push({
            value: c.id,
            displayValue: `${c.category} / ${c.subCategory}`,
          });
        });
      },
    });

    this.deleteBook = fb.control('', [Validators.required]);
  }


  addNewBook() {
    let book: Book = {
      id: 0,
      userId:this.apiService.getUserInfo()?.id ?? 0,
      bookName: this.newBook.get('bookName')?.value,
      author: this.newBook.get('author')?.value,
      bookCategoryId: this.newBook.get('category')?.value,
      price: this.newBook.get('price')?.value,
      bookCategory: { id: 0, category: '', subCategory: '' },
      ordered: false,
      description:this.newBook.get('description')?.value,
      rating:this.newBook.get('rating')?.value
      //userId:this.newBook.get('userId')?.value,
    
    };
    console.log(this.addNewBook);
    this.apiService.addBook(book).subscribe({
      next: (res) => {
        if (res === 'inserted') this.snackBar.open('Book Added', 'OK');
        this.router.navigate(['/home']);
      },
    });
  }

}

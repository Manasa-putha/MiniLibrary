import { NgModule } from '@angular/core';
import { BookStoreComponent } from './book-store/book-store.component';
import { SharedModule } from '../shared/shared.module';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { ReturnBookComponent } from './return-book/return-book.component';
import { MatDialogModule } from '@angular/material/dialog';
import { BookDetailsModalComponent } from './book-details-modal/book-details-modal.component';
import { MatIconModule } from '@angular/material/icon';
import { AddbookComponent } from './addbook/addbook.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [BookStoreComponent, MaintenanceComponent, ReturnBookComponent,  BookDetailsModalComponent,AddbookComponent],
  imports: [SharedModule, MatDialogModule, MatIconModule ,RouterModule],
})
export class BooksModule {}

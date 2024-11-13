import { Component } from '@angular/core';
import { Order } from '../../models/models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiService } from '../../shared/services/api.service';

@Component({
  selector: 'all-orders',
  templateUrl: './all-orders.component.html',
  styleUrl: './all-orders.component.scss',
})
export class AllOrdersComponent {
  columnsForPendingReturns: string[] = [
    'orderId',
    'userIdForOrder',
    'bookId',
     'bookName',
  ];

  columnsForCompletedReturns: string[] = [
    'orderId',
    'userIdForOrder',
    'bookId',
    'returnedDate',
  ];

  showProgressBar: boolean = false;
  ordersWithPendingReturns: Order[] = [];
  ordersWithCompletedReturns: Order[] = [];

  constructor(private apiService: ApiService, private snackBar: MatSnackBar) {
    apiService.getOrders().subscribe({
      next: (res: Order[]) => {
        this.ordersWithPendingReturns = res.filter((o) => !o.returned);
        this.ordersWithCompletedReturns = res.filter((o) => o.returned);
      },
      error: (err) => {
        this.snackBar.open('No Orders Found', 'OK');
      },
    });
  }
}

import { Component, inject, ViewChild } from '@angular/core';
import { Present } from '../../../models/PresentModel';
import { Observable } from 'rxjs';
import { PresentService } from '../../../services/present.service';
import { DonorService } from '../../../services/donor.service';
import { DataViewModule } from 'primeng/dataview';
import { Tag } from 'primeng/tag';
import { CardModule } from 'primeng/card';
import { signal } from '@angular/core';
import { LoginService } from '../../../services/login.service';
import { OrderService } from '../../../services/order.service';
import { MessageService } from 'primeng/api';
import { UserModel } from '../../../models/UserModel';
import { Table } from 'primeng/table';
@Component({
  selector: 'app-show-present',
  templateUrl: './show-present.component.html',
  styleUrl: './show-present.component.scss'
})
export class ShowPresentComponent {
  constructor(private messageService: MessageService) { }
  @ViewChild('dt') dt: Table | undefined;
  presentService: PresentService = inject(PresentService);
  donorService: DonorService = inject(DonorService);
  orderService: OrderService = inject(OrderService)
  loginService: LoginService = inject(LoginService)
  presents$: Observable<Present[]> = this.presentService.GetAllPresents();
  currentUserId: number = parseInt(localStorage.getItem('userId') || '0', 10);
  cart!: Present[]
  presents!: any[];
  layout: string = 'grid';
  filteredPresents: any[] = []; // רשימה מסוננת
  ngOnInit() {
    this.presents$.subscribe(
      (data) => {
        this.filteredPresents = [...data]; // יצירת עותק לנתונים המסוננים
      }
    );
  }
  applyFilterGlobal(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.toLowerCase();
    this.presents$.subscribe((next) => {
      this.filteredPresents = next.filter((item) =>
        item.name?.toLowerCase().includes(filterValue) ||
        item.price?.toString().toLowerCase().includes(filterValue) ||
        item.donorId?.toString().toLowerCase().includes(filterValue)
      );

    }, (error) => {
      // במקרה של שגיאה, הצגת הודעת שגיאה
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error, life: 2000 });
    }
    );
  }
  ngOnChanges() {
    this.currentUserId = Number(sessionStorage.getItem('userId'));
    this.orderService.cart$.subscribe(cart=>{
      this.cart=cart
    })  }

  AddToCart(p: Present) {
    console.log(this.currentUserId);

    this.currentUserId = Number(sessionStorage.getItem('userId'));

    this.orderService.AddToCart({ presentId: p.id, UserId: this.currentUserId }).subscribe(
      (next) => {
        this.orderService.cart$.subscribe(cart=>{
          this.cart=cart
        })
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'המתנה נוספה לסל', life: 700 });
      },
      (error) => {
        // במקרה של שגיאה, הצגת הודעת שגיאה
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error, life: 2000 });
      }

    )
  }
}



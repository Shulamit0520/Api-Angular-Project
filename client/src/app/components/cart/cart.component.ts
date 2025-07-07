import { Component, inject } from '@angular/core';
import { Present } from '../../models/PresentModel';
import { Observable } from 'rxjs';
import { PresentService } from '../../services/present.service';
import { DonorService } from '../../services/donor.service';
import { DataViewModule } from 'primeng/dataview';
import { Tag } from 'primeng/tag';
import { CardModule } from 'primeng/card';
import { signal } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { OrderService } from '../../services/order.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { map, filter } from 'rxjs/operators'; // Ensure map is imported correctly
import { orderDTO } from '../../models/DTO/OrderDTO';
import { UserModel } from '../../models/UserModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { Router } from '@angular/router';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
  constructor(private messageService: MessageService, private fb: FormBuilder, private router: Router,private confirmationService: ConfirmationService) { }

  presentService: PresentService = inject(PresentService);
  donorService: DonorService = inject(DonorService);
  orderService: OrderService = inject(OrderService)
  loginService: LoginService = inject(LoginService)
  currentOrder: orderDTO = new orderDTO;
  currentUserId: number = Number(sessionStorage.getItem('userId'));
  presents$: Observable<Present[]> = this.presentService.GetAllPresents();
  cartForm!: FormGroup;
  presents!: any[];
  layout: string = 'grid';
  cart!: Present[]

  price: number = 0;
  submitted: boolean = false;
  presentDialog: boolean = false;
  list:Present[]=[]
  flag:boolean[]=[]
  quantity:number[]=[]
  hideDialog() {
    this.presentDialog = false;
    this.submitted = false;
  }
ngOnChanges(){
this.orderService.cart$.subscribe(cart=>{
  this.cart=cart
})
}
  ngOnInit() {
      this.orderService.getUserCart(this.currentUserId); // קריאה לטעינת העגלה
      this.orderService.cart$.subscribe(cart => {
        this.cart = cart;
      });
      
       
    this.cartForm = this.fb.group({
      creditCard: [
        '',
        [Validators.required, Validators.pattern(/^\d{16}$/)], // בדיוק 16 ספרות
      ],
      validity: [
        '',
        [
          Validators.required,
          Validators.pattern(/^(0[1-9]|1[0-2])\/\d{2}$/), // פורמט MM/YY
        ],
      ],
      cvc: ['', [Validators.required, Validators.pattern(/^\d{3}$/)]], // בדיוק 3 ספרות
      id: [
        '',
        [
          Validators.required,
          Validators.pattern(/^\d{9}$/), // בדיוק 9 ספרות
        ],
      ],
    });
    this.orderService.getSumCart(Number(sessionStorage.getItem('userId'))).subscribe((p) => {
      this.price = p, console.log(this.price);

    })

  }
  async deleteOrder(p: Present) {
    this.currentUserId = Number(sessionStorage.getItem('userId'));
    this.currentOrder = { UserId: this.currentUserId, presentId: p.id }
    console.log(this.currentOrder);
                  this.orderService.deleteOrder(this.currentOrder).subscribe(
                    (next) => {
                      console.log(next);        
                      this.orderService.getUserCart(this.currentUserId); // קריאה לטעינת העגלה
                      this.orderService.cart$.subscribe(cart => {
                        this.cart = cart;
                      });                  
                      this.messageService.add({ severity: 'error', summary: '', detail: 'מתנה הוסרה מהסל', life: 1500 });
                      this.orderService.getSumCart(Number(sessionStorage.getItem('userId'))).subscribe((p) => {
                        this.price = p, console.log(this.price);
                      }, (error) => {
                        confirm(error.error.error);
                      })
                    }, (error) => {
                      confirm(error.error.error);
                    }
                  );
                   }
  // async deleteOrder(p: Present) {
  //   this.currentUserId = Number(sessionStorage.getItem('userId'));
  //   this.currentOrder = { UserId: this.currentUserId, presentId: p.id };
  
  //   try {
  //     await this.orderService.deleteOrder(this.currentOrder).toPromise(); // מחיקת מוצר
  //     this.orderService.getUserCart(this.currentUserId); // קריאה לטעינת העגלה
  //     this.orderService.cart$.subscribe(cart => {
  //       this.cart = cart;
  //       console.log('Cart:', cart); // וידוא שהתוכן מתעדכן
  //     });
  //     this.messageService.add({ severity: 'error', summary: '', detail: 'מתנה הוסרה מהסל', life: 1500 });
  
  //     const total = await this.orderService.getSumCart(this.currentUserId).toPromise(); // חישוב סכום
  //     // this.price = total;
  //     console.log(this.price);
  //   } catch (error) {
  //     console.error(error);
  //   }
  // }
  
  save() {
    this.presentDialog = true;
    this.submitted = true;

    this.orderService.Payment(Number(sessionStorage.getItem('userId'))).subscribe((data) => {
      console.log(data);
      this.orderService.cart$.subscribe(cart=>{
        this.cart=cart
      })  
            this.router.navigate([''])

    }, (error) => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error, life: 2000 });
    })
    if (this.cartForm.valid) {
      console.log('Form Data:', this.cartForm.value);
      this.orderService.cart$.subscribe(cart=>{
        this.cart=cart
      })
            this.messageService.add({ severity: 'successfull', summary: 'successfull', detail: '!התשלום התקבל בהצלחה,תזכו למצצות', life: 1500 });
      this.presentDialog = false; // סגירת הדיאלוג אחרי הצלחה
    } else {
      alert('בבקשה תמלא נכון את השדות');
    }
  }
}



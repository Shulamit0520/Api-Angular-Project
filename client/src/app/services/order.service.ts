import { inject, Injectable } from '@angular/core';
import { UserDTO } from '../models/DTO/UserDTO';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { orderDTO } from '../models/DTO/OrderDTO';
import { Present } from '../models/PresentModel';
import { OrderModel } from '../models/OrderModel';

@Injectable({
  providedIn: 'root'
})
 
export class OrderService {
  httpClient: HttpClient = inject(HttpClient);
  list:Present[]=[]
  constructor() { }
  BASE_URL = 'http://localhost:5000/api/Order';
  AddToCart(o: orderDTO): Observable<any> {
    return this.httpClient.post<any>(this.BASE_URL, o)   
  }
  private cartSubject = new BehaviorSubject<Present[]>([]);
  cart$ = this.cartSubject.asObservable();
  
  getUserCart(userId: number): void {
    const url = `http://localhost:5000/api/Order/getCart`;
    this.httpClient.get<Present[]>(`${url}/${userId}`).subscribe(data => {
      const list: Present[] = [];
      data.forEach(item => {
        const existingItem = list.find(x => x.id === item.id);
        if (existingItem) {
          existingItem.quantity! += 1;
        } else {
          list.push({ ...item, quantity: 1 });
        }
      });
      this.cartSubject.next(list);

    });
  }
  
  //  getUserCart(userId: number): Present[] {
  //   const url = 'http://localhost:5000/api/Order/getCart';
  //   this.list=[]
  //   console.log(this.list);
    
  //   var res= this.httpClient.get<Present[]>(`${url}/${userId}`)
  //   res.subscribe((data:Present[])=>{
  //   data.forEach(item => {
  //     // בדיקה אם הפריט כבר ברשימה
  //     const existingItem = this.list.find(x => x.id === item.id);
  //     if (existingItem) {
  //       existingItem.quantity!+=1; // הגדלת הכמות אם קיים
  //     } else {   
  //       this.list.push({ ...item, quantity: 1 });
  //     }
  //   });
  //  })
  //     console.log(this.list);
  //      return  this.list ||[]
  // }
  Payment(userId: number): Observable<any> {
    const url = 'http://localhost:5000/api/Order/payment';
    return this.httpClient.post<any>(`${url}/${userId}`,{})
  }

  deleteOrder(order: orderDTO): Observable<void> {
    return this.httpClient.delete<void>(this.BASE_URL, { body: order });

  }
   getSumCart(userId: number): Observable<number> {
    const url = 'http://localhost:5000/api/Order/getSumCart';
    return this.httpClient.get<number>(`${url}/${userId}`, );

  }

  getAllPayed(presentID: number): Observable<any>  {
    return this.httpClient.get<any[]>(`http://localhost:5000/api/Order/getPayed/${presentID}`)
    
  }
    
  getTotalSum(): Observable<any>  {
    return this.httpClient.get<any[]>('http://localhost:5000/api/Order/totalSum/')
  }
}
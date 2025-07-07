import { Component, inject } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Observable } from 'rxjs';
import { RaffleService } from '../../services/raffle.service';
import { PresentService } from '../../services/present.service';
import { Present } from '../../models/PresentModel';
import { UserModel } from '../../models/UserModel';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-raffle',
  templateUrl: './raffle.component.html',
  styleUrl: './raffle.component.css'
})
export class RaffleComponent {
  orderService: OrderService = inject(OrderService);
  presentService: PresentService = inject(PresentService);
  raffleService: RaffleService = inject(RaffleService);
  loginService: LoginService = inject(LoginService);

  presents$: Observable<Present[]> = this.presentService.GetAllPresents()
  value: boolean | null = null;
  winnerObj: UserModel | null = null
  winnerName: string = ''
  winnerID: number = -1
  i: number = 0
  totalsum$: Observable<any> = this.orderService.getTotalSum()
  sum: number = 0

  ngOnChanges() {
    this.totalsum$.subscribe(data => {
      console.log(data);
      this.sum = data
    })
    this.presents$=this.presentService.GetAllPresents()

  }


  ngOnInit() {
    this.totalsum$.subscribe(data => {
      console.log(data);
      this.sum = data
    })
  }
  async raffle(presentID: number) {
    console.log(presentID);
    this.raffleService.raffle(presentID).subscribe(
      (userData) => {
        this.presents$=this.presentService.GetAllPresents()
        console.log(userData);    
        this.winnerObj = userData;
        alert(`${this.winnerObj.fullName} זכה`);

      },
      (error) => {
        console.log(error.error.error);
        alert(error.error.error);
      }
    );
  }
  downloadExcel(): void {
    console.log("df");
    
    this.raffleService.downloadGiftsExcel();
  }
}

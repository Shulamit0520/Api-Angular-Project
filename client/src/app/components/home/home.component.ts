import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { NonNullableFormBuilder } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private router: Router) { }
  loginService: LoginService = inject(LoginService);
  userId: number = Number(sessionStorage.getItem('userId')) | -1
  roles = ''
  items: MenuItem[] | undefined;
  ngOnInit() {
    console.log(this.roles);

    this.items = [
      {
        label: 'ניהול',
        icon: 'pi pi-spin pi pi-cog',
        items: [
          {
            label: 'תורמים',
            route: '/donorsManage'
          },
          {
            label: 'מתנות',
            route: '/presentsManage'
          },
          {
            label: 'הגרלה',
            route: '/raffle'
          }
        ]
      },
      {
        label: 'מתנות לבחירה',
        icon: 'pi pi-th-large',
        route: '/presents'
        // command: () => {
        // this.router.navigate(['/presents']);
        // }
      },
      {
        label: 'כניסה',
        icon: ' pi pi-sign-in',
        route: '/login'

      },
      {
        label: 'יציאה',
        icon: ' pi pi-sign-out',
        route: '/logout'

      },
      {
        label: 'הרשמה',
        icon: ' pi pi-user-plus',
        route: '/register'

      },
      {
        label: 'דף הבית',
        icon: ' pi pi-home ',
        route: '/'

      },
    //  this.userId != -1?
      {
        label: 'סל קניות',
        icon: ' pi pi-shopping-cart',
        route: '/cart'

      }
    
    ];

    this.loginService.data$.subscribe(data => {
      this.roles = data; // קבלת הנתון מהשירות
    }, (error) => {
       confirm(error.error.error)  ;
  });

  }


}


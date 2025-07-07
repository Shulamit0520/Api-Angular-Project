import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
 constructor( private router: Router) {

  }
  ShowPresents(){
    if (sessionStorage.getItem('token')!= null)
      this.router.navigate(['/presents']);
    else
      this.router.navigate(['/login']);
    
  }
}

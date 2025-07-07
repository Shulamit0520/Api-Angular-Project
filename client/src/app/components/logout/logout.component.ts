import { Component, Inject } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent {
  flag:boolean=false
  constructor(private router: Router,private messageService: MessageService, private confirmationService: ConfirmationService) {  
    const token = sessionStorage.getItem('token');
    if(token == null)
        this.flag=  true
  }  
     loginService: LoginService = Inject(LoginService);

  async logOut() {

     if(confirm("האם את.ה רוצה בטוח לצאת?")){
      sessionStorage.clear()
       this.flag= true
       this.loginService.getUserDetails();
       this.router.navigate(['']);
     }
        
    //     this.messageService.add({ severity: 'successfull', summary: 'Successful', detail: 'תזכה למצוות!!', life: 4000 });
    //   }
    // }
    // );
  }

}


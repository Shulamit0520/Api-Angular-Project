import { Component } from '@angular/core';
import { UserModel } from '../../models/UserModel';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';
import { catchError, first, of, take, throwError } from 'rxjs';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup = new FormGroup({});

  constructor(public loginService: LoginService, private router: Router, private messageService: MessageService,) {
    this.loginForm = new FormGroup({
      username: new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(4), Validators.maxLength(4)]),
    });
  }
  user: UserModel = new UserModel();
  username!: string;
  passward!: string;
  errorMessage: string | null = null;

  // isUser:boolean = false;

  async send() {
    sessionStorage.clear();

    console.log(this.username, this.passward);
    this.user.username = this.username
    this.user.passward = this.passward
    await this.loginService.isUser(this.username, this.passward).subscribe({
      next: (data) => {
        console.log(data);
        if (data.token) {
          console.log(data);
          sessionStorage.setItem("token", data.token);
          this.loginService.getUserDetails();
          this.router.navigate(['/presents']);
        }
      },
      error: (err) => {
        console.error('Error during login:', err.error.error);
        // this.messageService.add({ severity: 'danger', detail: 'בבקשה הירשם', life: 1500 });

        if(confirm("בבקשה הירשם") == true)
          this.router.navigate(['/register']);

      },
      complete: () => {
        console.log("Login request completed successfully.");
      }
    });
  }


}    
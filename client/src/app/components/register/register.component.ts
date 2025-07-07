import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterService } from '../../services/register.service';
import { UserDTO } from '../../models/DTO/UserDTO';
import { first } from 'rxjs';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm!: FormGroup;
  // ivrit: RegExp = /^[a-z\s]*$/;
  emailReg: RegExp = new RegExp('[]@"".""')
  constructor(public registerService: RegisterService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      username: [
        '',
        [Validators.required, Validators.minLength(3), Validators.maxLength(10)]
      ],
      password: [
        '',
        [Validators.required,Validators.minLength(4)],
      ],
      email: [
        '',
        [Validators.required, Validators.email]
      ],
      phone: [
        '',
        [Validators.required, Validators.pattern(/^\d{10}$/), 
        ],
      ],
      fullName: [
        '',
        [Validators.required, Validators.minLength(5)]
      ]
    });
  }
  u: UserDTO = new UserDTO();

  send() {
    if (this.registerForm.valid) {
      console.log('Form Data:', this.registerForm.value);
      this.registerService.createUser(this.u).subscribe(
        () => {      
            this.router.navigate(['/login'])  
        }, (error) => {
          // במקרה של שגיאה, הצגת הודעת שגיאה
          alert(error.error.error)
        });
    } else {
      alert('בבקשה תמלא נכון את השדות');
    }
  }
}
// else
// window.alert("username or password duplicate")


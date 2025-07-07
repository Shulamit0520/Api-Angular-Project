import { inject, Injectable } from '@angular/core';
import { UserDTO } from '../models/DTO/UserDTO';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class RegisterService {
  httpClient: HttpClient = inject(HttpClient);
  constructor() { }
  createUser(u: UserDTO): Observable<any> {
    let url = 'http://localhost:5000/api/User/register'
    return this.httpClient.post<any>(url, u)
  }

}

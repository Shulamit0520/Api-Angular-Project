
import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { Donor } from '../models/DonorModel';
import { DonorDTO } from '../models/DTO/DonorDTO';
import { UserModel } from '../models/UserModel';
import { UserDTO } from '../models/DTO/UserDTO';
import { jwtDecode } from 'jwt-decode'
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  // private currentUserSubject = new BehaviorSubject<UserModel|null>(null);
  // currentUser: Observable<UserModel | null> = this.currentUserSubject.asObservable();
  currentUser: UserModel = new UserModel
  BASE_URL = 'http://localhost:5000/api/User';
  httpClient: HttpClient = inject(HttpClient);
  constructor() { }
  private token = sessionStorage.getItem('token'); // קבלת ה-Token שנשמר לאחר התחברות


  private dataSubject = new BehaviorSubject<string>(''); // משתנה משותף עם ערך התחלתי
  data$ = this.dataSubject.asObservable(); // Observable שניתן להאזין לו

  sendRoles(data: string) {
    this.dataSubject.next(data); // עדכון הנתון
  }

  isUser(username: string, passWard: string): Observable<any> {
    console.log(username, passWard);
    let a = {userName: username, passWard: passWard }
    let url = 'http://localhost:5000/api/User/login'
    return this.httpClient.post<any>(url, a)
  }
  getUserDetails() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.token}`, // הוספת ה-Token לכותרת הבקשה
    });
    const url = 'http://localhost:5000/api/User/userDetails';
    this.httpClient.get<UserModel>(url, { headers }).subscribe(
      (user) => {
        // this.currentUserSubject.next(user); // עדכון ה-BehaviorSubject
        this.currentUser = user
        sessionStorage.setItem('userId', user.id.toString())
        this.sendRoles(user.roles)
      },
      (err) => {
        console.error('Error occurred:', err.message);
      }
    );
  }
  getUserById(userId:number): Observable<UserModel> {
    return this.httpClient.get<UserModel>(`${this.BASE_URL}/${userId}`)
  }

}
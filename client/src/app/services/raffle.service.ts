import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from './login.service';
import { UserModel } from '../models/UserModel';
import { Present } from '../models/PresentModel';

@Injectable({
  providedIn: 'root'
})

export class RaffleService {

  donorService: LoginService = inject(LoginService);
  httpClient: HttpClient = inject(HttpClient);
  BASE_URL = 'http://localhost:5000/api/User';

  constructor() { }
  raffle(presentId: number):Observable<UserModel> {   
     let url = 'http://localhost:5000/api/Raffle/random'
    return this.httpClient.get<UserModel>(`${url}/${presentId}`)

  }
  createAuthorizationHeaders() {
    return {
        'Content-Type': 'application/json',  // הכותרת הזו תלויה בסוג הנתונים
  };
}
downloadGiftsExcel(): void {
    const url = this.BASE_URL+'/export_gifts'; // עדכן את ה-URL לפי ה-API שלך
    this.httpClient.get(url , { headers: this.createAuthorizationHeaders(),responseType: 'blob' }).subscribe(
      (blob) => {
        const link = document.createElement('a');
        const fileUrl = window.URL.createObjectURL(blob);
  
        link.href = fileUrl;
        link.download = 'Random.xlsx'; // שם הקובץ שיורד
        link.click();
  
        window.URL.revokeObjectURL(fileUrl); // נקה את הקובץ מהזיכרון
      },
      (error) => {
        console.error('Error downloading the file:', error);
        alert('Failed to download the file. Please try again.');
      }
    );
  }


}

import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

import { Donor } from '../models/DonorModel';
import { DonorDTO } from '../models/DTO/DonorDTO';
import { Present } from '../models/PresentModel';

@Injectable({
  providedIn: 'root'
})
export class DonorService {

  BASE_URL = 'http://localhost:5000/api/Donor';
  httpClient: HttpClient = inject(HttpClient);
  constructor() { }
  GetAllDonors(): Observable<Donor[]> {
    var res = this.httpClient.get<Donor[]>(this.BASE_URL);
    return res;
  }
  
  deleteDonor(id: number): Observable<void> {

    return this.httpClient.delete<void>(`${this.BASE_URL}/${id}`)
  }

  UpdateDonor(p: DonorDTO, id: number): Observable<void> {

    return this.httpClient.put<void>(`${this.BASE_URL}/${id}`, p);

  }

  AddDonor(value: DonorDTO): Observable<void> {
    return this.httpClient.post<void>(this.BASE_URL, value);

  }
  GetPresentsDonor(donorId:number): Observable<Present[]> {
    var res = this.httpClient.get<Present[]>(`${this.BASE_URL}/presentsDonor/${donorId}`);
    return res;
  }
//   FilterDonors(kind:string,value:string): Observable<Donor[]> {
// console.log(value);

//     var res = this.httpClient.get<Donor[]>(`${this.BASE_URL}/FilterDonors/${value}`);
//     console.log(res);
    
//     return res;
//   }
FilterDonors(name: string | '', present: string | ''): Observable<Donor[]> {
  // הכנת פרמטרים לשאילתה
  const params = new HttpParams()
    .set('name', name ?? '') // אם name הוא null, ישלח כערך ריק
    .set('present', present ?? ''); // כנ"ל עבור present

  // שליחת בקשה לשרת עם פרמטרים
  return this.httpClient.get<Donor[]>(`${this.BASE_URL}/FilterDonors`, { params });
}

}

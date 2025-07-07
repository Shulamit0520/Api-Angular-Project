
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Present } from '../models/PresentModel';
import { PresentDTO } from '../models/DTO/PresentDTO';
import { DonorService } from './donor.service';
import { Donor } from '../models/DonorModel';

@Injectable({
  providedIn: 'root'
})
export class PresentService {
  donorService: DonorService = inject(DonorService);
  BASE_URL = 'http://localhost:5000/api/present';
  httpClient: HttpClient = inject(HttpClient);
  constructor() { }
  GetAllPresents(): Observable<Present[]> {
    var res = this.httpClient.get<Present[]>(this.BASE_URL);
    return res;
  }
  getDonorDeatails(donorId:number): Observable<Donor> {
    var res = this.httpClient.get<Donor>(`${this.BASE_URL}/getDonorDeatails/${donorId}`);
    return res;
  }

  deletePresent(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.BASE_URL}/${id}`)
  }

  UpdatePresent(p: PresentDTO, id: number): Observable<void> {
    return this.httpClient.put<void>(`${this.BASE_URL}/${id}`, p);
  }

  AddPresent(value: PresentDTO): Observable<void> {
  console.log(value);
  console.log(value.donorId);
    return this.httpClient.post<void>(this.BASE_URL, value);
  }
}

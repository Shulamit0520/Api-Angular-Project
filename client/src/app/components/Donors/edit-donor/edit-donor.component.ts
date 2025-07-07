import { Donor } from '../../../models/DonorModel';
import { DonorService } from '../../../services/donor.service';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, inject } from '@angular/core';
import {  MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Observable, of } from 'rxjs';
import { DonorDTO } from '../../../models/DTO/DonorDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-edit-donor',
  templateUrl: './edit-donor.component.html',
  styleUrl: './edit-donor.component.css'
})
export class EditDonorComponent {
  donorForm: FormGroup = new FormGroup({});

constructor(private messageService: MessageService) { 
   this.donorForm = new FormGroup({
        name: new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
        phone: new FormControl(null, [Validators.required,Validators.pattern(/^\d{10}$/),]),
      });
}

  donorService: DonorService = inject(DonorService);


  @Input() donors$!: Observable<Donor[]>
  @Input() donor!: Donor
  @Input() donorEdit!: Donor
  @Output() sendDonors: EventEmitter<Observable<Donor[]>> = new EventEmitter<Observable<Donor[]>>();


  donorDialog: boolean = false;
  donorName: string = ''
  donorId: number = 0
  donorSelected!: Donor
  donorDTO: DonorDTO = new DonorDTO();
  submitted: boolean = false;

ngOnChanges(){
  this.donor = { ...this.donorEdit }

}

  editDonor() {
    this.donor = { ...this.donorEdit }
    console.log(this.donorEdit);
    
    this.donorDialog = true;
  }
  hideDialog() {
    this.donorDialog = false;
    this.submitted = false;
  }

  async saveDonor() {

    this.donorDTO.name = this.donor.name
    this.donorDTO.phone = this.donor.phone
    this.submitted = true;

    if (this.donor.name?.trim() && this.donor.phone) {
    
        this.donorService.UpdateDonor(this.donorDTO, this.donor.id).subscribe(
          (next) => {
          this.donors$ = this.donorService.GetAllDonors();
          this.sendDonors.emit(this.donors$)
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'מתנה עודכנה בהצלחה', life: 2000 });

          
        }, (error) => {
          // במקרה של שגיאה, הצגת הודעת שגיאה
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error, life: 2000 });
      });
      this.donorDialog = false;
      this.donor = { id: 0, name: '',phone: '' };    }
  }
}


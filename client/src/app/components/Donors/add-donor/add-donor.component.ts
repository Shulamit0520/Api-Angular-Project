import { Donor } from '../../../models/DonorModel';
import { DonorService } from '../../../services/donor.service';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, inject } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Observable, of } from 'rxjs';
import { DonorDTO } from '../../../models/DTO/DonorDTO';
import { EditDonorComponent } from '../edit-donor/edit-donor.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-add-donor',
  templateUrl: './add-donor.component.html',
  styleUrl: './add-donor.component.css'
})
export class AddDonorComponent {
  donorForm: FormGroup = new FormGroup({});

  constructor(private messageService: MessageService, private confirmationService: ConfirmationService) { 
    this.donorForm = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      phone: new FormControl(null, [Validators.required,Validators.pattern(/^\d{10}$/)]),
    });
  }
  

  donorService: DonorService = inject(DonorService);


  @Input() donors$!: Observable<Donor[]>
  @Output() sendDonors: EventEmitter<Observable<Donor[]>> = new EventEmitter<Observable<Donor[]>>();
  donorDialog: boolean = false;
  donors!: Donor[];
  donorName: string = ''
  donorId: number = 0
  donorSelected!: Donor
  donor: Donor = new Donor();
  donorDTO: DonorDTO = new DonorDTO();
  submitted: boolean = false;

  ngOnInit() {
    this.donors$ = this.donorService.GetAllDonors();
  }
  hideDialog() {
    this.donorDialog = false;
    this.submitted = false;
  }

  openNew() {

    this.submitted = false;
    this.donorDialog = true;
    console.log("add -component");
    
  }
  async saveDonor() {

    this.donorDTO.name = this.donor.name
    this.donorDTO.phone = this.donor.phone
    this.submitted = true;
    
    if (this.donor.name?.trim() && this.donor.phone) {

      this.donorService.AddDonor(this.donorDTO).subscribe(
        (next) => {
        this.donors$ = this.donorService.GetAllDonors();
        this.sendDonors.emit(this.donors$)
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'מתנה נוספה בהצלחה', life: 2000 });
      },
       (error) => {
        // במקרה של שגיאה, הצגת הודעת שגיאה
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error , life: 2000 });
    });
    }
    this.donorDialog = false;
    this.donor = { id: 0, name: '',phone: '' };       }


}

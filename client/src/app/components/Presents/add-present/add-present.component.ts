import { Present } from '../../../models/PresentModel';
import { PresentService } from '../../../services/present.service';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, inject } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Observable, of } from 'rxjs';
import { DonorService } from '../../../services/donor.service';
import { Donor } from '../../../models/DonorModel';

import { PresentDTO } from '../../../models/DTO/PresentDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginComponent } from '../../login/login.component';
@Component({
  selector: 'app-add-present',
  templateUrl: './add-present.component.html',
  styleUrl: './add-present.component.css'
})
export class AddPresentComponent {
 presentForm: FormGroup = new FormGroup({});

  constructor(private messageService: MessageService, private confirmationService: ConfirmationService) { 
    // this.presentForm = new FormGroup({
    //   name: new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    //   price: new FormControl(null, [Validators.required,Validators.min(10)]),
    //   image: new FormControl(null, [Validators.required, Validators.pattern(/\.(jpg|jpeg|png)$/i)]),
    //   category: new FormControl(null, [Validators.required,Validators.minLength(2)]),
    //   // donor: new FormControl(null, [Validators.required])
    //   donor: new FormControl(null, Validators.required),

    // });
    this.presentForm = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      details: new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      price: new FormControl(null, [Validators.required, Validators.min(10)]),
      image: new FormControl(null, [Validators.required, Validators.pattern(/\.(jpg|jpeg|png)$/i)]),
      category: new FormControl(null, [Validators.required, Validators.minLength(2)]),
      donor: new FormControl(null, [Validators.required]), // תקין כעת
    });
  
  }
  onDonorChange(event: any) {
    console.log('Selected Donor:', event.value);
    this.presentDTO.donorId = event.value.id;
  }
  presentService: PresentService = inject(PresentService);
  donorService: DonorService = inject(DonorService);

  @Input() presents$!: Observable<Present[]>
  @Output() sendPresents: EventEmitter<Observable<Present[]>> = new EventEmitter<Observable<Present[]>>();

  donors$: Observable<Donor[]> = this.donorService.GetAllDonors();
  presentDialog: boolean = false;
  presents!: Present[];
  donorName: string = ''
  donorId: number = 0
  donorSelected!: Donor
  present: Present = new Present();
  presentDTO: PresentDTO = new PresentDTO();
  submitted: boolean = false;
  testArray?: Present[];
  ngOnInit() {
    this.donors$ = this.donorService.GetAllDonors();
  }
  openNew() {
    this.present.price = 10;
    this.submitted = false;
    this.presentDialog = true;
  }


  hideDialog() {
    this.presentDialog = false;
    this.submitted = false;
  }

  async savePresent() { 
    this.submitted = true;
    this.presentDTO.name = this.present.name
    this.presentDTO.details = this.present.details
    this.presentDTO.price = this.present.price
    this.presentDTO.category = this.present.category
    this.presentDTO.image = this.present.image
    //לזכור לשנות את זה חזרה
    console.log(this.donorSelected);
    
  
    if (this.presentDTO.name?.trim() && this.presentDTO.price && this.presentDTO.donorId) {

      this.presentService.AddPresent(this.presentDTO).subscribe(
        (next) => {
            const message  = 'מתנה נוספה בהצלחה';
            this.presents$ = this.presentService.GetAllPresents();
            this.sendPresents.emit();
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: message, life: 2000 });
        },
        (error) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error || 'שגיאה בעת הוספת המתנה', life: 2000 });
        }
    );
    }
    this.presents$ = this.presentService.GetAllPresents();
    this.presentDialog = false;
    this.present = { id: 0, name: '',details:'', price: 10, image: '', donorId: 0 ,category:'',winner:"",isRaffle:false};
  }


}

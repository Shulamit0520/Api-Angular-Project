import { Present } from '../../../models/PresentModel';
import { PresentService } from '../../../services/present.service';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, inject } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Observable, of } from 'rxjs';
import { DonorService } from '../../../services/donor.service';
import { Donor } from '../../../models/DonorModel';
import { PresentDTO } from '../../../models/DTO/PresentDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-present',
  templateUrl: './edit-present.component.html',
  styleUrl: './edit-present.component.css'
})
export class EditPresentComponent {
  presentForm: FormGroup = new FormGroup({});

  constructor(private messageService: MessageService, private confirmationService: ConfirmationService) {
    // this.presentForm = new FormGroup({
    //   name: new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    //   price: new FormControl(null, [Validators.required, Validators.min(10)]),
    //   image: new FormControl(null, [Validators.required, Validators.pattern(/\.(jpg|jpeg|png)$/i)]),
    //   category: new FormControl(null, [Validators.required, Validators.minLength(2)])
    //   // donor: new FormControl(null, [Validators.required])
    // })
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
    this.presentDTO.donorId = event.value.id;
  }

  presentService: PresentService = inject(PresentService);
  donorService: DonorService = inject(DonorService);


  @Input() presents$!: Observable<Present[]>
  @Input() present!: Present
  @Input() presentEdit!: Present
  @Output() sendPresents: EventEmitter<Observable<Present[]>> = new EventEmitter<Observable<Present[]>>();



  donors$: Observable<Donor[]> = this.donorService.GetAllDonors();
  presentDialog: boolean = false;
  donorName: string = ''
  donorId: number = 0
  donorSelected?: Donor
  presentDTO: PresentDTO = new PresentDTO();
  submitted: boolean = false;

  // ngOnInit(){  
  //     this.donors$ = this.donorService.GetAllDonors();
  //     console.log(this.present);  
  // }

  ngOnChanges() {
    
    this.present = { ...this.presentEdit }
  }

  editPresent() {
    this.donors$.subscribe(data => {
    const donorName= data.find(x => x.id === this.presentEdit.donorId)
    this.donorName=donorName?.name||''
    this.presentDTO.donorId=donorName?.id||1
})
    this.present = { ...this.presentEdit }
    this.presentDialog = true;
  }

  hideDialog() {
    this.presentDialog = false;
    this.submitted = false;
  }

  async savePresent() {
    this.presentDTO.name = this.present.name
    this.presentDTO.details = this.present.details
    this.presentDTO.price = this.present.price
    this.presentDTO.category = this.present.category
    this.presentDTO.image = this.present.image
    
    this.submitted = true;
    if (this.present.name?.trim() && this.present.price && this.present.donorId) {
      if (this.present.id != 0) {
        this.presentService.UpdatePresent(this.presentDTO, this.present.id).subscribe(
          (next) => {
            this.presents$ = this.presentService.GetAllPresents();
            this.sendPresents.emit()
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'מתנה עודכנה בהצלחה', life: 2000 });
            this.present = { id: 0, name: '',details:'', price: 10, image: '', donorId: 0 ,category:'',winner:"",isRaffle:false};
            this.donorSelected = { id: 0, name: '', phone: '' }
          }, (error) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error, life: 2000 });
          });

      }
      this.presentDialog = false;
    }
  }
}

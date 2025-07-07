import { Donor } from '../../../models/DonorModel';
import { DonorService } from '../../../services/donor.service';
import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Observable, of } from 'rxjs';
import { DonorDTO } from '../../../models/DTO/DonorDTO';
import { EditDonorComponent } from '../edit-donor/edit-donor.component';
import { AddDonorComponent } from '../add-donor/add-donor.component';
import { Present } from '../../../models/PresentModel';
@Component({
    selector: 'app-donor',
    templateUrl: './donor.component.html',
    styleUrl: './donor.component.css'
})
export class DonorComponent {
    constructor(private messageService: MessageService, private confirmationService: ConfirmationService) { }
    @ViewChild('dt') dt: Table | undefined;
    @ViewChild(AddDonorComponent) addDonorComponent: AddDonorComponent | undefined;
    @ViewChild(EditDonorComponent) editDonorComponent: EditDonorComponent | undefined;


    donorService: DonorService = inject(DonorService);
    donors$: Observable<Donor[]> = this.donorService.GetAllDonors();
    donor: Donor = new Donor();
    donorEdit: Donor = new Donor();
    presentsDonor$!: Observable<Present[]>
    f: number = 0
    presentDialog: boolean = false
    kind:string='name';
    value:string=''


    getPresentsDonor(donorId: number) {
        console.log(this.value);
        console.log(this.kind);    
        this.f = donorId
        this.presentsDonor$ = this.donorService.GetPresentsDonor(donorId);
        this.presentDialog = true
    }
    
    ngOnInit() {
        this.donor = { id: 0, name: '', phone: '' };
        // this.donors$ = this.donorService.GetAllDonors();
    }
   
    filter(){
        console.log("value" ,this.value);
        console.log("kind",this.kind);  
    if(this.kind=='name')
        this.donors$ = this.donorService.FilterDonors(this.value,'');
    if(this.kind=='present')
        this.donors$ = this.donorService.FilterDonors('',this.value);

    }
    openNew(x: number, donorEdit: Donor) {
        this.donorEdit = donorEdit
        if (x == 1) {
            this.addDonorComponent?.openNew()
            this.donors$ = this.donorService.GetAllDonors();
        }



        else {
            this.editDonorComponent?.editDonor()
            this.donors$ = this.donorService.GetAllDonors();
        }

    }

    async deleteDonor(donor: Donor) {
        this.confirmationService.confirm({
            message: '?' + donor.name + ' האם את.ה רוצה בטוח למחוק ',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.donorService.deleteDonor(donor.id).subscribe((next) => {
                    this.donors$ = this.donorService.GetAllDonors();
                    this.donor = { id: 0, name: '', phone: '' };
                    this.messageService.add({ severity: 'error', summary: 'Successful', detail: ' תורם הוסר מהרשימה', life: 2000 });
                }, (error) => {
                    // במקרה של שגיאה, הצגת הודעת שגיאה
                    alert(error.error.error)
                }
                )

            }
        }
        );
        this.donor = { id: 0, name: '', phone: '' };
    }

    getSeverity(donor?: string) {
        return "not"
    }
    setDonors() {
        this.donors$ = this.donorService.GetAllDonors();
    }
}

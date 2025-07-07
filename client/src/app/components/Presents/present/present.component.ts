import { Present } from '../../../models/PresentModel';
import { PresentService } from '../../../services/present.service';
import { Component, Input, OnInit, ViewChild, inject } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Observable, of } from 'rxjs';
import { DonorService } from '../../../services/donor.service';
import { AddPresentComponent } from '../add-present/add-present.component';
import { EditPresentComponent } from '../edit-present/edit-present.component';
import { Donor } from '../../../models/DonorModel';

@Component({
    selector: 'app-present',
    templateUrl: './present.component.html',
    styleUrl: './present.component.css'
})
export class PresentComponent {
    constructor(private messageService: MessageService, private confirmationService: ConfirmationService) { }
    @ViewChild('dt') dt: Table | undefined;
    @ViewChild(AddPresentComponent) addPresentComponent: AddPresentComponent | undefined;
    @ViewChild(EditPresentComponent) editPresentComponent: EditPresentComponent | undefined;


    applyFilterGlobal($event: any, stringVal: any) {
        this.dt!.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
    }

    presentService: PresentService = inject(PresentService);
    donorService: DonorService = inject(DonorService);
    presents$: Observable<Present[]> = this.presentService.GetAllPresents();
    present: Present = new Present();
    presentEdit: Present = new Present();
    presentsAccept$?: Present;
    testArray?: Present[];
    flag: boolean = false
    presentDialog: boolean = false;
    donorDialog: boolean = false
    dtails: Donor = new Donor();
    ngOnChanges() {
        this.presents$ = this.presentService.GetAllPresents();
    }

    ngOnInit() {
        this.presents$ = this.presentService.GetAllPresents();
    }

async openNew(x: number, presentEdit: Present) {

        this.presentEdit = presentEdit
        if (x == 1) {
            this.addPresentComponent?.openNew()
            this.presents$ = this.presentService.GetAllPresents();
        }
        else {
            this.editPresentComponent?.editPresent()
            this.presents$ = this.presentService.GetAllPresents();
        }
    }

    async deletePresent(present: Present) {
        this.confirmationService.confirm({
            message: '?' + present.name + ' האם את.ה רוצה בטוח למחוק ',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            // closeOnEscape: true,
            accept: () => {
                this.presentService.deletePresent(present.id).subscribe(
                    (next) => {
                        this.presents$ = this.presentService.GetAllPresents();
                        this.messageService.add({ severity: 'error', summary: 'Successful', detail: ' מתנה הוסרה מהרשימה', life: 2000 });
                    },
                    (error) => {
                        // במקרה של שגיאה, הצגת הודעת שגיאה
                        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.error || 'שגיאה בעת הוספת המתנה', life: 2000 });
                    }
                )
            }
        }
        );
        this.present = { id: 0, name: '',details:'', price: 10, image: '', donorId: 0 ,category:'',winner:"",isRaffle:false};
    }
    
    setPresents() {
        this.presents$ = this.presentService.GetAllPresents();
    }

    getDonorDetails(donorId: number) {
        this.donorDialog = true
        this.present.donorId=donorId
        this.presentService.getDonorDeatails(donorId).subscribe(d => {
            this.dtails = d
        })
    }
}

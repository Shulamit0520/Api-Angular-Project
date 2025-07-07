import { Donor } from "./DonorModel";

export class Present {
    id:number=0;
    name!: string ; 
    details!: string ;   
    price: number = 10
    category: string ='';
    image: string='';
    donorId: number = 0;
    winner:string='';
    isRaffle:boolean=false
    quantity?: number=1; // שדה אופציונלי

}

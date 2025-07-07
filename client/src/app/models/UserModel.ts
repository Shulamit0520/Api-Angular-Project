export class UserModel
{
    id:number=0;
    username!:string 
    passward!:string 
    email?:string 
    phone!:string 
    roles:string = "user";
    winner:boolean=false;
    fullName!:string ;
}

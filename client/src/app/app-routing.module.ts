import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DonorComponent } from './components/Donors/donor/donor.component';
import { PresentComponent } from './components/Presents/present/present.component';
import { HomeComponent } from './components/home/home.component';
import { ShowPresentComponent } from './components/Presents/show-present/show-present.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CartComponent } from './components/cart/cart.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import {RaffleComponent} from './components/raffle/raffle.component'
import { LogoutComponent } from './components/logout/logout.component';

const routes: Routes = [
  {path:'', component:HomePageComponent},
  // {path:'', redirectTo:'donors', pathMatch:'prefix'},
  { path: 'donorsManage', component: DonorComponent },
  { path: 'presentsManage', component: PresentComponent },
  { path: 'raffle', component: RaffleComponent },

  { path: 'presents', component: ShowPresentComponent },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'cart', component: CartComponent }
  // {path:'', redirectTo:'start-learning', pathMatch:'full'},
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

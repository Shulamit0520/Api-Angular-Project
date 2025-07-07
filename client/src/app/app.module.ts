
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
// PrimeNG Modules
import { AccordionModule } from 'primeng/accordion';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { InputNumberModule } from 'primeng/inputnumber';
import { PasswordModule } from 'primeng/password';
import { SelectButtonModule } from 'primeng/selectbutton';
import { DataViewModule } from 'primeng/dataview';
import { MenubarModule } from 'primeng/menubar';
import { CardModule } from 'primeng/card';
import { OrderListModule } from 'primeng/orderlist';
import { TriStateCheckboxModule } from 'primeng/tristatecheckbox';



// Components
import { PresentComponent } from './components/Presents/present/present.component';
import { DonorComponent } from './components/Donors/donor/donor.component';
import { AddPresentComponent } from './components/Presents/add-present/add-present.component';
import { EditPresentComponent } from './components/Presents/edit-present/edit-present.component';
import { AddDonorComponent } from './components/Donors/add-donor/add-donor.component';
import { EditDonorComponent } from './components/Donors/edit-donor/edit-donor.component';
import { ShowPresentComponent } from './components/Presents/show-present/show-present.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

// Services and Interceptors
import { MessageService, ConfirmationService } from 'primeng/api';
import { PresentService } from './services/present.service';
import { DonorService } from './services/donor.service';
import { TokenInterceptor } from './TokenInterceptor.interceptor';
import { CartComponent } from './components/cart/cart.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ImageModule } from 'primeng/image';
import { RaffleComponent } from './components/raffle/raffle.component';
import { LogoutComponent } from './components/logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    PresentComponent,
    DonorComponent,
    AddPresentComponent,
    EditPresentComponent,
    AddDonorComponent,
    EditDonorComponent,
    ShowPresentComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    CartComponent,
    HomePageComponent,
    RaffleComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AccordionModule,
    ButtonModule,
    TableModule,
    DialogModule,
    RippleModule,
    ToastModule,
    ToolbarModule,
    ConfirmDialogModule,
    InputTextModule,
    InputTextareaModule,
    FileUploadModule,
    DropdownModule,
    TagModule,
    RadioButtonModule,
    RatingModule,
    InputNumberModule,
    PasswordModule,
    SelectButtonModule,
    DataViewModule,
    MenubarModule,
    CardModule,
    OrderListModule,
    ImageModule,
    TriStateCheckboxModule,
    OrderListModule
  ],
  providers: [
    MessageService,
    ConfirmationService,
    PresentService,
    DonorService,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

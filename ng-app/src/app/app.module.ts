import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {  HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { ContentComponent } from './content/content.component';

//
import {MatIconModule} from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SignupPageComponent } from './signup-page/signup-page.component';
import { RezerveComponent } from './rezerve/rezerve.component';
import { PlaneComponent } from './plane/plane.component';
import { PlaneService } from './Service/plane.service';
import { PlaneUpdateComponent } from './plane/plane-update/plane-update.component';
import { CartComponent } from './cart/cart.component';
import { AuthInterceptor } from './auth.interceptor';
import { AllRezerveComponent } from './rezerve/all-rezerve/all-rezerve.component';
import { RezDetailsComponent } from './rezerve/rez-details/rez-details.component';
import { CheckoutComponent } from './checkout/checkout.component';



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ContentComponent,
    LoginPageComponent,
    SignupPageComponent,
    RezerveComponent,
    PlaneComponent,
    PlaneUpdateComponent,
    CartComponent,
    AllRezerveComponent,
    RezDetailsComponent,
    CheckoutComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatIconModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    PlaneService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi:true
    }
    // AuthInterceptor,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


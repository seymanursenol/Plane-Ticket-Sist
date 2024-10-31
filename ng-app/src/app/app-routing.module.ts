import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentComponent } from './content/content.component';
import { LoginPageComponent } from './login-page/login-page.component';

import { NavbarComponent } from './navbar/navbar.component';
import { SignupPageComponent } from './signup-page/signup-page.component';
import { RezerveComponent } from './rezerve/rezerve.component';
import { PlaneComponent } from './plane/plane.component';
import { PlaneUpdateComponent } from './plane/plane-update/plane-update.component';
import { CartComponent } from './cart/cart.component';
import { AllRezerveComponent } from './rezerve/all-rezerve/all-rezerve.component';
import { RezDetailsComponent } from './rezerve/rez-details/rez-details.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { AdminGuard } from './guards/admin-guard';


const routes: Routes = [
  {path: '', component: ContentComponent},
  {path: 'navbar', component: NavbarComponent},
  {path: 'login-page', component: LoginPageComponent},
  {path: 'rezerve', component: RezerveComponent},
  {path: 'signup-page', component: SignupPageComponent },
  {path: 'planes', component: PlaneComponent,canActivate: [AdminGuard]},
  {path: 'plane_edit/:id', component: PlaneUpdateComponent,canActivate: [AdminGuard]},
  {path: 'rezerve/:name', component: RezerveComponent},
  {path: 'cart', component: CartComponent},
  {path: 'allRez', component: AllRezerveComponent,canActivate: [AdminGuard]},
  {path: 'rezDetails/:cartId', component: RezDetailsComponent ,canActivate: [AdminGuard]},
  {path: 'checkout/:planeId', component: CheckoutComponent}
 ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

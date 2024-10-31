import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Planes } from '../Models/Planes';
import { PlaneService } from '../Service/plane.service';
import { Rezervation } from '../Models/Rezervation';
import { RezervationService } from '../Service/rezervation.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent {


  plane: Planes | undefined;
  Planes: Planes | undefined;
  model: any={};
  Rezervation: Rezervation[];


  constructor(private route: ActivatedRoute, private router: Router, private rezervationService: RezervationService, private planeService: PlaneService){}
  ngOnInit(){
    const id = this.route.snapshot.paramMap.get('planeId');
    console.log("plane: " +id);

    if (id) {
      this.planeService.getPlaneById(+id).subscribe(plane => {
        this.plane = plane;
        this.Planes = { ...plane };
      });
    } else {
      console.warn("Geçersiz cartId.");
    }
  }


  checkTicket() {
    const _model: Rezervation= {
      UserName: this.model.UserName,
      Phone: this.model.Phone,
      Email: this.model.Email,
      CardName: this.model.CardName,
      CardNumber: this.model.CardNumber,
      ExpirationMonth: this.model.Month,
      ExpirationYear: this.model.Year,
      Cvc: this.model.Cvc
    }
    this.rezervationService.addPayment(_model).subscribe(response=>{
      console.log('Payment successful', response);
    },
    error => {
      console.error('Payment failed', error);
    })
    console.log("Veriler: ", _model);
  }

}

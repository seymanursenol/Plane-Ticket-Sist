import { Component } from '@angular/core';
import { PlaneService } from '../Service/plane.service';
import { EnumPlaneState, IPlane, Planes } from '../Models/Planes';
import { NgForm } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { format } from 'date-fns';
import { AuthService } from '../Service/auth.service';
import { tap } from 'rxjs';
@Component({
  selector: 'app-plane',
  templateUrl: './plane.component.html',
  styleUrls: ['./plane.component.css']
})
export class PlaneComponent {



  planes : Planes[];
  selectedPlane: Planes | null = null;
  model : any = {};
  form: NgForm;
  userProfile: any;
  plane: Planes | undefined;
  showCurrentOnly: boolean = false; // Default değeri false


  constructor(private authService: AuthService, private planeService : PlaneService,private router: Router,){}



  ngOnInit(): void {
    this.authService.userProfile$
    .pipe(
      tap(x=> console.log("plane user:",x))
    )
    .subscribe(profile => {
      this.userProfile = profile;
    });
    this.getPlane();

  }

  getPlane() {
    this.planeService.getPlanes().subscribe(Planes => {
      this.planes = this.showCurrentOnly ? Planes.filter(plane => plane.planeState === 0) : Planes;
    });
  }


  planeCreate(form:NgForm){
    this.authService.userProfile$
    .pipe(
      tap(x=> console.log("plane user:",x))
    )
    .subscribe(profile => {
      this.userProfile = profile;
    });
    // debugger;
    const _model: Planes = {
      planeState: this.model.EnumPlaneState,
      incoming: this.model.incoming,
      outgoing: this.model.outgoing,
      price: this.model.price,
      ticketTotal: this.model.ticketTotal,
      time: format(Date.parse(this.model.time), 'yyyy-MM-dd HH:mm:ss')
    }

    this.planeService.createPlane(_model).subscribe(p=>{
       console.log("Ekleme başarılı");
       this.router.navigate(['/planes']).then(() => {
        debugger;
        window.location.reload();}
       );
    },error=>{
      console.log("Ekleme başarısız"+ error);
    })
  }

  planeDelete(planeId: number) {
    this.planeService.getPlaneDelete(planeId).subscribe(response => {
      console.log("Plane status updated:", response);
      window.location.reload();
    }, error => {
      console.error("Error updating plane status:", error);
    });
  }
}


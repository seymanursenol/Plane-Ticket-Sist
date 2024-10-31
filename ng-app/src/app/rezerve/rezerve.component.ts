import { Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PlaneService } from '../Service/plane.service';
import { IPlaneId, Planes } from '../Models/Planes';
import { NgForm } from '@angular/forms';
import { Rezervation } from '../Models/Rezervation';
import { HttpClient } from '@angular/common/http';
import { User } from '../Models/User';
import { AuthService } from '../Service/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'rezerve',
  templateUrl: './rezerve.component.html',
  styleUrls: ['./rezerve.component.css']
})
export class RezerveComponent implements OnDestroy {
  isAuthenticated: boolean = false;
  @ViewChild('flightList') flightList: ElementRef;

  subscribeList = new Subscription();
  constructor(private route: ActivatedRoute, private planesService: PlaneService, private router: Router, private http: HttpClient, private authService: AuthService) {}
  ngOnDestroy(): void {
    //TODO : unsubscribe ol
    this.subscribeList.unsubscribe();
  }

  cityName: string = '';
  outgoing: string = '0';
  incoming: string = '0';
  planes: Planes[] = [];
  model: any = {};
  userId: string | null = null;
  _planes: IPlaneId[];
  count: number;

  ngOnInit() {
    this.cityName = this.route.snapshot.paramMap.get('name') || '';
  this.subscribeList.add(
     this.authService.userProfile$.subscribe(profile => {
      this.isAuthenticated = profile;
    }));

  }

  searchPlanes(form: NgForm) {
    this.planesService.getPlanesByCities(this.outgoing, this.incoming).subscribe(
      (data: Planes[]) => {
        this.planes = data;
        this.count = this.planes.length;
        console.log("Toplam: ", this.count)
      }
    );
    this.scrollToFlightList();
    this.getUserId();
  }

  scrollToFlightList() {
    if (this.flightList) {
      this.flightList.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }

  AddRezervation(form: NgForm) {
    if(this.isAuthenticated==null){
      alert("Lütfen giriş yapın.");
      this.router.navigate(['/login-page']);
    }
      const planeId = this.model.planeId;
      const UserId = this.model.userId;

      console.log("Gönderilen veriler:", planeId);
      this.planesService.AddRez(planeId).subscribe(response => {
          console.log('Rezervasyon başarıyla eklendi');
              this.router.navigate(['/checkout/'+planeId]);
          },
          error => {
            console.log("IPLaneId: ", planeId)
              console.error('Rezervasyon eklenirken hata oluştu:', error);
          }
      );
  }

  setModelValues(plane: any) {
    this.model.planeId = plane.id;
    this.model.userId = this.userId;
  }


  getUserId() {
    const token = localStorage.getItem('token');
    this.http.get('http://localhost:5177/api/Planes/GetUserId', {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }).subscribe(
      (response: any) => {
        this.userId = response.userId;
      },
      error => {
        console.error('Kullanıcı ID\'si alınırken hata oluştu:', error);
      }
    );
  }

}

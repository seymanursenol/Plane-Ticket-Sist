import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Service/auth.service';
import { PlaneService } from '../Service/plane.service';
import { Planes } from '../Models/Planes';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.css']
})
export class ContentComponent implements OnInit {


  planes: Planes[];
  isLoggedIn = false;

  constructor(private authService: AuthService, private router: Router, private planeService: PlaneService,private http: HttpClient) { }

  ngOnInit(): void {


    this.getPlane();
  }
  getPlane()
  {
    this.planeService.getPlanes().subscribe(Planes=>{
      this.planes= Planes;
    })
  }

}

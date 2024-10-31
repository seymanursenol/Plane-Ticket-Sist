import { Injectable } from '@angular/core';
import { Rezervation } from '../Models/Rezervation';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RezervationService {
  private apiUrl = 'http://localhost:5177/api/Cart/';
  constructor(private http: HttpClient) { }

  ngInOnit(){

  }

  addPayment(rezervation: Rezervation){
    const token = localStorage.getItem('token');
      return this.http.post(this.apiUrl + "CheckOut", rezervation, {
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        }
    });
  }
}

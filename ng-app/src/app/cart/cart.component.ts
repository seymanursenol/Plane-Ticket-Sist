import { Component } from '@angular/core';
import { CartService } from '../Service/cart.service';
import { Cart } from '../Models/Cart';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  private apiUrl = 'http://localhost:5177/api/Cart/';
  userTokenData: any;
  cart: Cart[];

  constructor(private cartService: CartService, private route: Router, private activeRouter: ActivatedRoute, private http: HttpClient) {}

  ngOnInit() {
    this.getCart();
  }

  getCart() {
    const token = localStorage.getItem('token');

    this.http.get('http://localhost:5177/api/Cart/GetTicket', {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    }).subscribe(response => {
      this.userTokenData = response;
      console.log('Sepet bilgileri:', this.userTokenData);

      // Burada toplam fiyatı hesaplayın
    }, error => {
      console.error('Sepet bilgileri alınamadı:', error);
    });
  }

}

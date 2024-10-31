import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart, CartItem } from '../Models/Cart';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = environment.apiUrl+ 'Cart';

  constructor(private http: HttpClient) { }

  getAllCarts(): Observable<Cart[]> {
    return this.http.get<Cart[]>(this.apiUrl);
  }
  getCartDetails(cartId: string): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/GetDetails/${cartId}`);
  }
  cartItemDelete(cartItemId: CartItem): Observable<CartItem>{
    return this.http.delete<CartItem>(`${this.apiUrl}/cartItemDelete/${cartItemId.cartItemId}`);
  }
}

import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Cart, CartItem } from 'src/app/Models/Cart';
import { CartService } from 'src/app/Service/cart.service';

@Component({
  selector: 'app-rez-details',
  templateUrl: './rez-details.component.html',
  styleUrls: ['./rez-details.component.css']
})
export class RezDetailsComponent {

  carts: Cart[] =[];
  cart: Cart | undefined;
  Carts: Cart | undefined;
  cartId: string | null;
  searchTerm: string = '';
  cartItems: CartItem[];

  constructor( private router: Router,private cartService: CartService,private route: ActivatedRoute) {}
  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('cartId');
    console.log("cart: ", id);

    if (id) {
      this.cartService.getCartDetails(id).subscribe(cart => {
        console.log("Api'den gelen veri: ", cart);
        this.cart = cart; // Artık burada cart bir nesne olacak
      }, error => {
        console.error("Hata: ", error);
      });

    } else {
      console.warn("Geçersiz cartId.");
    }
  }


  convertToDate(dateString: string): Date {
    const parts = dateString.split(' ');
    const dateParts = parts[0].split('.');
    const timeParts = parts[1].split(':');
    return new Date(
      +dateParts[2], // Yıl
      +dateParts[1] - 1, // Ay (0 tabanlı)
      +dateParts[0], // Gün
      +timeParts[0], // Saat
      +timeParts[1], // Dakika
      +timeParts[2] // Saniye
    );
  }
  DeleteCartDetails(cartItem: CartItem) {
      this.cartService.cartItemDelete(cartItem).subscribe(c=>{
        this.cartItems.splice(this.cartItems.findIndex(c=>c.cartItemId==cartItem.cartItemId),1);
      });
      window.location.reload();
      console.log(cartItem);
    }
}

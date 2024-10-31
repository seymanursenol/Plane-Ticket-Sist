import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Cart } from 'src/app/Models/Cart';
import { CartService } from 'src/app/Service/cart.service';

@Component({
  selector: 'app-all-rezerve',
  templateUrl: './all-rezerve.component.html',
  styleUrls: ['./all-rezerve.component.css']
})
export class AllRezerveComponent {

  carts: Cart[] = [];
  searchTerm: string = ''; // Arama terimi için değişken
  showMore: { [key: number]: boolean } = {}; // Hangi cart için daha fazla gösterileceğini saklar

  constructor(private router: Router, private cartService: CartService) {}

  ngOnInit() {
    this.getAllCart();
  }

  getAllCart() {
    this.cartService.getAllCarts().subscribe((data: Cart[]) => {
      this.carts = data;
      console.log(this.carts);
    });
  }

  filteredCarts(): Cart[] {
    if (!this.searchTerm) {
      return this.carts; // Arama terimi boşsa tüm cart'ları döndür
    }

    const lowerCaseSearchTerm = this.searchTerm.toLowerCase();

    // Arama terimine göre filtrele
    return this.carts.filter(cart =>
      cart.userItemsModels.some(user =>
        user.userName.toLowerCase().includes(lowerCaseSearchTerm)
      )
    );
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

  shouldShowMore(cartId: number): boolean {
    return this.showMore[cartId];
  }

  toggleShowMore(cartId: number): void {
    this.showMore[cartId] = !this.showMore[cartId];
  }
}

export interface Cart {
  cartId: number;
  userId: string;
  cartItems: CartItem[];
  userItemsModels: UserItem[];
}

export interface CartItem {
  cartItemId: number;
  outGoing: string;
  inComing: string;
  price: number;
  ticketTotal: number;
  time: string;
}

export interface UserItem {
  userName: string;
}

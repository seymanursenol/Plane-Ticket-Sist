
export interface Rezervation {
  UserName: string;
  Phone: number;
  Email: string;
  CardName: string;
  CardNumber: number;
  ExpirationMonth: number;
  ExpirationYear: number;
  Cvc: number;
}


export interface OrderItem{
  id?: number;
  orderId: number;
  PlanesId: number;
  Price: DoubleRange;
}

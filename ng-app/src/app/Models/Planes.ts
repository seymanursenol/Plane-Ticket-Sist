
export class Planes {
  id?: number;
  outgoing: string;
  incoming: string;
  time: string;
  ticketTotal: number;
  price: number;
  planeState: EnumPlaneState;
}
export interface IPlane{
  id?: number;
  outgoing: string;
  incoming: string;
  time: Date;
  ticketTotal: number;
  price: number;
}
export enum EnumPlaneState{
  waiting=0,
  cancel=1
}
export interface IPlaneId{
Id?: number;

}

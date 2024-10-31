import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPlaneId, Planes } from '../Models/Planes';
import { HttpClient } from '@angular/common/http';
import { Rezervation } from '../Models/Rezervation';

@Injectable({
  providedIn: 'root'
})
export class PlaneService {

  private apiUrl = 'http://localhost:5177/api/Planes/';

  constructor(private http: HttpClient) { }

  getPlanes(): Observable<Planes[]> {
    return this.http.get<Planes[]>(this.apiUrl);
  }

  createPlane(plane: Planes) {
    return this.http.post(this.apiUrl + "Add_Plane", plane);
  }

  updatePlane(plane: Planes): Observable<any> {
    return this.http.put(this.apiUrl + plane.id, plane);
  }

  getPlaneById(id: number): Observable<Planes> {
    return this.http.get<Planes>(`${this.apiUrl}${id}`);
  }
  getPlaneDelete(id: number): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}cancel/${id}`, { planeState: 'cancel' });
  }
  getPlanesByCities(outgoing: string, incoming: string):
  Observable<Planes[]> {
    return this.http.get<Planes[]>(`${this.apiUrl}filter?outgoing=${outgoing}&incoming=${incoming}`);
  }

  AddRez(rez: number) {
    const token = localStorage.getItem('token');
    console.log("rez",rez);
    return this.http.post(this.apiUrl + "AddRezervation", {planeID: rez}, {
      headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
      }
  });
  }
}

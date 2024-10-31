import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, Subject, tap } from 'rxjs';
import { ILoginResponse, IUserLogin, User } from '../Models/User';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + 'Auth';
  isAuthenticated: boolean= false;

  private token = new BehaviorSubject<any>(null);

  private userProfileSubject = new BehaviorSubject<any>(null);
  userProfile$ = this.userProfileSubject.asObservable();

  constructor(private http: HttpClient) {

  }
  ngInOnit(){
    this.isAdmin();
  }


  isAdmin(): boolean {
    const username = localStorage.getItem('username');
    if (username=='admin') {
      return true;
    }
    else{
      return false;
    }
  }

  login(model: IUserLogin): Observable<any> {
    return this.http.post<ILoginResponse>(this.apiUrl + '/login', model).pipe(
      map((response: any) => {
        console.log("Full Response:", response);
        if (response && response.token) {
          localStorage.setItem('token', response.token);
          this.token.next(response.token);
          if (response.userName) {
            localStorage.setItem('username', response.userName);
            this.userProfileSubject.next({
              username: response.userName,
              token: response.token
            });
          } else {
            console.error("Username bulunamadı:", response);
          }

        } else {
          console.error("Token bulunamadı:", response);
        }
      })
    );
  }


  register(model: any): Observable<any> {
    return this.http.post<ILoginResponse>(`${this.apiUrl}/register`, model);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/logout`, {}).pipe(
      map(response => {
        localStorage.removeItem('token');
        this.token.next(null);
        // this.userProfile$.subscribe(profile => {
        //   this.isAuthenticated = false;
        //   console.log("Profil bilgisi: " + this.isAuthenticated);
        // });
        return response;

      })
    );
  }


  getUserName() {
    return this.token.value ? this.token.value.UserName : null;
  }

  isValid(){
    const token = localStorage.getItem('token');
    const username = localStorage.getItem('username');
    if(!!token){
      // token expire olmuş mu?

      var tokenModel = {
        token: token,
        username: username
      };

      this.userProfileSubject.next(tokenModel);
      console.log("token Is Valid",this.userProfileSubject.next(tokenModel));
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Service/auth.service';
import { MessageService } from '../Service/message.service';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';
import { th } from 'date-fns/locale';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isAuthenticated: boolean = false;
  userProfile: any;
  message : string;
  messageColor: string;
  isAdmin: boolean;

  constructor(private router: Router, private authService: AuthService, private messageService: MessageService, private http: HttpClient) {}

  ngOnInit(): void {
    this.messageService.currentMessage.subscribe(messageObj => {
      this.message = messageObj.text;
      this.messageColor = messageObj.color;
    });

    this.authService.userProfile$.subscribe(profile => {
      this.userProfile = profile;
      // console.log("User Profile:", this.userProfile);
    });

    this.authService.userProfile$
    .pipe(
      tap(x=> console.log("navbar user:",x))
    )
    .subscribe(profile => {
      this.userProfile = profile;
      this.isAdmin = this.authService.isAdmin();
      // console.log("navbar" +this.userProfile.username);

    });
    this.LoggenAdmin;

  }

  logout() {
    this.authService.logout().subscribe(() => {

      this.router.navigate(['/']); // Çıkış yaptıktan sonra yönlendirme
      this.userProfile = null;
      this.isAdmin = false;
    });
  }

  get LoggenAdmin():boolean{
    return this.authService.isAdmin();

  }
}

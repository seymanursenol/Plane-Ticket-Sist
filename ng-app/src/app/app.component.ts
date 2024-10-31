import { Component, OnInit } from '@angular/core';
import { AuthService } from './Service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
messages: any;
userProfile: any;
  constructor(private authService: AuthService) {}

  ngOnInit() {
    // Uygulama başlatıldığında kullanıcı bilgisini yükle
    this.authService.isValid();
    // this.authService.isAdmin();
  }
}

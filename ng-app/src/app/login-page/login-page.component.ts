import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../Service/auth.service';
import { Route, Router } from '@angular/router';
import { IUserLogin, User } from '../Models/User';

@Component({
  selector: 'login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  @Output() loginSuccess = new EventEmitter<void>();

  model: any={};
  subscription: any;
  isLogin: boolean = true;
  user : IUserLogin[];

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  Login(form: NgForm){
    const _model: IUserLogin={
      Email: this.model.Email,
      Password: this.model.Password
    }
    this.authService.login(_model).subscribe(
      next=> {
        this.router.navigate(['']);
        this.loginSuccess.emit();
        this.isLogin = true;
      }, error => {
        console.log("Başarısız");
        alert("Lütfen bilgilerinizi doğru girin.");
        this.router.navigate(['/login-page']);
      }
    )
  }
}

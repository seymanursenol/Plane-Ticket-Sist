import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../Service/auth.service';
import { IUser, User } from '../Models/User';

@Component({
  selector: 'signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css'],
})
export class SignupPageComponent {

form: NgForm;
user : User[];
model : any = {};


  constructor( private authService: AuthService, private router: Router){}

  ngOnInit() {


  }

  Register(form: NgForm) {
    const _model: User={
      UserName: this.model.UserName,
      Email: this.model.Email,
      Password: this.model.Password
    }
    console.log(_model);

    this.authService.register(_model).subscribe(response => {
      console.log('User registered');
      // console.log('Confirmation URL:', response.ConfirmUrl);
      this.router.navigate(['/login-page']);
    }, error => {
      console.log(error);
    });
  }
}

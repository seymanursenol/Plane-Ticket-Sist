import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { faL } from "@fortawesome/free-solid-svg-icons";
import { map, Observable, tap } from "rxjs";
import { AuthService } from "../Service/auth.service";
@Injectable({providedIn: 'root'})
export class AdminGuard implements CanActivate{
  constructor(private authService: AuthService, private router: Router){}

  canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot):boolean | Observable<boolean|UrlTree> | Promise<boolean | UrlTree> {

    return this.authService.userProfile$.pipe(
      tap(x=> console.log("auth guard new  user:", x)),
      map(user=>{
        console.log("adminguard: " + user.username,);
        return !!user && (user.username || user.username)== "admin"
      }),
      tap(isAdmin=>{
        if(!isAdmin){
          this.router.navigate(["login-page"]);
        }
      })
    );
  }
}

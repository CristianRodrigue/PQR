import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private auth: AuthService) { }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
      if (this.auth.isAuthenticated()) {
      
        console.log('usuario logueado');
        // TODO: Validar
        // var mySession = JSON.parse(sessionStorage.getItem('session')!);
        // let idBusiness = mySession.businessId;
        
        return true;
      }
      else {
        console.log('usuario no logueado');
        this.router.navigateByUrl('/home');
        return false;
      }


  }
  
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = environment.URL_API;
  public accessToken: any;

  constructor(private http: HttpClient) { }

  login(userEmail: string, password: string) {

    this.logout();

    const authData = {
      email: userEmail,
      password: password,
      returnSecureToken: true
    };

    return this.http.post(
      this.url + '/Auth/loginPQR',
      authData
    ).pipe(
      map((result: any) => {
        
        if (result.status === 'Ok') {
          console.log('Login Ok');
          sessionStorage.setItem('token', JSON.stringify(result.token));
        }

        return result;
      })
    );

  }

  logout() {
    sessionStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    if (sessionStorage.getItem('token')) {
      this.accessToken = sessionStorage.getItem('token'); // TODO: Revisar para que se usa esta variable
      return true;
    }
    return false;
  }



}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserModel } from '../models/user.model';

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

  getAll() {
    return this.http.get(this.url + '/Auth/GetAll');
  }

  getById(id: string): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(this.url + '/Auth/GetById/' + id);
  }


  create(nombre: string, email: string, password: string, ) {
    
    const data = {
      Name: nombre,
      Email: email,
      Password: password,
      //Role: null,
    };

    return this.http.post(`${this.url}/Auth/Create`, data);
  }



  delete(Id: string) {
    return this.http.delete(this.url + '/Auth/Delete/' + Id);
  }

  // Validadores

  getEmailValidator(Email: string) {
     
    //let data = this.http.get(this.url + '/Auth/EmailValidator/' + Email);
    //console.log('data email', data);

    return this.http.get(this.url + '/Auth/EmailValidator/' + Email);
  }



}

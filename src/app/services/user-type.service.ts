import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserTypeService {

  private url = environment.URL_API;

  constructor(private http: HttpClient) { }

  getAll() {

    return this.http.get(this.url + '/UserType/GetAll');

  }
}

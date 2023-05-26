import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PQRModel } from '../models/pqr.model';

@Injectable({
  providedIn: 'root'
})
export class PqrService {

  private url = environment.URL_API;

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url + '/Pqr/GetAll');
  }

  createPQR(country: string) {

    const data = {
      CountryId: country,
      Comentario: 'No',  
      Email: 'No', 
      Name: 'No',
      PhoneNumber: 'No',
      RazonSocial: 'No'
    };

    return this.http.post(`${this.url}/Pqr/CreatePQR`, data);
  }

}

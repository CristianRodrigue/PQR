import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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

  getById(id: string): Observable<PQRModel[]> {
    return this.http.get<PQRModel[]>(this.url + '/Pqr/GetById/' + id);
  }

  
  createPQR(country: string, comentario: string, email:string, name:string,telefono:string,razonSocial:string,caseType:string,userType:string,nit:string,cedula:string,) {

    const data = {
      CountryId: country,
      CaseTypeId:caseType,
      UserTypeId:userType,
      Nit:nit,
      Cedula:cedula,
      Comentario: comentario,  
      Email: email, 
      Name: name,
      PhoneNumber: telefono,
      RazonSocial: razonSocial,
      
    };

    return this.http.post(`${this.url}/Pqr/CreatePQR`, data);
  }

}

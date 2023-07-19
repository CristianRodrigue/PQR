import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PQRModel } from '../models/pqr.model';
import { Byte } from "@angular/compiler/src/util";

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
    return this.http.get<PQRModel[]> (this.url + '/Pqr/GetById/' + id)
  }

  update(id: string, caseStatus: PQRModel): Observable<any> {
    return this.http.put<any>(`${this.url}/Pqr/Update/${id}`, caseStatus);
  }

  
  createPQR(country: string, comentario: string, email:string, name: string, telefono: string, razonSocial: string, caseType: string, userType: string, nit: string, cedula: string, autorizo: boolean, file: any) {

console.log('file: ', file);


    let requestImage: any = {};

    const data: any = {
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
      autorizaTratamientoDatos: autorizo,
      
    };

    if (file !== null) {
      requestImage = {
        //id: file,
        height: file.height,
        timestamp: file.timestamp,
        uri: file.uri,
        fileName: file.fileName,
        data: file.data
      };

      data['File'] = requestImage;
    }

    console.log('data:', data);

    return this.http.post(`${this.url}/Pqr/CreatePQR`, data);
  }

}

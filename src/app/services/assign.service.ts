import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AssignModel } from '../models/assign.model';

@Injectable({
  providedIn: 'root'
})
export class AssignService {

  private url = environment.URL_API;

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url + '/Assign/GetAll');
  }

  getById(id: string): Observable<AssignModel[]> {
    return this.http.get<AssignModel[]>(this.url + '/Assign/GetById/' + id);
  }

  create(name:string, mailTemplate:string, employee:string) {

    const data = {    
      Name: name,
      IdMailTemplate: mailTemplate,
      IdEmployee: employee
    };

    return this.http.post(`${this.url}/Assign/Create`, data);
  }

  update(name:string, mailTemplate:string, employee:string) {

    const data = {    
      Name: name,
      IdMailTemplate: mailTemplate,
      IdEmployee: employee
    };

    return this.http.put(`${this.url}/Assign/Update`, data);
  }

  delete(Id: string) {
    return this.http.delete(this.url + '/Assign/Delete/' + Id);
  }
}

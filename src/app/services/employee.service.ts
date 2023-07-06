import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EmployeeModel } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private url = environment.URL_API;

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url + '/Employee/GetAll');
  }

  getById(id: string): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>(this.url + '/Employee/GetById/' + id);
  }

  create(name:string, email:string, division:string, cargo:string) {

    const data = {    
      Name: name,
      Email: email,
      Division: division,
      Cargo: cargo
    };

    return this.http.post(`${this.url}/Employee/Create`, data);
  }

  update(name:string, email:string, division:string, cargo:string) {

    const data = {    
      Name: name,
      Email: email,
      Division: division,
      Cargo: cargo
    };

    return this.http.put(`${this.url}/Employee/Update`, data);
  }

  delete(Id: string) {
    return this.http.delete(this.url + '/Employee/Delete/' + Id);
  }

}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MailModel } from '../models/mail.model';

@Injectable({
  providedIn: 'root'
})
export class CreateMessageService {

  private url = environment.URL_API;

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url + '/MailTemplate/GetAll');
  }

  getById(id: string): Observable<MailModel[]> {
    return this.http.get<any>(this.url + '/MailTemplate/GetById/' + id).pipe(
      map((response: { data: MailModel[]; }) => response.data as MailModel[])
    );
  }

  createMail(name:string,description:string,html:string){
    const data={
      Name:name,
      Description:description,
      Html:html,
      
    };

    return this.http.post(`${this.url}/MailTemplate/Create`, data);
  }

  updateMessage(id: string, message: MailModel): Observable<any> {
    return this.http.put<any>(`${this.url}/MailTemplate/Update/${id}`, message);
  }
  

}

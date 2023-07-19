import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FileModel } from '../models/file.model';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  private url = environment.URL_API;

  constructor(private http: HttpClient) { }

  getById(id: string): Observable<any> {
    
    return this.http.get (this.url + '/File/GetById/' + id,{ responseType: 'blob'})
  }
}

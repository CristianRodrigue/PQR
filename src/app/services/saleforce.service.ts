import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SaleforceService {

  private url = environment.URL_API;
  
  constructor(private http: HttpClient) { }

  query(id: string): Observable<boolean> {
    
    return this.http.get<boolean> (this.url + '/Neo/QuerySalesforce/' + id)
  }
}

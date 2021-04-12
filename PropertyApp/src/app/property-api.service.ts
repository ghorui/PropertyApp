import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Property } from '../app/property/property';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PropertyApiService implements OnInit {

  properties: any;

  constructor(private httpClient: HttpClient) {  }

  ngOnInit() {  }

  GetProperties(): Observable<Property[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, DELETE, PUT',
        'Access-Control-Allow-Origin': '*'
        })
      };

      return this.httpClient.request<Property[]>('GET', 'https://localhost:44384/api/Properties',  httpOptions );
  }

  SaveProperties(event: any, row: any){
    event.target.disabled = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, DELETE, PUT',
        'Access-Control-Allow-Origin': '*'
        })
      };

    this.httpClient.post('https://localhost:44384/api/Properties',row, httpOptions).subscribe(
      () =>{
        alert("Record saved successfully!");
        location.reload();
      }
    );
}

}

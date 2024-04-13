import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment as env } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private endpoint = "Flight/searchFlights/"
  constructor(private http: HttpClient) { }

  searchFlights(origin: string, destination: string, currency: string, typeJourney: string): Observable<any> {
    let params = new HttpParams()
      .set('origin', origin)
      .set('destination', destination)
      .set('currency', currency)
      .set('typeJourney', typeJourney);
  
    return this.http.get(`${env.url_api}/${env.api}/${this.endpoint}`, { params: params });
  }
}

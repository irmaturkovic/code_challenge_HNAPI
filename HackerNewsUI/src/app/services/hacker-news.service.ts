import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class HackerNewsService {
  private baseUrl = "https://localhost:7270/api/";

  constructor(private http: HttpClient) { }

  getNewestStories(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}HackerNewsItems?type=newstories`);
  }
}

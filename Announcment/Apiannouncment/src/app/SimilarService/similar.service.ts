import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class SimilarService {
  private ApiUrl = 'https://localhost:7167/Announcment/announcements';
  constructor(private http: HttpClient) { }
  
  getAllSimilarAnnouncements(id: number): Observable<any> {
    const url = `${this.ApiUrl}/${id}/similar`;
    return this.http.get<any>(url);
  }
}

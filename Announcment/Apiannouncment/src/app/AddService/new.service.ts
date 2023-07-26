import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';



@Injectable({
  providedIn: 'root'
})
export class NewService {
  private apiUrl = 'https://localhost:7167/Announcment/announcements';

  constructor(private http: HttpClient) {}


  createAnnouncement(announcement: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, announcement);
  }
}

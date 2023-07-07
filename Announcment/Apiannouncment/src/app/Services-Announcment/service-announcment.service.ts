import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class ServiceAnnouncmentService {
 private ApiUrl = 'https://localhost:44394/Announcment/announcements'


 constructor(private http: HttpClient) { }

 getIDRange(): Observable<any> {
  const url = `https://localhost:44394/Announcment/id-range`;
  return this.http.get(url);
}
 getAnnouncements(): Observable<any> {
   return this.http.get<any>(`${this.ApiUrl}`);
 }
 deleteAnnouncement(id: number) {
  const url = `${this.ApiUrl}/${id}`;
  return this.http.delete(url);
}
getbyid(id: number): Observable<any> {
  const url = `https://localhost:44394/Announcment/announcements/${id}`;
  return this.http.get(url);
}
}

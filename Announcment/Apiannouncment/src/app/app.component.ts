import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit{
  title = 'Apiannouncment';
   announcmentId:number = 0;
  announcment:any;
  constructor (private http:HttpClient,private router:Router){}
  ngOnInit(): void {
    this.http.get('https://localhost:4002/api/announcment').subscribe;
  }

  refreshPage(): void {
    this.router.navigateByUrl('https://localhost:4002/api/announcment', { skipLocationChange: true }).then(() => {
      this.router.navigate([this.router.url]);
    });
  }
  
}

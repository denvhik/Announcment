import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { SimilarService } from 'src/app/SimilarService/similar.service';
@Component({
  selector: 'app-similar',
  templateUrl: './similar.component.html',
  styleUrls: ['./similar.component.css']
})
export class SimilarComponent {
id: number = 0 ;
announcements:any [] = [];
similarAnnouncements: any[] = [];
isIdInputted: boolean = false;
constructor (private http:HttpClient,private similarservice:SimilarService){}

getAllSimilarAnnouncements(id:number) {
  if (this.id !== 0) {
    this.isIdInputted = true;
  this.similarservice.getAllSimilarAnnouncements(id).subscribe(response => {
    console.log(id);
this.similarAnnouncements = response;
console.log(response);
    })
}
}
}

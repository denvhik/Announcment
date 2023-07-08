import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceAnnouncmentService } from 'src/app/Services-Announcment/service-announcment.service';
@Component({
  selector: 'app-announcment',
  templateUrl: './announcment.component.html',
  styleUrls: ['./announcment.component.css']
  
})
export class AnnouncmentComponent  {
  id:number =  0;
  showWarning: boolean = false;
  minId: number = 0;
  maxId: number = 0;
  selectedAnnouncement: any = null;
  editedAnnouncement: any = null;
  announcements: any[] = [];



  constructor(private http: HttpClient,private serviceAnnouncmentService:ServiceAnnouncmentService) 
  {
    
  }
  
  ngOnInit(): void {
   this.serviceAnnouncmentService.getIDRange().subscribe(response => 
    {
      this.maxId = response.maxId;
      this.minId = response.minId;
    }, error => {
      console.error(error);
    });
    this.getAnnouncements();
  };
  
  getAnnouncements() {
    this.http.get('https://localhost:44394/Announcment/announcements').subscribe((response: any) => {
      this.announcements = response;
    });
  }
  deleteAnnouncements(id: number) {
    console.log(id);
    this.http.delete('https://localhost:44394/Announcment/announcements/'+ id).subscribe((response:any ) => {
      if (response !== null) {
        this.announcements = response;
      }
      this.getAnnouncements();
    });
  }
  getbyid() {
    if (this.id && this.id >= this.minId && this.id <= this.maxId) {
      this.serviceAnnouncmentService.getbyid(this.id)
        .subscribe(response => {
         this.announcements = [response];
          console.log(response);
        }, error => {
          // Обробка помилки
          console.error(error);
          this.showWarning = true;
        });
    } else {
      this.showWarning = true;
    }
  }

  dismissWarning() {
    this.showWarning = false;
  }

  updateAnnouncement(announcement: any) {
    this.selectedAnnouncement = announcement;
    this.editedAnnouncement = { ...announcement };
    console.log(announcement);
  }

  saveAnnouncement() {
    this.serviceAnnouncmentService.updateAnnouncement(this.selectedAnnouncement.id, this.editedAnnouncement).subscribe(
      () => {
        this.selectedAnnouncement = null;
        this.editedAnnouncement = null;
        this.getAnnouncements();
      },
      (error) => {
        console.error('Failed to update announcement', error);
      }
    );
  }
  cancelEdit() {
    this.selectedAnnouncement = null;
    this.editedAnnouncement = null;
  }

}


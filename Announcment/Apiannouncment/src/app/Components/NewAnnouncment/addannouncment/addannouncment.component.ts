import { Component } from '@angular/core';
import {NewService} from 'src/app/AddService/new.service';
@Component({
  selector: 'app-addannouncment',
  templateUrl: './addannouncment.component.html',
  styleUrls: ['./addannouncment.component.css']
})
export class AddannouncmentComponent {
  announcement: any = {
    title: '',
    description: ''
  };
  showSuccessMessage: boolean = false;

  constructor(private newService:NewService) 
  {
    
  }
  createAnnouncement() {
    this.newService.createAnnouncement(this.announcement).subscribe(() => {
      this.showSuccessMessage = true;
      this.resetForm();
      setTimeout(() => {
        this.showSuccessMessage = false;
      }, 3000);
    }, error => {
      console.error('Error creating announcement:', error);
    });
  }

  resetForm() {
    this.announcement.title = '';
    this.announcement.description = '';
  }
}



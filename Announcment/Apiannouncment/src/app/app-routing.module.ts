import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AnnouncmentComponent } from './Components/announcment/announcment.component';


const routes: Routes = [
  {
    path: '',
    component:AnnouncmentComponent
  },
  {
    path: 'Announcment',
    component:AnnouncmentComponent
  },
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

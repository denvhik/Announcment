import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AnnouncmentComponent } from './Components/announcment/announcment.component';
import { SimilarComponent } from './Components/getSimilar/similar/similar.component';
import { AddannouncmentComponent } from './Components/NewAnnouncment/addannouncment/addannouncment.component';
const routes: Routes = [
  {
    path: '',
    component:AnnouncmentComponent
  },
  {
    path: 'Announcment',
    component:AnnouncmentComponent
  },
  {
    path: 'Similar',
    component:SimilarComponent

  },
  
  {
    path: 'New',
    component:AddannouncmentComponent
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

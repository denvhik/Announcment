import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AnnouncmentComponent } from './Components/announcment/announcment.component';
import { SimilarComponent } from './Components/getSimilar/similar/similar.component';
import { AddannouncmentComponent } from './Components/NewAnnouncment/addannouncment/addannouncment.component';

@NgModule({
  declarations: [
    AppComponent,
    AnnouncmentComponent,
    SimilarComponent,
    AddannouncmentComponent,

  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

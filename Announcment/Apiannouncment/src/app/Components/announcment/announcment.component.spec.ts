import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AnnouncmentComponent } from './announcment.component';
import { FormsModule } from '@angular/forms';

describe('AnnouncmentComponent', () => {
  let component: AnnouncmentComponent;
  let fixture: ComponentFixture<AnnouncmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule
      ],
      declarations: [AnnouncmentComponent]
    });
    fixture = TestBed.createComponent(AnnouncmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

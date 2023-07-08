import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddannouncmentComponent } from './addannouncment.component';

describe('AddannouncmentComponent', () => {
  let component: AddannouncmentComponent;
  let fixture: ComponentFixture<AddannouncmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddannouncmentComponent]
    });
    fixture = TestBed.createComponent(AddannouncmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

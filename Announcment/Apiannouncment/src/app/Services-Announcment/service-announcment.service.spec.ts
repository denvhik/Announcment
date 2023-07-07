import { TestBed } from '@angular/core/testing';

import { ServiceAnnouncmentService } from './service-announcment.service';

describe('ServiceAnnouncmentService', () => {
  let service: ServiceAnnouncmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceAnnouncmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

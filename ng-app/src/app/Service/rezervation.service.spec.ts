import { TestBed } from '@angular/core/testing';

import { RezervationService } from './rezervation.service';

describe('RezervationService', () => {
  let service: RezervationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RezervationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

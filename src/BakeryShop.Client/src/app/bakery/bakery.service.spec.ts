import { TestBed } from '@angular/core/testing';

import { BakeryService } from './bakery.service';

describe('BakeryService', () => {
  let service: BakeryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BakeryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { SaleforceService } from './saleforce.service';

describe('SaleforceService', () => {
  let service: SaleforceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SaleforceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CdbCalculationService } from './cdb-calculation.service';

describe('CdbCalculationService', () => {
  let service: CdbCalculationService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CdbCalculationService]
    });

    service = TestBed.inject(CdbCalculationService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should call API and return calculation result', () => {
    const mockResponse = { grossValue: 1123.08, netWorth: 898.47 };

    service.calculate({ redemptionValue: 1000, termMonths: 12 })
      .subscribe(res => {
        expect(res).toEqual(mockResponse);
      });

    const req = httpMock.expectOne('https://localhost:60357/api/cdb-calculation/calculate');
    expect(req.request.method).toBe('POST');

    req.flush(mockResponse);
  });
});

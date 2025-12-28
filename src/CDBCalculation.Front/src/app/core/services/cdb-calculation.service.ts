import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CdbCalculationRequest } from '../../models/cdb-calculation-request.model';
import { CdbCalculationResponse } from '../../models/cdb-calculation-response.model';

@Injectable({ providedIn: 'root' })
export class CdbCalculationService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private readonly http: HttpClient) { }

  calculate(request: CdbCalculationRequest): Observable<CdbCalculationResponse> {
    return this.http.post<CdbCalculationResponse>(this.apiUrl, request);
  }
}

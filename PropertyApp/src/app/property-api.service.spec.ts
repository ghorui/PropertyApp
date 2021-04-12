import { TestBed } from '@angular/core/testing';

import { PropertyApiService } from './property-api.service';
import { HttpClient } from '@angular/common/http';

describe('PropertyApiService', () => {
  let service: PropertyApiService;
  let httpClient: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PropertyApiService);
    httpClient = TestBed.inject(HttpClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

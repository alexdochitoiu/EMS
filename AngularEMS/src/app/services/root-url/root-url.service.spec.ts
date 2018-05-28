import { TestBed, inject } from '@angular/core/testing';

import { RootUrlService } from './root-url.service';

describe('RootUrlService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RootUrlService]
    });
  });

  it('should be created', inject([RootUrlService], (service: RootUrlService) => {
    expect(service).toBeTruthy();
  }));
});

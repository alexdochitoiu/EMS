import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InfrastructureService {

  public readonly URL = 'http://localhost:61330';
  public readonly TOKEN_KEY = 'auth_token';

  constructor() { }
}

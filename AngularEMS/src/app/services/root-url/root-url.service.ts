import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RootUrlService {

  public readonly URL = 'http://localhost:61330';

  constructor() { }
}

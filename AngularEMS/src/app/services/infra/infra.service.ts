import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InfrastructureService {

  public readonly URL = 'http://localhost:61330';
  public readonly TOKEN_KEY = 'auth_token';
  public readonly EMAIL_KEY = 'auth_email';
  public readonly PHOTO_URL_KEY = 'auth_photo_url';
  public readonly DEFAULT_PHOTO_URL = 'https://image.ibb.co/nkGP0y/default_profile_photo.jpg';
  constructor() { }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InfrastructureService } from '../infra/infra.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  private url: string;

  constructor(private http: HttpClient,
    private infra: InfrastructureService) { }

  public alertNearbyUsers(model: any) {
    this.url = this.infra.URL + '/api/alert/nearby';
    return this.http.post(this.url, model).pipe(map((response: Response) => <any>response));
  }
}

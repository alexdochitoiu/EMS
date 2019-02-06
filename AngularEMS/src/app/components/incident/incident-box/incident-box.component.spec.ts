import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncidentBoxComponent } from './incident-box.component';

describe('IncidentBoxComponent', () => {
  let component: IncidentBoxComponent;
  let fixture: ComponentFixture<IncidentBoxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncidentBoxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncidentBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

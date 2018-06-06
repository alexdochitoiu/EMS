import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnnouncementBoxComponent } from './announcement-box.component';

describe('AnnouncementComponent', () => {
  let component: AnnouncementBoxComponent;
  let fixture: ComponentFixture<AnnouncementBoxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnnouncementBoxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnnouncementBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

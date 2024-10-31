import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RezDetailsComponent } from './rez-details.component';

describe('RezDetailsComponent', () => {
  let component: RezDetailsComponent;
  let fixture: ComponentFixture<RezDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RezDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RezDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllRezerveComponent } from './all-rezerve.component';

describe('AllRezerveComponent', () => {
  let component: AllRezerveComponent;
  let fixture: ComponentFixture<AllRezerveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllRezerveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllRezerveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

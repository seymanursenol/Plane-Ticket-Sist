import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaneUpdateComponent } from './plane-update.component';

describe('PlaneUpdateComponent', () => {
  let component: PlaneUpdateComponent;
  let fixture: ComponentFixture<PlaneUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlaneUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlaneUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

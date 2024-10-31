import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RezerveComponent } from './rezerve.component';

describe('RezerveComponent', () => {
  let component: RezerveComponent;
  let fixture: ComponentFixture<RezerveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RezerveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RezerveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

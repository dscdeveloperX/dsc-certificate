import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PruebaAComponent } from './prueba-a.component';

describe('PruebaAComponent', () => {
  let component: PruebaAComponent;
  let fixture: ComponentFixture<PruebaAComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [PruebaAComponent]
    });
    fixture = TestBed.createComponent(PruebaAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

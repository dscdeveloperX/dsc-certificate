import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArrancarComponent } from './arrancar.component';

describe('ArrancarComponent', () => {
  let component: ArrancarComponent;
  let fixture: ComponentFixture<ArrancarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ArrancarComponent]
    });
    fixture = TestBed.createComponent(ArrancarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

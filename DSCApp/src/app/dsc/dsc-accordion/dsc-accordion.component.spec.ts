import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DscAccordionComponent } from './dsc-accordion.component';

describe('DscAccordionComponent', () => {
  let component: DscAccordionComponent;
  let fixture: ComponentFixture<DscAccordionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [DscAccordionComponent]
    });
    fixture = TestBed.createComponent(DscAccordionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

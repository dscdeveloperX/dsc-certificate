import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DscAccordionItemComponent } from './dsc-accordion-item.component';

describe('DscAccordionItemComponent', () => {
  let component: DscAccordionItemComponent;
  let fixture: ComponentFixture<DscAccordionItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [DscAccordionItemComponent]
    });
    fixture = TestBed.createComponent(DscAccordionItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

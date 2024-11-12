import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcordionPanelComponent } from './acordion-panel.component';

describe('AcordionPanelComponent', () => {
  let component: AcordionPanelComponent;
  let fixture: ComponentFixture<AcordionPanelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [AcordionPanelComponent]
    });
    fixture = TestBed.createComponent(AcordionPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

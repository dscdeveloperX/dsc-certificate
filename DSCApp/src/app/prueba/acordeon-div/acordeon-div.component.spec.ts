import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcordeonDivComponent } from './acordeon-div.component';

describe('AcordeonDivComponent', () => {
  let component: AcordeonDivComponent;
  let fixture: ComponentFixture<AcordeonDivComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [AcordeonDivComponent]
    });
    fixture = TestBed.createComponent(AcordeonDivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

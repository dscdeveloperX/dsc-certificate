import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IframePdfComponent } from './iframe-pdf.component';

describe('IframePdfComponent', () => {
  let component: IframePdfComponent;
  let fixture: ComponentFixture<IframePdfComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [IframePdfComponent]
    });
    fixture = TestBed.createComponent(IframePdfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

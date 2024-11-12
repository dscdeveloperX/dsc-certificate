import { Component, Input, ChangeDetectionStrategy, signal, WritableSignal, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-dsc-accordion-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dsc-accordion-item.component.html',
  styleUrls: ['./dsc-accordion-item.component.css']
})
export class DscAccordionItemComponent implements OnInit, OnDestroy {
  
  
  @Input({required:true}) public Title!:string;
  @Input({required:true}) public Icon!:string;
  @Input({required:true}) public Name!:string;
  @Input({required:false}) public Active:boolean=false;
  public onOff!:boolean;
  @Output() public Click:EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {
    this.onOff = this.Active;
  }

  public OnClick():void{
    //this.Active = !this.Active;
    this.onOff =  !this.onOff;
    this.Click.emit(this.Name);
  }

  public Open():void{
    this.onOff =  true;
    //this.Active = true;
  }

  public Close():void{
    this.onOff =  false;
    //this.Active = false;
  }

  ngOnDestroy(): void {
    alert('destroy');
    this.Click.unsubscribe();
  }

}

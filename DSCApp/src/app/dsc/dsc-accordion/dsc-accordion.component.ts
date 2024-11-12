import { AfterViewChecked, AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChildren, OnInit, QueryList, ViewChildren, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscAccordionItemComponent } from '../dsc-accordion-item/dsc-accordion-item.component';

@Component({
  selector: 'app-dsc-accordion',
  standalone: true,
  imports: [CommonModule, DscAccordionItemComponent],
  templateUrl: './dsc-accordion.component.html',
  styleUrls: ['./dsc-accordion.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DscAccordionComponent implements AfterViewInit, OnInit {
  
  
  @ContentChildren(DscAccordionItemComponent)
  public accordion!:QueryList<DscAccordionItemComponent>;
  private items!:DscAccordionItemComponent[];



  ngAfterViewInit(): void {
    this.items = this.accordion.toArray();
    this.items.forEach((x)=>{
      //suscribir
      /*x.Click.subscribe(x=>{
        this.Refresh(x);
      });*/
      x.Close();
      if(x.Active){
        x.Open();
      }
    });
   
  }

  public Refresh(name:string):void{
    this.items = this.accordion.toArray();
    this.items.forEach((x)=>{
       
      //x.Close();
      if(x.Name != name ){
        if(x.onOff){
          x.Close();
        }
      }else{
        if(x.onOff){
        x.Open();
        }else{
          x.Close();
        }
      }
    });
  }

  ngOnInit(): void {
    //alert('init');
    /*//convertimos querylist en array
    const items:DscAccordionItemComponent[] = this.accordion.toArray();
    //
    items.forEach((x)=>{
      console.log(x.Name + ' | ' + x.Active );
      x.Close();
      if(x.Active){
        x.Open();
      }
    });*/

  }

  



}

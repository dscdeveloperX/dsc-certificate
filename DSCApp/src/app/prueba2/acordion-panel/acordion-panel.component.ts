import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscAccordionComponent } from 'src/app/dsc/dsc-accordion/dsc-accordion.component';
import { DscAccordionItemComponent } from 'src/app/dsc/dsc-accordion-item/dsc-accordion-item.component';

@Component({
  selector: 'app-acordion-panel',
  standalone: true,
  imports: [CommonModule, DscAccordionComponent, DscAccordionItemComponent],
  templateUrl: './acordion-panel.component.html',
  styleUrls: ['./acordion-panel.component.css']
})
export class AcordionPanelComponent{
@ViewChild('acordion1') public acordion1!:DscAccordionComponent;



  ngOnInit(): void {
    this.OnGenerar();
  }

  public Data:any=[];



  public OnClickAcordion1(name:string){
    this.acordion1.Refresh(name);
  }

  
  public OnGenerar(){
    this.Data = [
      {
        active:true,
        name:'a',
        title:'titulo - 1',
        icon:'photo/foto1.jpg'
      },
      {
        active:false,
        name:'b',
        title:'titulo - 2',
        icon:'photo/foto2.jpg'
      },
      {
        active:false,
        name:'c',
        title:'titulo - 3',
        icon:'photo/foto3.jpg'
      },
      {
        active:false,
        name:'d',
        title:'titulo - 4',
        icon:'photo/foto1.jpg'
      },
      {
        active:false,
        name:'e',
        title:'titulo - 5',
        icon:'photo/foto2.jpg'
      },
      {
        active:false,
        name:'f',
        title:'titulo - 6',
        icon:'photo/foto3.jpg'
      },
      {
        active:false,
        name:'g',
        title:'titulo - 7',
        icon:'photo/foto1.jpg'
      },
      {
        active:false,
        name:'h',
        title:'titulo - 8',
        icon:'photo/foto2.jpg'
      },
      {
        active:false,
        name:'i',
        title:'titulo - 9',
        icon:'photo/foto3.jpg'
      }
    ];
  }


  public OnGenerar2(){
    this.Data = [
      {
        active:false,
        name:'b',
        title:'titulo - 2',
        icon:'photo/foto2.jpg'
      },
      {
        active:true,
        name:'c',
        title:'titulo - 3',
        icon:'photo/foto3.jpg'
      }
    ];
  }

}

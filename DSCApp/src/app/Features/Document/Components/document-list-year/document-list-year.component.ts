import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';

@Component({
  selector: 'app-document-list-year',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './document-list-year.component.html',
  styleUrls: ['./document-list-year.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentListYearComponent  implements OnInit {
  
  
  /********************************************************************
  variables de entrada y salida */
  public Data:WritableSignal<string[]|undefined> = signal<string[]|undefined>(undefined);
  private DataYear!:string;
  //public Data:WritableSignal<IDocumentType[]|undefined> = signal<IDocumentType[]|undefined>(undefined);
  //modal id
  //private id:string="modal-type";
  private id:string="modal-year";
  //*******************************************************************
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  

  
  ngOnInit(): void {
    this.ReciveData();
  }
  
  /***********************************************************************
  servicios
  ***********************************************************************/
    //modal (recibe data de origen)
    private ReciveData():void{
      this.dscModalService.dataIn(this.id)?.subscribe(
      (x)=>{
        console.log(x.data);
        this.Data.set(x.data);
        
      }
      );
      }
    
    public Onclick(item:string){
      this.DataYear = item;
      this.closeModal();
    }
  //modal (cierra y envia data a origen)
    public closeModal() {
    this.dscModalService.close(this.id,{data:this.DataYear});
    }


  //

/****************************************************************************
metodos
****************************************************************************/



}

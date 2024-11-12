import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';


@Component({
  selector: 'app-document-list-type',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './document-list-type.component.html',
  styleUrls: ['./document-list-type.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentListTypeComponent  implements OnInit {
  
  
  /********************************************************************
  variables de entrada y salida */
  public Data:WritableSignal<IDocumentType[]|undefined> = signal<IDocumentType[]|undefined>(undefined);
  private DataType!:IDocumentType;
  //public Data:WritableSignal<IDocumentType[]|undefined> = signal<IDocumentType[]|undefined>(undefined);
  //modal id
  //private id:string="modal-type";
  private id:string="modal-type";
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
    
    public Onclick(item:IDocumentType){
      this.DataType = item;
      this.closeModal();
    }
  //modal (cierra y envia data a origen)
    public closeModal() {
    this.dscModalService.close(this.id,{data:this.DataType});
    }


  //

/****************************************************************************
metodos
****************************************************************************/



}

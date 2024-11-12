import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';
import { DocumentTypeApiService } from 'src/app/Core/Services/document-type-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-document-type-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './document-type-delete.component.html',
  styleUrls: ['./document-type-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentTypeDeleteComponent implements OnInit  {

  /********************************************************************
    variables de entrada y salida */
    public Data:WritableSignal<IDocumentType|undefined> = signal<IDocumentType|undefined>(undefined);
    //modal id
    private id:string="modal-delete";
    /********************************************************************
    inject*/
    private documentTypeApiService = inject(DocumentTypeApiService);
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
      private ReciveData(){
        this.dscModalService.dataIn(this.id)?.subscribe(
        (x)=>{
          this.Data.set(x.data);
          
        }
        );
        }
      
    //modal (cierra y envia data a origen)
      public closeModal() {
      this.dscModalService.close(this.id,{data:'action-delete'});
      }
  
  
    public OnDocumentTypeDelete():void{
      this.documentTypeApiService.DocumentTypeDelete(this.Data()!.DocumentTypeID!).subscribe(
      {
        next:(data:IDataResponse<IDocumentType>)=>{
          if(data.ErrorCodigo == 0){//State
            //asigno data reseteada
            this.Data.set({
              DocumentTypeID:'',
              DocumentTypeDescription:'',
              DocumentTypeActive:false
            });
            this.closeModal();
          }
        }
      }
    );
  }
  


   



}

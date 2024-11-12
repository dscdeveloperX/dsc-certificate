import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IParameter } from 'src/app/Core/Models/Entity/IParameter';
import { ParameterApiService } from 'src/app/Core/Services/parameter-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-parameter-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './parameter-delete.component.html',
  styleUrls: ['./parameter-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ParameterDeleteComponent implements OnInit {
  
  
  /********************************************************************
  variables de entrada y salida */
  public Data:WritableSignal<IParameter|undefined> = signal<IParameter|undefined>(undefined);
  //modal id
  private id:string="modal-delete";
  /********************************************************************
  inject*/
  private parameterApiService = inject(ParameterApiService);
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


  public OnparameterDelete():void{
  this.parameterApiService.ParameterDelete(this.Data()!.ParameterID!).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//State
          //asigno data reseteada
          this.Data.set({
            ParameterID:0,
            CompanyID:'',
            ParameterName:'',
            ParameterValue:'',
            ParameterType:'',
            ParameterActive:true
          });
          this.closeModal();
        }
      }
    }
  );
}

/****************************************************************************
metodos
****************************************************************************/



}

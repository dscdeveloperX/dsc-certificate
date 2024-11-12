import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IOccupation } from 'src/app/Core/Models/Entity/IOccupation';
import { OccupationApiService } from 'src/app/Core/Services/occupation-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-occupation-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './occupation-delete.component.html',
  styleUrls: ['./occupation-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class OccupationDeleteComponent implements OnInit  {

  /********************************************************************
    variables de entrada y salida */
    public Data:WritableSignal<IOccupation|undefined> = signal<IOccupation|undefined>(undefined);
    //modal id
    private id:string="modal-delete";
    /********************************************************************
    inject*/
    private occupationApiService = inject(OccupationApiService);
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
  

    
  
    public OnOccupationDelete():void{
      this.occupationApiService.OccupationDelete(this.Data()!.OccupationID!).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          if(data.ErrorCodigo == 0){//State
            //asigno data reseteada
            this.Data.set({
              OccupationID:'',
              OccupationDescription:'',
              OccupationActive:false
            });
            this.closeModal();
          }
        }
      }
    );
  }
  



}

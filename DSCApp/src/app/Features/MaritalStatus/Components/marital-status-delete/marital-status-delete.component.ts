import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IMaritalStatus } from 'src/app/Core/Models/Entity/IMaritalStatus';
import { MaritalStatusApiService } from 'src/app/Core/Services/marital-status-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-marital-status-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './marital-status-delete.component.html',
  styleUrls: ['./marital-status-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class MaritalStatusDeleteComponent implements OnInit  {

  /********************************************************************
    variables de entrada y salida */
    public Data:WritableSignal<IMaritalStatus|undefined> = signal<IMaritalStatus|undefined>(undefined);
    //modal id
    private id:string="modal-delete";
    /********************************************************************
    inject*/
    private maritalStatusApiService = inject(MaritalStatusApiService);
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
  
      public OnMaritalStatusDelete():void{
        this.maritalStatusApiService.MaritalStatusDelete(this.Data()!.MaritalStatusID!).subscribe(
          {
            next:(data:IDataResponse<any>)=>{
              if(data.ErrorCodigo == 0){//State
                //asigno data reseteada
                this.Data.set({
                  MaritalStatusID:'',
                  MaritalStatusDescription:'',
                  MaritalStatusActive:false
                });
                this.closeModal();
              }
            }
          }
        );
      }
  
  
  




}

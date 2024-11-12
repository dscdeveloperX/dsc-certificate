import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IEmployee } from 'src/app/Core/Models/Entity/IEmployee';
import { EmployeeApiService } from 'src/app/Core/Services/employee-api.service';
import { urlHostBase } from 'src/app/Core/Constans/api-params';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-employee-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee-delete.component.html',
  styleUrls: ['./employee-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class EmployeeDeleteComponent implements OnInit {
  
  
  /********************************************************************
  variables de entrada y salida */
  public Data:WritableSignal<IEmployee|undefined> = signal<IEmployee|undefined>(undefined);
  //modal id
  private id:string="modal-delete";
  /********************************************************************
  inject*/
  private employeeApiService = inject(EmployeeApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  private CompanyID!:number;
  public Host:string = urlHostBase;

  
  ngOnInit(): void {
    this.ReciveData();
    //session compania
    this.CompanyID = 1;
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


 

  public OnEmployeeDelete():void{
  this.employeeApiService.EmployeeDelete(this.CompanyID, this.Data()!.EmployeeID!).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//State
          //asigno data reseteada
          this.Data.set({
            CompanyID:0,
            PersonID:'',
            EmployeeDateEntry:new Date()
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



  /************************************************************************
   * **********************************************************************
   * **********************************************************************
   * **********************************************************************
   * **********************************************************************
  */
  

  
}

import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { ICompany } from 'src/app/Core/Models/Entity/ICompany';
import { CompanyApiService } from 'src/app/Core/Services/company-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-company-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-delete.component.html',
  styleUrls: ['./company-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CompanyDeleteComponent implements OnInit {
  
  
  /********************************************************************
  variables de entrada y salida */
  public Data:WritableSignal<ICompany|undefined> = signal<ICompany|undefined>(undefined);
  //modal id
  private id:string="modal-delete";
  /********************************************************************
  inject*/
  private companyApiService = inject(CompanyApiService);
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


  public OnCompanyDelete():void{
  this.companyApiService.CompanyDelete(this.Data()!.CompanyID!).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//state
          //asigno data reseteada
          this.Data.set({
            CompanyID:0,
            CompanyRuc:'',
            ProvinceID:0,
            ProvinceName:'',
            CityID:0,
            CityName:'',
            CompanyName:'',
            CompanyAddress:'',
            CompanyPhone:'',
            CompanyActive:false
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

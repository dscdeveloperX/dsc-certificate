import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IGender } from 'src/app/Core/Models/Entity/IGender';
import { GenderApiService } from 'src/app/Core/Services/gender-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-gender-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './gender-delete.component.html',
  styleUrls: ['./gender-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class GenderDeleteComponent implements OnInit  {

/********************************************************************
  variables de entrada y salida */
  public Data:WritableSignal<IGender|undefined> = signal<IGender|undefined>(undefined);
  //modal id
  private id:string="modal-delete";
  /********************************************************************
  inject*/
  private genderApiService = inject(GenderApiService);
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

    

  public OnGenderDelete():void{
  this.genderApiService.GenderDelete(this.Data()!.GenderID!).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//State
          //asigno data reseteada
          this.Data.set({
            GenderID:'',
            GenderDescription:'',
            GenderActive:false
          });
          this.closeModal();
        }
      }
    }
  );
}




}

import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IPerson } from 'src/app/Core/Models/Entity/IPerson';
import { PersonApiService } from 'src/app/Core/Services/person-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-person-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './person-delete.component.html',
  styleUrls: ['./person-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class PersonDeleteComponent implements OnInit{

/********************************************************************
  variables de entrada y salida */
  public Data:WritableSignal<IPerson|undefined> = signal<IPerson|undefined>(undefined);
  //modal id
  private id:string="modal-delete";
  /********************************************************************
  inject*/
  private personApiService = inject(PersonApiService);
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


  public OnPersonDelete():void{
  this.personApiService.PersonDelete(this.Data()!.PersonID!).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo== 0){//State
          //asigno data reseteada
          this.Data.set({
            PersonID :'',
            PersonPhoto:'',
            PersonSignatureImage:'',
            CityID :0,
            ProvinceID :0,
            PersonName :'',
            PersonSurname :'',
            PersonDateOfBirth :new Date(),
            PersonPhone :'',
            PersonEmail :'',
            MaritalStatusID :'',
            GenderID :'',
            PersonActive :false
          });
          this.closeModal();
        }
      }
    }
  );
}


  /**********************************************************************
  ********************************************************************
  ********************************************************************
  ********************************************************************
  ********************************************************************
  */
  //private personApiService = inject(PersonApiService);

  /*public OnPersonDelete():void{
  this.personApiService.PersonDelete("0000000001").subscribe(
    {
      next:(data:IData)=>{
        console.log(data);
      }
    }
  );
}*/
}


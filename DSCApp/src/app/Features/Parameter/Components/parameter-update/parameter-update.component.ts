import { Component, inject,effect, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter, DoCheck } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { CompanyApiService } from 'src/app/Core/Services/company-api.service';
import { ParameterApiService } from 'src/app/Core/Services/parameter-api.service';
import { ICompany } from 'src/app/Core/Models/Entity/ICompany';
import { IParameter } from 'src/app/Core/Models/Entity/IParameter';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-parameter-update',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './parameter-update.component.html',
  styleUrls: ['./parameter-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ParameterUpdateComponent implements OnInit{
  
  /********************************************************************
  propiedades de entrada*/
  /********************************************************************
  eventos de salida*/
  
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private companyApiService = inject(CompanyApiService);
  private parameterApiService = inject(ParameterApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  campos*/
  public DataCompany:WritableSignal<ICompany[]|undefined> = signal<ICompany[]|undefined>(undefined);
  public Data:WritableSignal<IParameter|undefined> = signal<IParameter|undefined>(undefined);
  public formGroup!:FormGroup;
  //modal id
  private id:string="modal-update";
  

  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
    CompanyID:['0000000000000',[Validators.compose([Validators.required])]],
    ParameterType:['',[Validators.compose([Validators.required])]],
    ParameterName:['',[Validators.compose([Validators.required])]],
    ParameterValue:['',[Validators.compose([Validators.required])]],
    ParameterActive:[true,[Validators.compose([Validators.required])]]
    });
    //*******************************
    this.OnCompanyRead();
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
      this.InitializateFormulary(this.Data()!);
    }
    );
    }
  
//modal (cierra y envia data a origen)
  public closeModal() {
  this.dscModalService.close(this.id,{data:'action-update'});
  }

  private OnParameterUpdate():void{
  let data:IParameter = {
    ParameterID:this.Data()!.ParameterID,
    CompanyID:this.CompanyIDControl.value,
    ParameterType:this.ParameterTypeControl.value,
    ParameterName:this.ParameterNameControl.value,
    ParameterValue:this.ParameterValueControl.value,
    ParameterActive:this.ParameterActiveControl.value
  };
  this.parameterApiService.ParameterUpdate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//State
          this.formGroup.reset();
          this.closeModal();
        }
      }
    }
  );
}



public OnCompanyRead():void{
  this.companyApiService.CompanyRead(null, null,1,1).subscribe(
    {
      next:(data:IDataResponse<ICompany>)=>{
        this.DataCompany?.set(data.Data);
        console.log(this.DataCompany());
      }
    }
  );
}

/****************************************************************************
metodos
****************************************************************************/

public InitializateFormulary(data:IParameter){
  this.Data.set(data);
  this.formGroup.patchValue(
    this.Data()!
  );
  
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnParameterUpdate()
  }
}



/******************************************************************
propiedades
******************************************************************/

public get CompanyIDControl():FormControl{
  return this.formGroup.get('CompanyID') as FormControl;
}
public get ParameterTypeControl():FormControl{
  return this.formGroup.get('ParameterType') as FormControl;
}
public get ParameterNameControl():FormControl{
  return this.formGroup.get('ParameterName') as FormControl;
}
public get ParameterValueControl():FormControl{
  return this.formGroup.get('ParameterValue') as FormControl;
}
public get ParameterActiveControl():FormControl{
  return this.formGroup.get('ParameterActive') as FormControl;
}

public OnRefresh(){console.log('refresh-update');}

}
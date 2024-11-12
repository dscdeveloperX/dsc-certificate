import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, Output, EventEmitter } from '@angular/core';
import{FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { CompanyApiService } from 'src/app/Core/Services/company-api.service';
import { ParameterApiService } from 'src/app/Core/Services/parameter-api.service';
import { ICompany } from 'src/app/Core/Models/Entity/ICompany';
import { IParameter } from 'src/app/Core/Models/Entity/IParameter';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-parameter-create',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './parameter-create.component.html',
  styleUrls: ['./parameter-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ParameterCreateComponent implements OnInit {

  /********************************************************************
  variables*/
  //modal id
  private id:string="modal-create";
  /********************************************************************
  inject*/
  private companyApiService = inject(CompanyApiService);
  private parameterApiService = inject(ParameterApiService);
  private formBuilder = inject(FormBuilder);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  public DataCompany:WritableSignal<ICompany[]|undefined> = signal<ICompany[]|undefined>(undefined);
  public formGroup!:FormGroup;
  

  ngOnInit(): void {
    //inicializamos formulario
    this.formGroup = this.formBuilder.group({
    CompanyID:['0000000000000',[Validators.compose([Validators.required])]],
    ParameterType:['',[Validators.compose([Validators.required])]],
    ParameterName:['',[Validators.compose([Validators.required])]],
    ParameterValue:['',[Validators.compose([Validators.required])]],
    ParameterActive:[true,[Validators.compose([Validators.required])]]
    });
    //cargamos lista de companias
    this.OnCompanyRead();
    //
    this.ReciveData();
  }

  /***********************************************************************
  servicios
  ***********************************************************************/
  //modal (recibe data de origen)
  private ReciveData(){
  this.dscModalService.dataIn(this.id)?.subscribe(
  (x)=>{}
  );
  }

  private OnParameterCreate():void{
  let data:IParameter = {
    CompanyID:this.CompanyIDControl.value,
    ParameterType:this.ParameterTypeControl.value,
    ParameterName:this.ParameterNameControl.value,
    ParameterValue:this.ParameterValueControl.value,
    ParameterActive:this.ParameterActiveControl.value
  };
  this.parameterApiService.ParameterCreate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//State
          this.OnFormReset();
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

//modal (cierra y envia data a origen)
public closeModal() {
  this.dscModalService.close(this.id,{data:'action-create'});
}

/****************************************************************************
metodos
****************************************************************************/

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnParameterCreate()
  }
}

private OnFormReset():void{
  this.formGroup.reset();
  this.ParameterActiveControl.setValue(true);
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


public OnRefresh(){console.log('refresh-create');}

}
import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyApiService } from 'src/app/Core/Services/company-api.service';
import { ProvinceApiService } from 'src/app/Core/Services/province-api.service';
import { ICompany } from 'src/app/Core/Models/Entity/ICompany';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { ICity } from 'src/app/Core/Models/Entity/ICity';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { IProvince } from 'src/app/Core/Models/Entity/IProvince';
import { IProvinceCity } from 'src/app/Core/Models/Entity/IProvinceCity';


@Component({
  selector: 'app-company-update',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './company-update.component.html',
  styleUrls: ['./company-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CompanyUpdateComponent implements OnInit{
  
  /********************************************************************
  propiedades de entrada*/
  /********************************************************************
  eventos de salida*/
  
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private companyApiService = inject(CompanyApiService);
  private provinceApiService = inject(ProvinceApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  campos*/
  public Data:WritableSignal<ICompany|undefined> = signal<ICompany|undefined>(undefined);
  //public DataCompany:WritableSignal<ICompany[]|undefined> = signal<ICompany[]|undefined>(undefined);
  public DataCity:WritableSignal<ICity[]> = signal<ICity[]>([]);
  public DataProvince:WritableSignal<IProvince[]> = signal<IProvince[]>([]);
  public formGroup!:FormGroup;
  //modal id
  private id:string="modal-update";
  

  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      CompanyRuc:['',[Validators.compose([Validators.required])]],
      ProvinceID:['',[Validators.compose([Validators.required])]],
      CityID:['',[Validators.compose([Validators.required])]],
      CompanyName:['',[Validators.compose([Validators.required])]],
      CompanyAddress:['',[Validators.compose([Validators.required])]],
      CompanyPhone:['',[Validators.compose([Validators.required])]],
      CompanyActive:[true,[Validators.compose([Validators.required])]]
    });
    //*******************************
    //this.OnCompanyRead();
    this.ReciveData();
    this.OnProvinceRead();
    this.OnChangeProvince();
    
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

  public OnProvinceRead():void{
    //CAMBIAR PROVINCIA CIUDAD
    this.provinceApiService.ProvinceRead(null, null,1,1000).subscribe(
      {
        next:(data:IDataResponse<IProvince>)=>{
          this.DataProvince?.set(data.Data);
          console.log(this.DataProvince());
        }
      }
    );
  }

  public OnCityRead(provinceID:number):void{
    this.provinceApiService.ProvinceCityRead(provinceID, true,1,1000).subscribe(
      {
        next:(data:IDataResponse<IProvinceCity>)=>{
          this.DataCity?.set(data.Data);
          console.log(this.DataCity());
        }
      }
    );
  }

  private OnCompanyUpdate():void{
  let data:ICompany = {
    CompanyID:this.Data()!.CompanyID,
    CompanyRuc:this.CompanyRucControl.value,
    ProvinceID:this.ProvinceIDControl.value,
    CityID:this.CityIDControl.value,
    CompanyName:this.CompanyNameControl.value,
    CompanyAddress:this.CompanyAddressControl.value,
    CompanyPhone:this.CompanyPhoneControl.value,
    CompanyActive:this.CompanyActiveControl.value
  };
  this.companyApiService.CompanyUpdate(data).subscribe(
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




/*public OnCompanyRead():void{
  this.companyApiService.CompanyRead(null, null,1,1).subscribe(
    {
      next:(data:IData)=>{
        this.DataCompany?.set(data.Data);
        console.log(this.DataCompany());
      }
    }
  );
}*/

/****************************************************************************
metodos
****************************************************************************/

//obtenemos el objeto de formulario en cada cambio
private OnChangeProvince():void{
  this.ProvinceIDControl.valueChanges.subscribe((value)=>{
    /*console.log('valueChanges');
    console.log(value );*/
    this.OnCityRead(value);
  });
  }



public InitializateFormulary(data:ICompany){
  this.Data.set(data);
  this.formGroup.patchValue(
    this.Data()!
  );
  
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnCompanyUpdate()
  }
}



/******************************************************************
propiedades
******************************************************************/

public get CompanyRucControl():FormControl{
  return this.formGroup.get('CompanyRuc') as FormControl;
}
public get ProvinceIDControl():FormControl{
  return this.formGroup.get('ProvinceID') as FormControl;
}
public get CityIDControl():FormControl{
  return this.formGroup.get('CityID') as FormControl;
}
public get CompanyNameControl():FormControl{
  return this.formGroup.get('CompanyName') as FormControl;
}
public get CompanyAddressControl():FormControl{
  return this.formGroup.get('CompanyAddress') as FormControl;
}
public get CompanyPhoneControl():FormControl{
  return this.formGroup.get('CompanyPhone') as FormControl;
}
public get CompanyActiveControl():FormControl{
  return this.formGroup.get('CompanyActive') as FormControl;
}

  
  
  /*
  ****************************************************************************
  ****************************************************************************
  ****************************************************************************
  ****************************************************************************
  ****************************************************************************
  ****************************************************************************
  ****************************************************************************
  ****************************************************************************
  **/

}

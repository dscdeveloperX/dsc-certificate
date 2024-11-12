import { Component, inject, ChangeDetectionStrategy,signal, WritableSignal, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DscCalendarComponent } from 'src/app/dsc/dsc-calendar/dsc-calendar.component';
import { PersonApiService } from 'src/app/Core/Services/person-api.service';
import { ParameterApiService } from 'src/app/Core/Services/parameter-api.service';
import { CityApiService } from 'src/app/Core/Services/city-api.service';
import { ProvinceApiService } from 'src/app/Core/Services/province-api.service';
import { MaritalStatusApiService } from 'src/app/Core/Services/marital-status-api.service';
import { GenderApiService } from 'src/app/Core/Services/gender-api.service';
import { IPerson } from 'src/app/Core/Models/Entity/IPerson';
import { ICity } from 'src/app/Core/Models/Entity/ICity';
import { IProvinceCity } from 'src/app/Core/Models/Entity/IProvinceCity';
import { IGender } from 'src/app/Core/Models/Entity/IGender';
import { IMaritalStatus } from 'src/app/Core/Models/Entity/IMaritalStatus';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { DateToString } from 'src/app/Core/Constans/convert';

@Component({
  selector: 'app-person-update',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule,DscCalendarComponent],
  templateUrl: './person-update.component.html',
  styleUrls: ['./person-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class PersonUpdateComponent {

/********************************************************************
  propiedades de entrada*/
  /********************************************************************
  eventos de salida*/
  //modal id
  private id:string="modal-update";
  @ViewChild('PersonDateOfBirth')
  public PersonDateOfBirth!:DscCalendarComponent;//calendar
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private personApiService = inject(PersonApiService);
  private parameterApiService = inject(ParameterApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  private cityApiService = inject(CityApiService);
  private provinceApiService = inject(ProvinceApiService);
  private maritalStatusApiService = inject(MaritalStatusApiService);
  private genderApiServicee = inject(GenderApiService);
  /**********************************************************************
  campos*/
  public DataPerson:WritableSignal<IPerson[]|undefined> = signal<IPerson[]|undefined>(undefined);
  public DataCity:WritableSignal<ICity[]> = signal<ICity[]>([]);
  public DataProvince:WritableSignal<IProvinceCity[]> = signal<IProvinceCity[]>([]);
  public DataGender:WritableSignal<IGender[]> = signal<IGender[]>([]);
  public DataMaritalStatus:WritableSignal<IMaritalStatus[]> = signal<IMaritalStatus[]>([]);
  public Data:WritableSignal<IPerson|undefined> = signal<IPerson|undefined>(undefined);
  public formGroup!:FormGroup;
  
  

  
  ngOnInit(): void {
     //inicializamos formulario
     this.formGroup = this.formBuilder.group({
      PersonID:['',[Validators.compose([Validators.required])]],
      CityID:['',[Validators.compose([Validators.required])]],
      ProvinceID:['',[Validators.compose([Validators.required])]],
      PersonName:['',[Validators.compose([Validators.required])]],
      PersonSurname:['',[Validators.compose([Validators.required])]],
      PersonDateOfBirth:['',[Validators.compose([Validators.required])]],
      PersonEmail:['',[Validators.compose([Validators.required])]],
      PersonPhone:['',[Validators.compose([Validators.required])]],
      MaritalStatusID:['',[Validators.compose([Validators.required])]],
      GenderID:['',[Validators.compose([Validators.required])]],
      PersonActive:[true,[Validators.compose([Validators.required])]]
      });
    //*******************************
    
    this.ReciveData();
    this.OnProvinceRead();
    this.OnChangeProvince();
    this.OnMaritalStatusRead();
    this.OnGenderRead();
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
  

    

    public OnGenderRead():void{
      this.genderApiServicee.GenderRead(null, true,1,100).subscribe(
        {
          next:(data:IDataResponse<IGender>)=>{
            this.DataGender?.set(data.Data);
          }
        }
      );
    }

    public OnProvinceCityRead(provinceID:number):void{
      //HAY QUE CORREGIR PROVINCIA CIUDAD
      this.provinceApiService.ProvinceCityRead(provinceID, true,1,1000).subscribe(
        {
          next:(data:IDataResponse<ICity>)=>{
            this.DataCity?.set(data.Data);
            console.log(this.DataProvince());
          }
        }
      );
    }
  
  
  
  
    public OnProvinceRead():void{
      this.provinceApiService.ProvinceRead(null, null,1,1000).subscribe(
        {
          next:(data:IDataResponse<IProvinceCity>)=>{
            this.DataProvince?.set(data.Data);
            console.log(this.DataCity());
          }
        }
      );
    }

    public OnMaritalStatusRead():void{
      this.maritalStatusApiService.MaritalStatusRead(null, true,1,100).subscribe(
        {
          next:(data:IDataResponse<IMaritalStatus>)=>{
            this.DataMaritalStatus?.set(data.Data);
          }
        }
      );
    }
//modal (cierra y envia data a origen)
  public closeModal() {
  this.dscModalService.close(this.id,{data:'action-update'});
  }

  public OnPersonUpdate():void{
    let data:IPerson= {
      PersonID:this.Data()!.PersonID,
      ProvinceID:this.ProvinceIDControl.value,
      CityID:this.CityIDControl.value,
      PersonName:this.PersonNameControl.value,
      PersonSurname:this.PersonSurnameControl.value,
      PersonDateOfBirth:this.PersonDateOfBirthControl.value,
      PersonPhone:this.PersonPhoneControl.value,
      PersonEmail:this.PersonEmailControl.value,
      MaritalStatusID:this.MaritalStatusIDControl.value,
      GenderID:this.GenderIDControl.value,
      PersonActive:this.PersonActiveControl.value
    };
    this.personApiService.PersonUpdate(data).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          if(data.ErrorCodigo== 0){//State
            this.OnFormReset();
            this.closeModal();
            
          }
      }
    }
    );
  }


/****************************************************************************
metodos
****************************************************************************/

public OnModalPersonDateOfBirthOpen(){
  this.PersonDateOfBirth.Open();
}

public OnPersonDateOfBirth(data:string){
  this.PersonDateOfBirthControl.setValue(data);
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnPersonUpdate()
  }
}

private OnFormReset():void{
  //this.formGroup.reset();
  this.PersonIDControl.reset();
  this.CityIDControl.reset();
  this.ProvinceIDControl.reset();
  this.PersonNameControl.reset();
  this.PersonSurnameControl.reset();
  this.PersonEmailControl.reset();
  this.GenderIDControl.reset();
  this.MaritalStatusIDControl.reset();
  //establecemos fecha inicial
  this.PersonDateOfBirthControl.setValue(DateToString(new Date()));
}

//obtenemos el objeto de formulario en cada cambio
private OnChangeProvince(){
this.ProvinceIDControl.valueChanges.subscribe((value)=>{
  
  this.OnProvinceCityRead(value);
});
}


public InitializateFormulary(data:IPerson){
  this.Data.set(data);
  this.formGroup.patchValue(
    this.Data()!
  );
  
}



/******************************************************************
propiedades
******************************************************************/

public get PersonIDControl():FormControl{
  return this.formGroup.get('PersonID') as FormControl;
}
/*public get PhotoControl():FormControl{
  return this.formGroup.get('Photo') as FormControl;
}
public get SignatureControl():FormControl{
  return this.formGroup.get('Signature') as FormControl;
}
*/
public get PersonEmailControl():FormControl{
  return this.formGroup.get('PersonEmail') as FormControl;
}
public get CityIDControl():FormControl{
  return this.formGroup.get('CityID') as FormControl;
}

public get ProvinceIDControl():FormControl{
  return this.formGroup.get('ProvinceID') as FormControl;
}
public get PersonNameControl():FormControl{
  return this.formGroup.get('PersonName') as FormControl;
}
public get PersonSurnameControl():FormControl{
  return this.formGroup.get('PersonSurname') as FormControl;
}
public get PersonDateOfBirthControl():FormControl{
  return this.formGroup.get('PersonDateOfBirth') as FormControl;
}
public get MaritalStatusIDControl():FormControl{
  return this.formGroup.get('MaritalStatusID') as FormControl;
}
public get PersonPhoneControl():FormControl{
  return this.formGroup.get('PersonPhone') as FormControl;
}
public get GenderIDControl():FormControl{
  return this.formGroup.get('GenderID') as FormControl;
}
public get PersonActiveControl():FormControl{
  return this.formGroup.get('PersonActive') as FormControl;
}


}

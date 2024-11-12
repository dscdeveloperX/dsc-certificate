import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, Output, EventEmitter, Renderer2, ViewChild } from '@angular/core';
import{FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';
import { DscCalendarComponent } from 'src/app/dsc/dsc-calendar/dsc-calendar.component';
import { PersonApiService } from 'src/app/Core/Services/person-api.service';
import { EmployeeApiService } from 'src/app/Core/Services/employee-api.service';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { IPersonEmployee } from 'src/app/Core/Models/Entity/IPersonEmployee';
import { urlHostBase } from 'src/app/Core/Constans/api-params';
import { DateToString } from 'src/app/Core/Constans/convert';
import { IEmployee } from 'src/app/Core/Models/Entity/IEmployee';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-employee-create',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule, DscCalendarComponent],
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class EmployeeCreateComponent implements OnInit {

  /********************************************************************
  variables*/
  @ViewChild('EmployeeDateEntry') EmployeeDateEntry!:DscCalendarComponent;
  @ViewChild('EmployeeDateExit') EmployeeDateExit!:DscCalendarComponent;
  
  //modal id
  private id:string="modal-create";
  /********************************************************************
  inject*/
  private personApiService = inject(PersonApiService);
  private employeeApiService = inject(EmployeeApiService);
  private formBuilder = inject(FormBuilder);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  public DataPersonEmployee:WritableSignal<IPersonEmployee[]|undefined> = signal<IPersonEmployee[]|undefined>(undefined);
  public formGroup!:FormGroup;
  private CompanyID!:number;
  public EmployeeActive:WritableSignal<boolean> = signal<boolean>(true);
  public Host:string = urlHostBase;
  public PersonPhoto:WritableSignal<string> = signal<string>('');

  ngOnInit(): void {
    //inicializamos formulario
    this.formGroup = this.formBuilder.group({
    PersonID:['',[Validators.compose([Validators.required])]],
    EmployeeDateEntry:['',[Validators.compose([Validators.required])]],
    EmployeeDateExit:[''],
    EmployeeReason:[''],
    EmployeeActive:[true,[Validators.compose([Validators.required])]]
    });
    this.CompanyID = 1;
    
    //
    this.ReciveData();
    //detecta cambios de activacion de empleado
    this.EmployeeActiveControl.valueChanges.subscribe((value)=>{
      this.EmployeeActive.set(value);
    });
    //establecemos fecha inicial
    this.EmployeeDateEntryControl.setValue(DateToString(new Date()));  
    this.EmployeeDateExitControl.setValue(DateToString(new Date()));  
      
  }

  
  /***********************************************************************
  servicios
  ***********************************************************************/
  //modal (recibe data de origen)
  private ReciveData(){
  this.dscModalService.dataIn(this.id)?.subscribe(
  (x)=>{
    //cargamos lista de companias
    this.OnPersonRead();
    //establecemos fecha inicial
    this.EmployeeDateEntryControl.setValue(DateToString(new Date()));  
    this.EmployeeDateExitControl.setValue(DateToString(new Date()));  
  }
  );
  }


  public OnEmployeeCreate():void{
    //si esta activado el empleado
    //para evitar problemas con el calendar de dateexit ya que siempre necesita un fecha
    //creamos una variable a la que le asignamos la fecha is la hay o undefinde si no. 
    let EmployeeDateExit = this.EmployeeDateExitControl.value;
    if(this.EmployeeActiveControl.value){
      EmployeeDateExit = undefined;
      this.EmployeeReasonControl.setValue(undefined);
    }
    let data:IEmployee = {
      CompanyID:this.CompanyID,
      PersonID:this.PersonIDControl.value,
      EmployeeDateEntry:this.EmployeeDateEntryControl.value,
      EmployeeDateExit:EmployeeDateExit,
      EmployeeReason:this.EmployeeReasonControl.value,
      EmployeeActive:this.EmployeeActiveControl.value
    };
    this.employeeApiService.EmployeeCreate(data).subscribe(
      {
        next:(data:IDataResponse<IEmployee>)=>{
          if(data.ErrorCodigo == 0){//State
            this.DataPersonEmployee.set([]);
            this.OnFormReset();
            this.closeModal();
            
          }
        }
      }
    );
  }


  /*private OnParameterCreate():void{
  let data:IParameter = {
    CompanyID:this.CompanyIDControl.value,
    ParameterType:this.ParameterTypeControl.value,
    ParameterName:this.ParameterNameControl.value,
    ParameterValue:this.ParameterValueControl.value,
    ParameterActive:this.ParameterActiveControl.value
  };
  this.parameterApiService.ParameterCreate(data).subscribe(
    {
      next:(data:IData)=>{
        if(data.State){
          this.OnFormReset();
          this.closeModal();
          
        }
      }
    }
  );
}*/


public OnPersonRead():void{
  this.personApiService.PersonEmployeeRead(this.CompanyID).subscribe(
    {
      next:(data:IDataResponse<IPersonEmployee>)=>{
        this.DataPersonEmployee?.set(data.Data);
        console.log(this.DataPersonEmployee());
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

public OnPersonPhoto(value:string):void{
    this.PersonPhoto.set(value);
}

public OnEmployeeDateEntry(data:string){
  this.EmployeeDateEntryControl.setValue(data);
}
public OnEmployeeDateExit(data:string){
  this.EmployeeDateExitControl.setValue(data);
}

public OnModalEmployeeDateEntryOpen(){
  this.EmployeeDateEntry.Open();
}
public OnModalEmployeeDateExitOpen(){
  this.EmployeeDateExit.Open();
}


public OnSubmit(){
  if(this.formGroup.valid){
      this.OnEmployeeCreate()
  }
}

private OnFormReset():void{
  console.log("-1");
  this.formGroup.reset();
  console.log("0");
  this.EmployeeActiveControl.setValue(true);
  //fechas iniciales
  console.log("1");
  this.EmployeeDateEntryControl.setValue(DateToString(new Date()));  
  console.log("2");
    this.EmployeeDateExitControl.setValue(DateToString(new Date()));
    console.log("3");
}

/******************************************************************
propiedades
******************************************************************/

public get PersonIDControl():FormControl{
  return this.formGroup.get('PersonID') as FormControl;
}
public get EmployeeDateEntryControl():FormControl{
  return this.formGroup.get('EmployeeDateEntry') as FormControl;
}
public get EmployeeDateExitControl():FormControl{
  return this.formGroup.get('EmployeeDateExit') as FormControl;
}
public get EmployeeReasonControl():FormControl{
  return this.formGroup.get('EmployeeReason') as FormControl;
}
public get EmployeeActiveControl():FormControl{
  return this.formGroup.get('EmployeeActive') as FormControl;
}


  /**********************************************************************************
   * *********************************************************************************
   * *********************************************************************************
   * *********************************************************************************
   * *********************************************************************************
   * *********************************************************************************
  */

  

 

}
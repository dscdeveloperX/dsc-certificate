import { Component, inject,effect, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter, DoCheck, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DscCalendarComponent } from 'src/app/dsc/dsc-calendar/dsc-calendar.component';
import { EmployeeApiService } from 'src/app/Core/Services/employee-api.service';
import { IEmployee } from 'src/app/Core/Models/Entity/IEmployee';
import { urlHostBase } from 'src/app/Core/Constans/api-params';
import { DateToString } from 'src/app/Core/Constans/convert';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-employee-update',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule, DscCalendarComponent],
  templateUrl: './employee-update.component.html',
  styleUrls: ['./employee-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class EmployeeUpdateComponent implements OnInit{
  
  /********************************************************************
  propiedades de entrada*/
  
  @ViewChild('EmployeeDateExit') EmployeeDateExit!:DscCalendarComponent;
  /********************************************************************
  eventos de salida*/
  
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private employeeApiService = inject(EmployeeApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  campos*/
  public Data:WritableSignal<IEmployee|undefined> = signal<IEmployee|undefined>(undefined);
  public EmployeeActive:WritableSignal<boolean> = signal<boolean>(true);
  public formGroup!:FormGroup;
  //modal id
  private id:string="modal-update";
  //host
  public Host:string = urlHostBase;
  //
  private CompanyID!:number;
  

  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      EmployeeDateExit:['',[Validators.compose([Validators.required])]],
      EmployeeReason:[''],
      EmployeeActive:[true,[Validators.compose([Validators.required])]]
    });
    //*******************************
    this.ReciveData();
    //detecta cambios de activacion de empleado
    this.EmployeeActiveControl.valueChanges.subscribe((value)=>{
      this.EmployeeActive.set(value);
    });
    //inicia con sesion
    this.CompanyID = 1;
    //establecemos fecha inicial
    this.EmployeeDateExitControl.setValue(DateToString(new Date()));
      
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


  public OnEmployeeUpdate():void{
    //si esta activado el empleado 
    let EmployeeDateExit = this.EmployeeDateExitControl.value;
    if(this.EmployeeActiveControl.value){
      EmployeeDateExit = undefined;
      this.EmployeeReasonControl.setValue(undefined);
    }
    
    let data:IEmployee = {
      PersonPhoto:undefined,
      EmployeeID:this.Data()!.EmployeeID,
      CompanyID:this.CompanyID,
      CompanyName:undefined,
      PersonID:this.Data()!.PersonID,
      PersonName:undefined,
      PersonSurname:undefined,
      EmployeeDateEntry:new Date(),
      EmployeeDateExit:EmployeeDateExit,
      EmployeeReason:this.EmployeeReasonControl.value,
      EmployeeActive:this.EmployeeActiveControl.value
    };
    this.employeeApiService.EmployeeUpdate(data).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          if(data.ErrorCodigo == 0){
            this.formGroup.reset();
            //establecemos fecha inicial
          this.EmployeeDateExitControl.setValue(DateToString(new Date()));
            this.closeModal();
          }
        }
      }
    );
  }


  




/****************************************************************************
metodos
****************************************************************************/

public OnEmployeeDateExit(data:string){
  this.EmployeeDateExitControl.setValue(data);
}

public OnModalEmployeeDateExitOpen(){
  this.EmployeeDateExit.Open();
}

public InitializateFormulary(data:IEmployee){
  this.Data.set(data);
  this.formGroup.patchValue(
    this.Data()!
  );
  //si llenamos con un valor null le asignamos fecha actual
  if(this.EmployeeDateExitControl.value==null){
    this.EmployeeDateExitControl.setValue(DateToString(new Date())); 
  }
  
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnEmployeeUpdate()
  }
}



/******************************************************************
propiedades
******************************************************************/
  
public get EmployeeDateExitControl():FormControl{
  return this.formGroup.get('EmployeeDateExit') as FormControl;
}
public get EmployeeReasonControl():FormControl{
  return this.formGroup.get('EmployeeReason') as FormControl;
}
public get EmployeeActiveControl():FormControl{
  return this.formGroup.get('EmployeeActive') as FormControl;
}

  /******************************************************************************
  ******************************************************************************
  ******************************************************************************
  ******************************************************************************
  ******************************************************************************
  ******************************************************************************
  ******************************************************************************
  ******************************************************************************/

 

  

}
import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { GenderApiService } from 'src/app/Core/Services/gender-api.service';
import { IGender } from 'src/app/Core/Models/Entity/IGender';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-gender-update',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './gender-update.component.html',
  styleUrls: ['./gender-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class GenderUpdateComponent implements OnInit{

/********************************************************************
  propiedades de entrada*/
  /********************************************************************
  eventos de salida*/
  
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private genderApiService = inject(GenderApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  
  /**********************************************************************
  campos*/
  public Data:WritableSignal<IGender|undefined> = signal<IGender|undefined>(undefined);
  public formGroup!:FormGroup;
  //modal id
  private id:string="modal-update";
  

  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      GenderID:['',[Validators.compose([Validators.required])]],
      GenderDescription:['',[Validators.compose([Validators.required])]],
      GenderActive:[true,[Validators.compose([Validators.required])]]
    });
    //*******************************
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

  
  private OnGenderUpdate():void{
    let data:IGender = {
      GenderID:this.Data()!.GenderID,
      GenderDescription:this.GenderDescriptionControl.value,
      GenderActive:this.GenderActiveControl.value
    };
  this.genderApiService.GenderUpdate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorMessage){//State
          this.formGroup.reset();
          this.closeModal();
        }
      }
    }
  );
}





/****************************************************************************
metodos
****************************************************************************/

public InitializateFormulary(data:IGender){
  this.Data.set(data);
  this.formGroup.patchValue(
    this.Data()!
  );
  
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnGenderUpdate()
  }
}



/******************************************************************
propiedades
******************************************************************/

public get GenderIDControl():FormControl{
  return this.formGroup.get('GenderID') as FormControl;
}
public get GenderDescriptionControl():FormControl{
  return this.formGroup.get('GenderDescription') as FormControl;
}
public get GenderActiveControl():FormControl{
  return this.formGroup.get('GenderActive') as FormControl;
}


  
   
}

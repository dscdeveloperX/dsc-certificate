import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { GenderApiService } from 'src/app/Core/Services/gender-api.service';
import { IGender } from 'src/app/Core/Models/Entity/IGender';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-gender-create',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './gender-create.component.html',
  styleUrls: ['./gender-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class GenderCreateComponent implements OnInit{


  /********************************************************************
  variables*/
  //modal id
  private id:string="modal-create";
  /********************************************************************
  inject*/
  private genderApiService = inject(GenderApiService);
  private formBuilder = inject(FormBuilder);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  public formGroup!:FormGroup;
  

  ngOnInit(): void {
    //inicializamos formulario
    this.formGroup = this.formBuilder.group({
    GenderID:['',[Validators.compose([Validators.required])]],
    GenderDescription:['',[Validators.compose([Validators.required])]],
    GenderActive:[true,[Validators.compose([Validators.required])]]
    });
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

  private OnGenderCreate():void{
  let data:IGender = {
    GenderID:this.GenderIDControl.value,
    GenderDescription:this.GenderDescriptionControl.value,
    GenderActive:this.GenderActiveControl.value
  };
  this.genderApiService.GenderCreate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){
          this.OnFormReset();
          this.closeModal();
          
        }
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
      this.OnGenderCreate()
  }
}

private OnFormReset():void{
  this.formGroup.reset();
  this.GenderActiveControl.setValue(true);
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

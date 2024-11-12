import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { MaritalStatusApiService } from 'src/app/Core/Services/marital-status-api.service';
import { IMaritalStatus } from 'src/app/Core/Models/Entity/IMaritalStatus';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-marital-status-create',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './marital-status-create.component.html',
  styleUrls: ['./marital-status-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class MaritalStatusCreateComponent implements OnInit{


  /********************************************************************
  variables*/
  //modal id
  private id:string="modal-create";
  /********************************************************************
  inject*/
  private maritalStatusApiService = inject(MaritalStatusApiService);
  private formBuilder = inject(FormBuilder);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  public formGroup!:FormGroup;
  

  ngOnInit(): void {
    //inicializamos formulario
    this.formGroup = this.formBuilder.group({
    MaritalStatusID:['',[Validators.compose([Validators.required])]],
    MaritalStatusDescription:['',[Validators.compose([Validators.required])]],
    MaritalStatusActive:[true,[Validators.compose([Validators.required])]]
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


  private OnMaritalStatusCreate():void{
    let data:IMaritalStatus = {
      MaritalStatusID:this.MaritalStatusIDControl.value,
      MaritalStatusDescription:this.MaritalStatusDescriptionControl.value,
      MaritalStatusActive:this.MaritalStatusActiveControl.value
    };
  this.maritalStatusApiService.MaritalStatusCreate(data).subscribe(
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





//modal (cierra y envia data a origen)
public closeModal() {
  this.dscModalService.close(this.id,{data:'action-create'});
}

/****************************************************************************
metodos
****************************************************************************/

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnMaritalStatusCreate()
  }
}

private OnFormReset():void{
  this.formGroup.reset();
  this.MaritalStatusActiveControl.setValue(true);
}

/******************************************************************
propiedades
******************************************************************/

public get MaritalStatusIDControl():FormControl{
  return this.formGroup.get('MaritalStatusID') as FormControl;
}
public get MaritalStatusDescriptionControl():FormControl{
  return this.formGroup.get('MaritalStatusDescription') as FormControl;
}
public get MaritalStatusActiveControl():FormControl{
  return this.formGroup.get('MaritalStatusActive') as FormControl;
}

 
}

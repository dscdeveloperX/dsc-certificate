import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DocumentTypeApiService } from 'src/app/Core/Services/document-type-api.service';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-document-type-create',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './document-type-create.component.html',
  styleUrls: ['./document-type-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentTypeCreateComponent implements OnInit{


  /********************************************************************
  variables*/
  //modal id
  private id:string="modal-create";
  /********************************************************************
  inject*/
  private documentTypeApiService = inject(DocumentTypeApiService);
  private formBuilder = inject(FormBuilder);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  public formGroup!:FormGroup;
  

  ngOnInit(): void {
    //inicializamos formulario
    this.formGroup = this.formBuilder.group({
    DocumentTypeID:['',[Validators.compose([Validators.required])]],
    DocumentTypeDescription:['',[Validators.compose([Validators.required])]],
    DocumentTypeActive:[true,[Validators.compose([Validators.required])]]
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



  private OnDocumentTypeCreate():void{
  let data:IDocumentType = {
    DocumentTypeID:this.DocumentTypeIDControl.value,
    DocumentTypeDescription:this.DocumentTypeDescriptionControl.value,
    DocumentTypeActive:this.DocumentTypeActiveControl.value
  };
  this.documentTypeApiService.DocumentTypeCreate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//state
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
      this.OnDocumentTypeCreate()
  }
}

private OnFormReset():void{
  this.formGroup.reset();
  this.DocumentTypeActiveControl.setValue(true);
}

/******************************************************************
propiedades
******************************************************************/

public get DocumentTypeIDControl():FormControl{
  return this.formGroup.get('DocumentTypeID') as FormControl;
}
public get DocumentTypeDescriptionControl():FormControl{
  return this.formGroup.get('DocumentTypeDescription') as FormControl;
}
public get DocumentTypeActiveControl():FormControl{
  return this.formGroup.get('DocumentTypeActive') as FormControl;
}





}

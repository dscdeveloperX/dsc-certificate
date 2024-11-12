import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DocumentTypeApiService } from 'src/app/Core/Services/document-type-api.service';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-document-type-update',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './document-type-update.component.html',
  styleUrls: ['./document-type-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentTypeUpdateComponent implements OnInit{

  /********************************************************************
    propiedades de entrada*/
    /********************************************************************
    eventos de salida*/
    
    /********************************************************************
    inject*/
    private formBuilder = inject(FormBuilder);
    private documentTypeApiService = inject(DocumentTypeApiService);
    //modal injectar servicios
    private dscModalService = inject(DscModalService);
    
    /**********************************************************************
    campos*/
    public Data:WritableSignal<IDocumentType|undefined> = signal<IDocumentType|undefined>(undefined);
    public formGroup!:FormGroup;
    //modal id
    private id:string="modal-update";
    
  
    
    ngOnInit(): void {
      this.formGroup = this.formBuilder.group({
        DocumentTypeID:['',[Validators.compose([Validators.required])]],
    DocumentTypeDescription:['',[Validators.compose([Validators.required])]],
    DocumentTypeActive:[true,[Validators.compose([Validators.required])]]
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
  
    /*public OnDocumentTypeUpdate():void{
    let data:IDocumentType = {
      DocumentTypeID:'cheque',
      DocumentTypeDescription:'chfacil',
      DocumentTypeActive:true
    };
    this.documentTypeApiService.DocumentTypeUpdate(data).subscribe(
      {
        next:(data:IData)=>{
          console.log(data);
        }
      }
    );
  }*/
    
    private OnDocumentTypeUpdate():void{
      let data:IDocumentType = {
        DocumentTypeID:this.Data()!.DocumentTypeID,
        DocumentTypeDescription:this.DocumentTypeDescriptionControl.value,
        DocumentTypeActive:this.DocumentTypeActiveControl.value
      };
      this.documentTypeApiService.DocumentTypeUpdate(data).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          if(data.ErrorCodigo==0){//State
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
  
  public InitializateFormulary(data:IDocumentType){
    this.Data.set(data);
    this.formGroup.patchValue(
      this.Data()!
    );
    
  }
  
  public OnSubmit(){
    if(this.formGroup.valid){
        this.OnDocumentTypeUpdate()
    }
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

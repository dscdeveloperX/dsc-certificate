import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { MaritalStatusApiService } from 'src/app/Core/Services/marital-status-api.service';
import { IMaritalStatus } from 'src/app/Core/Models/Entity/IMaritalStatus';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-marital-status-update',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './marital-status-update.component.html',
  styleUrls: ['./marital-status-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class MaritalStatusUpdateComponent implements OnInit{

  /********************************************************************
    propiedades de entrada*/
    /********************************************************************
    eventos de salida*/
    
    /********************************************************************
    inject*/
    private formBuilder = inject(FormBuilder);
    private maritalStatusApiService = inject(MaritalStatusApiService);
    //modal injectar servicios
    private dscModalService = inject(DscModalService);
    
    /**********************************************************************
    campos*/
    public Data:WritableSignal<IMaritalStatus|undefined> = signal<IMaritalStatus|undefined>(undefined);
    public formGroup!:FormGroup;
    //modal id
    private id:string="modal-update";
    
  
    
    ngOnInit(): void {
      this.formGroup = this.formBuilder.group({
        MaritalStatusID:['',[Validators.compose([Validators.required])]],
        MaritalStatusDescription:['',[Validators.compose([Validators.required])]],
        MaritalStatusActive:[true,[Validators.compose([Validators.required])]]
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
  

    
    private OnMaritalStatusUpdate():void{
      let data:IMaritalStatus = {
        MaritalStatusID:this.Data()!.MaritalStatusID,
        MaritalStatusDescription:this.MaritalStatusDescriptionControl.value,
        MaritalStatusActive:this.MaritalStatusActiveControl.value
      };
    this.maritalStatusApiService.MaritalStatusUpdate(data).subscribe(
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
  
  
  
  
  
  /****************************************************************************
  metodos
  ****************************************************************************/
  
  public InitializateFormulary(data:IMaritalStatus){
    this.Data.set(data);
    this.formGroup.patchValue(
      this.Data()!
    );
    
  }
  
  public OnSubmit(){
    if(this.formGroup.valid){
        this.OnMaritalStatusUpdate()
    }
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

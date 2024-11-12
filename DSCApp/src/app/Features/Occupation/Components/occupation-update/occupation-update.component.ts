import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { OccupationApiService } from 'src/app/Core/Services/occupation-api.service';
import { IOccupation } from 'src/app/Core/Models/Entity/IOccupation';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-occupation-update',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './occupation-update.component.html',
  styleUrls: ['./occupation-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class OccupationUpdateComponent  implements OnInit{

  /********************************************************************
    propiedades de entrada*/
    /********************************************************************
    eventos de salida*/
    
    /********************************************************************
    inject*/
    private formBuilder = inject(FormBuilder);
    private occupationApiService = inject(OccupationApiService);
    //modal injectar servicios
    private dscModalService = inject(DscModalService);
    
    /**********************************************************************
    campos*/
    public Data:WritableSignal<IOccupation|undefined> = signal<IOccupation|undefined>(undefined);
    public formGroup!:FormGroup;
    //modal id
    private id:string="modal-update";
    
  
    
    ngOnInit(): void {
      this.formGroup = this.formBuilder.group({
        OccupationID:['',[Validators.compose([Validators.required])]],
    OccupationDescription:['',[Validators.compose([Validators.required])]],
    OccupationActive:[true,[Validators.compose([Validators.required])]]
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
  

  

    
    private OnOccupationUpdate():void{
      let data:IOccupation = {
        OccupationID:this.Data()!.OccupationID,
        OccupationDescription:this.OccupationDescriptionControl.value,
        OccupationActive:this.OccupationActiveControl.value
      };
      this.occupationApiService.OccupationUpdate(data).subscribe(
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
  
  public InitializateFormulary(data:IOccupation){
    this.Data.set(data);
    this.formGroup.patchValue(
      this.Data()!
    );
    
  }
  
  public OnSubmit(){
    if(this.formGroup.valid){
        this.OnOccupationUpdate()
    }
  }
  
  
  
  /******************************************************************
  propiedades
  ******************************************************************/
  
  public get OccupationIDControl():FormControl{
    return this.formGroup.get('OccupationID') as FormControl;
  }
  public get OccupationDescriptionControl():FormControl{
    return this.formGroup.get('OccupationDescription') as FormControl;
  }
  public get OccupationActiveControl():FormControl{
    return this.formGroup.get('OccupationActive') as FormControl;
  }
  
  


}

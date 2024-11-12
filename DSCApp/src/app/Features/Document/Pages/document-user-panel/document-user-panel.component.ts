import { Component, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef, inject, OnInit, OnChanges, EventEmitter, Output, Input, WritableSignal, signal, SimpleChanges, AfterViewInit, ElementRef, ViewChildren, QueryList, Signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DscModalComponent } from 'src/app/dsc/dsc-modal/dsc-modal.component';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DocumentListYearComponent } from '../../Components/document-list-year/document-list-year.component';
import { DocumentListTypeComponent } from '../../Components/document-list-type/document-list-type.component';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { DocumentTypeApiService } from 'src/app/Core/Services/document-type-api.service';
import { urlHostBase } from 'src/app/Core/Constans/api-params';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';
import { IDocumentGroup } from 'src/app/Core/Models/Entity/IDocumentGroup';
import { IDocumentUser } from 'src/app/Core/Models/Entity/IDocumentUser';
import { IPagination } from 'src/app/Core/Models/Entity/IPagination';
import { YearRange } from 'src/app/Core/Constans/utility';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { IDocumentAdmin } from 'src/app/Core/Models/Entity/IDocumentAdmin';

@Component({
  selector: 'app-document-user-panel',
  standalone: true,
  imports: [CommonModule ,DscModalComponent,FormsModule,ReactiveFormsModule, DocumentListYearComponent, DocumentListTypeComponent],
  templateUrl: './document-user-panel.component.html',
  styleUrls: ['./document-user-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentUserPanelComponent  implements OnInit, AfterViewInit {



  /********************************************************************
    variables de entrada y salida */
    @ViewChildren('inputDocumentID')
    public inputDocumentIDList!:QueryList<ElementRef>;
  /********************************************************************
    inject*/
    private documentApiService = inject(DocumentApiService);
    private documentTypeApiService = inject(DocumentTypeApiService);
    //modal injectar servicio
    private dscModalService = inject(DscModalService);
    private formBuilder=inject(FormBuilder);
  /**********************************************************************
    variables*/
    public HostBase:string = urlHostBase;
    public filterPaginateQuantity:number=0;
    public documentYearData:string[]=[];
    public DataYear:WritableSignal<string> = signal<string>('');
    public DataDocumentGroupType:WritableSignal<IDocumentType[]> = signal<IDocumentType[]>([]);
    public DataType:WritableSignal<IDocumentType|undefined> = signal<IDocumentType|undefined>(undefined);
    public DataDocumentGroupID:WritableSignal<IDocumentGroup[]> = signal<IDocumentGroup[]>([]);
    //modal id
    public idYear:string="modal-year";
    public idType:string="modal-type";
    public idDelete:string="modal-delete";
    public Data:WritableSignal<IDocumentUser[]> = signal<IDocumentUser[]>([]);
    public DataPagination:WritableSignal<IPagination>=signal<IPagination>({
      Previous:false, 
      Pages:0, 
      Page:1, 
      Next:true,
      Quantity:0,//inicia en 0
      Total:0
    });
    //
    private companyID:number=1;
    public formGroup!:FormGroup;
  
    constructor(){
      this.formGroup = this.formBuilder.group({
        documentGroupType:['',Validators.compose([Validators.required])],
        documentGroupDateYear:['',Validators.compose([Validators.required])],
      });
    }
  
  
    ngOnInit(): void {
      //inicializamos paginacion a 5
      this.filterPaginateQuantity = 10;
      //
      this.documentYearData = YearRange(0,50);
      //
      this.DataYear.set(YearRange(0,50)[0]);
      this.OnDocumentGroupType();
      //
      //
      this.documentGroupDateYearControl.valueChanges.subscribe(
        (value)=>{
          //this.OnDocumentGroupRead();
          //this.OnDocumentUser();
        }
      );
      this.documentGroupTypeControl.valueChanges.subscribe(
        (value)=>{
          //this.OnDocumentGroupRead();
          //this.OnDocumentUser();
        }
      );

    }
  
  
     //modal (recibe data de destino)
     ngAfterViewInit(): void {
      //create
      this.ReciveActionYear();
      //update
      this.ReciveActionType();
      //delete
      //this.ReciveActionDelete();
    }
    
  
  /***********************************************************************
    servicios
  ***********************************************************************/
    
    /*public OnParameterRead():void{
      this.parameterApiService.ParameterRead(null, null, 1,3).subscribe(
        {
          next:(data:IData)=>{
            this.Data?.set(data.Data);
            console.log(this.Data());
          }
        }
      );
    }*/

    public OnDocumentUser(){
      this.documentApiService.DocumentUserRead(this.companyID,this.DataType()!.DocumentTypeID, Number(this.DataYear())).subscribe({
        next:(data:IDataResponse<IDocumentUser>)=>{
          console.log(data.Data);
          this.Data.set(data.Data);
        }        
      });
    }
   


    public OnDocumentGroupType(){
      this.documentTypeApiService.DocumentTypeRead(null, null, 1,1000).subscribe({
        next:(data:IDataResponse<IDocumentType>)=>{
          this.DataDocumentGroupType.set(data.Data);
           //establezco valor type
           
            this.DataType.set(data.Data.find((x:IDocumentType)=>x.DocumentTypeID === 'CERT-619'));
           //cargo lista
          this.OnDocumentUser();
        }        
      });
    }

   
    
    
  
    private ReciveActionYear(){
      this.dscModalService.dataOut(this.idYear)?.subscribe(
        (d)=>{
          //if(x.data == 'action-year'){
          //this.OnDocumentCount(this.documentGroupIDControl.value);
          if(d.data.length > 0){
          this.DataYear.set(d.data);
          this.OnDocumentUser();
          }
          //}
        });
    }
  
    private ReciveActionType(){
      this.dscModalService.dataOut(this.idType)?.subscribe(
        (x)=>{
          //if(x.data == 'action-update'){
          //this.InitialPagination(this.DataPagination().Total, this.DataPagination().Quantity, 0);
          //console.log(x);
          if(x.data != undefined && x.data != ""){
          if(x.data.length > 0 || x.data.DocumentTypeID.length > 0){
          this.DataType.set(x.data);
          this.OnDocumentUser();
          }
          }
        });
    }
  
    /*private ReciveActionDelete(){
      this.dscModalService.dataOut(this.idDelete)?.subscribe(
        (x)=>{
          if(x.data == 'action-delete'){
          this.InitialPagination(this.DataPagination().Total, this.DataPagination().Quantity, 0);
          }
        });
    }*/
  
  /****************************************************************************
  metodos
  ****************************************************************************/
  
  public OnSubmit(){
    if(this.formGroup.valid){
      //this.OnDocumentCount(this.documentGroupIDControl.value);
      alert("onsubmit");
    }else{
      alert(`a: ${this.documentGroupTypeControl.value} b: ${this.documentGroupDateYearControl.value}`);      
    }
  }
  
  

    public get getDocumentID():number[]{
      let documentIDData:number[] =[];
      let listInputs:ElementRef[] = this.inputDocumentIDList.filter((e:ElementRef)=>e.nativeElement.checked == true);
      listInputs.forEach((item:ElementRef)=>{
        documentIDData.push(Number(item.nativeElement.value));
      });
      return documentIDData;
    }

    public OnChangeDocumentID(event:any):void{
      if(event.target.checked){
        //selecciono todas las filas
        this.inputDocumentIDList.forEach((item:ElementRef)=>{
          if(item.nativeElement.checked == false){
            item.nativeElement.checked = true;
          }      
        });
      }else{
      //deslecciono todas las filas
      this.inputDocumentIDList.forEach((item:ElementRef)=>{
        if(item.nativeElement.checked == true){
          item.nativeElement.checked = false;
        }      
      });
      }
      
    }

    public readID(index:number, item:IDocumentUser):number | undefined{
      return item.DocumentID;
    }
  
    
    //modal (abrir y enviar data a destino)
    public OnClickYear(){
      this.openModalYear(this.documentYearData);
    }
    public OnClickType(){
      this.openModalType(this.DataDocumentGroupType());
    }
  
    public DataImage:WritableSignal<string> = signal<string>('');
    public OnDocumentImageView(event:Event,name:string):void{
      this.documentApiService.DocumentImageViewer(name).subscribe({
        next:(data:IDataResponse<any>)=>{
          this.DataImage.set(data.Data[0]);
        }        
      });
    }

  
  public openModalYear(item:string[]) {
    this.dscModalService.open(this.idYear,{data:item});
  }
  
  public openModalType(item:IDocumentType[]) {
    this.dscModalService.open(this.idType,{data:item});
  }
  
  public openModalDelete(item:IDocumentAdmin) {
    this.dscModalService.open(this.idDelete,{data:item});
  }
  
  /******************************************************************
  propiedades
  ******************************************************************/
  
  public get documentGroupTypeControl():FormControl{
    return this.formGroup.get('documentGroupType') as FormControl;
  }
  public get documentGroupDateYearControl():FormControl{
    return this.formGroup.get('documentGroupDateYear') as FormControl;
  }
  

  }
  
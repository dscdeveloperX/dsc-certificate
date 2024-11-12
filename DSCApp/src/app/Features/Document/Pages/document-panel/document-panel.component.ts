import { Component, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef, inject, OnInit, OnChanges, EventEmitter, Output, Input, WritableSignal, signal, SimpleChanges, AfterViewInit, ElementRef, ViewChildren, QueryList } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DscModalComponent } from 'src/app/dsc/dsc-modal/dsc-modal.component';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { DocumentGroupApiService } from 'src/app/Core/Services/document-group-api.service';
import { DocumentTypeApiService } from 'src/app/Core/Services/document-type-api.service';
import { urlHostBase } from 'src/app/Core/Constans/api-params';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';
import { IDocumentGroup } from 'src/app/Core/Models/Entity/IDocumentGroup';
import { IDocumentAdmin } from 'src/app/Core/Models/Entity/IDocumentAdmin';
import { IPagination } from 'src/app/Core/Models/Entity/IPagination';
import { YearRange } from 'src/app/Core/Constans/utility';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-document-panel',
  standalone: true,
  imports: [CommonModule ,DscModalComponent,FormsModule,ReactiveFormsModule],
  templateUrl: './document-panel.component.html',
  styleUrls: ['./document-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentPanelComponent implements OnInit, AfterViewInit {



  /********************************************************************
    variables de entrada y salida */
    @ViewChildren('inputDocumentID')
    public inputDocumentIDList!:QueryList<ElementRef>;
  /********************************************************************
    inject*/
    private documentApiService = inject(DocumentApiService);
    private documentGroupApiService= inject(DocumentGroupApiService);
    private documentTypeApiService = inject(DocumentTypeApiService);
    //modal injectar servicio
    private dscModalService = inject(DscModalService);
    private formBuilder=inject(FormBuilder);
  /**********************************************************************
    variables*/
    public HostBase:string = urlHostBase;
    public filterPaginateQuantity:number=0;
    public documentYearData:string[]=[];
    public documentYear?:string;
    public DataDocumentGroupType:WritableSignal<IDocumentType[]> = signal<IDocumentType[]>([]);
    public DataDocumentGroupID:WritableSignal<IDocumentGroup[]> = signal<IDocumentGroup[]>([]);
    //modal id
    public idCreate:string="modal-create";
    public idUpdate:string="modal-update";
    public idDelete:string="modal-delete";
    public Data:WritableSignal<IDocumentAdmin[]> = signal<IDocumentAdmin[]>([]);
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
        documentGroupID:['',Validators.compose([Validators.required])]
      });
    }
  
  
    ngOnInit(): void {
      //inicializamos paginacion a 5
      this.filterPaginateQuantity = 10;
      //
      //
      this.OnDocumentGroupType();
      this.documentYearData = YearRange(0,50);
      //
      this.documentGroupDateYearControl.valueChanges.subscribe(
        (value)=>{
          this.OnDocumentGroupRead();
        }
      );
      this.documentGroupTypeControl.valueChanges.subscribe(
        (value)=>{
          this.OnDocumentGroupRead();
        }
      );

    }
  
  
     //modal (recibe data de destino)
     ngAfterViewInit(): void {
      //create
      this.ReciveActionCreate();
      //update
      this.ReciveActionUpdate();
      //delete
      this.ReciveActionDelete();
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

    
  
    public OnDocumentSendEmail():void{
      //lista id document par enviar por correo pdf a trabajadores
      let d = this.getDocumentID;
      if(d.length>0){
        this.documentApiService.DocumentUpdateXml(d).subscribe(
          {
            next:(data:IDataResponse<any>)=>{
              if(data.ErrorCodigo == 0){//State
                alert(data.ErrorMessage);
                this.OnDocumentCount(this.documentGroupIDControl.value);
              }else{
                alert(data.ErrorMessage);
              }
            },
            error:(e)=>console.error(e.message),
            complete:()=>console.info("email completo")
          }
        );
      }else{
        alert("No existen registros seleccionados");
      }
      
    }

    public OnDocumentDelete():void{
      //lista de id document para eliminar registros table y file pdf
      //lista id document par enviar por correo pdf a trabajadores
      let d = this.getDocumentID;
      if(d.length>0){
        if(confirm(`Confirmar eliminación de ${d.length} registro(s).`)){
        this.documentApiService.DocumentDeleteXml(d, this.documentGroupIDControl.value).subscribe(
          {
            next:(data:IDataResponse<any>)=>{
              if(data.ErrorCodigo == 0){//State
                alert(data.ErrorMessage);
                this.OnDocumentCount(this.documentGroupIDControl.value);
              }else{
                alert(data.ErrorMessage);
              }
            },
            error:(e)=>console.error(e.message),
            complete:()=>console.info("eliminado completo")
          }
        );
      }
      }else{
        alert("No existen registros seleccionados");
      }
    }


    public OnDocumentGroupType(){
      this.documentTypeApiService.DocumentTypeRead(null, null, 1,1000).subscribe({
        next:(data:IDataResponse<IDocumentType>)=>{
          this.DataDocumentGroupType.set(data.Data);
        }        
      });
    }

    public OnDocumentGroupRead():void{
      if(this.documentGroupDateYearControl.value !='' && this.documentGroupTypeControl.value !== ''){
        let prm_documentGroupType=this.documentGroupTypeControl.value;
        let prm_documentGroupDateYear:number=this.documentGroupDateYearControl.value;
        this.documentGroupApiService.DocumentGroupRead(this.companyID, prm_documentGroupType, prm_documentGroupDateYear, true, 1, 9000).subscribe({
          next:(data:IDataResponse<IDocumentGroup>)=>{
              this.DataDocumentGroupID.set(data.Data);
          }
        });    
      }
    }

    public OnDocumentAdminRead(page:number, quantity:number):void{
      this.documentApiService.DocumentAdminRead(this.documentGroupIDControl.value,page,quantity).subscribe(
        {
          next:(data:IDataResponse<IDocumentAdmin>)=>{
            this.Data?.set(data.Data);
            //console.log(this.Data());
          }
        }
      );
    }
    
    public OnDocumentCount(documentGroupID:number):void{
      this.documentApiService.DocumentCount(documentGroupID).subscribe(
        {
          next:(data:any)=>{
            this.InitialPagination(data.Data[0], this.filterPaginateQuantity, 0);
            
          }
        }
      );
    }
    
  
    private ReciveActionCreate(){
      this.dscModalService.dataOut(this.idCreate)?.subscribe(
        (x)=>{
          if(x.data == 'action-create'){
          this.OnDocumentCount(this.documentGroupIDControl.value);
          }
        });
    }
  
    private ReciveActionUpdate(){
      this.dscModalService.dataOut(this.idUpdate)?.subscribe(
        (x)=>{
          if(x.data == 'action-update'){
          this.InitialPagination(this.DataPagination().Total, this.DataPagination().Quantity, 0);
          }
        });
    }
  
    private ReciveActionDelete(){
      this.dscModalService.dataOut(this.idDelete)?.subscribe(
        (x)=>{
          if(x.data == 'action-delete'){
          this.InitialPagination(this.DataPagination().Total, this.DataPagination().Quantity, 0);
          }
        });
    }
  
  /****************************************************************************
  metodos
  ****************************************************************************/
  
  public OnSubmit(){
    if(this.formGroup.valid){
      this.OnDocumentCount(this.documentGroupIDControl.value);
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

    public readID(index:number, item:IDocumentAdmin):number | undefined{
      return item.DocumentID;
    }
  
    private InitialPagination(total:number,quantity:number, paginate:number ):void{
      this.DataPagination.mutate((x)=>{
        //otengo el total de registros de la tabla
        x.Total =  total;
      //filtra cantidad de registro a visualizar
        x.Quantity = quantity;
      //obtengo cantidad de paginas a paginar
        x.Pages = Math.ceil(total/quantity);
      })
      //realizo paginacion
      this.OnPaginate(paginate);
    }
    
    public OnPaginate(paginate:number):void{
      //agrego +1 | -1 a pagina actual
      this.DataPagination.mutate((x)=>{
        //agrego +1 | -1 a pagina actual
        x.Page += paginate
        //si elimino y desbordo rango entonces page tomara el valor de pages
        x.Page = (x.Page > x.Pages)?x.Pages:x.Page;
        //cuando creamo el primer registro iniciamos page a 1 y no en 0
        x.Page = (x.Page ==0 && x.Pages >0)?1:x.Page;
        //habilitamos previous
        x.Previous = (x.Page>1);
        //habilitamos next
        x.Next = (x.Pages>x.Page);
      });
      this.OnDocumentAdminRead(this.DataPagination().Page, this.DataPagination().Quantity);
    }
  
    public OnPaginateQuantity():void{
        this.OnDocumentCount(this.documentGroupIDControl.value);
    }
    
    //modal (abrir y enviar data a destino)
    public OnClickCreate(){
      this.openModalCreate();
    }
  
  
  public openModalCreate() {
    this.dscModalService.open(this.idCreate,{data:''});
  }
  
  public openModalUpdate(item:IDocumentAdmin) {
    this.dscModalService.open(this.idUpdate,{data:item});
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
  public get documentGroupIDControl():FormControl{
    return this.formGroup.get('documentGroupID') as FormControl;
  }
  

  }
  
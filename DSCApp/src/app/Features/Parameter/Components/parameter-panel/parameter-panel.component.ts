import { Component, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef, inject, OnInit, OnChanges, EventEmitter, Output, Input, WritableSignal, signal, SimpleChanges, AfterViewInit, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ParameterCreateComponent } from '../parameter-create/parameter-create.component';
import { ParameterUpdateComponent } from '../parameter-update/parameter-update.component';
import { ParameterDeleteComponent } from '../parameter-delete/parameter-delete.component';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DscModalComponent } from 'src/app/dsc/dsc-modal/dsc-modal.component';
import { FormsModule } from '@angular/forms';
import { IParameter } from 'src/app/Core/Models/Entity/IParameter';
import { IPagination } from 'src/app/Core/Models/Entity/IPagination';
import { ParameterApiService } from 'src/app/Core/Services/parameter-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-parameter-panel',
  standalone: true,
  imports: [CommonModule, ParameterCreateComponent, ParameterUpdateComponent, ParameterDeleteComponent, DscModalComponent,FormsModule],
  templateUrl: './parameter-panel.component.html',
  styleUrls: ['./parameter-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ParameterPanelComponent implements OnInit, AfterViewInit {



  /********************************************************************
    variables de entrada y salida */
    
  /********************************************************************
    inject*/
    private parameterApiService = inject(ParameterApiService);
    //modal injectar servicio
    private dscModalService = inject(DscModalService);
  /**********************************************************************
    variables*/
    public filterPaginateQuantity:number=0;
    //modal id
    public idCreate:string="modal-create";
    public idUpdate:string="modal-update";
    public idDelete:string="modal-delete";
    public Data:WritableSignal<IParameter[]> = signal<IParameter[]>([]);
    public DataPagination:WritableSignal<IPagination>=signal<IPagination>({
      Previous:false, 
      Pages:0, 
      Page:1, 
      Next:true,
      Quantity:0,//inicia en 0
      Total:0
    });
    
  
  
  
    ngOnInit(): void {
      //inicializamos paginacion a 5
      this.filterPaginateQuantity = 5;
      //
      this.OnParameterCount();
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
  
    public OnParameterCompanyRead(page:number, quantity:number):void{
      this.parameterApiService.ParameterCompanyRead('', null,page,quantity).subscribe(
        {
          next:(data:IDataResponse<IParameter>)=>{
            this.Data?.set(data.Data);
            //console.log(this.Data());
          }
        }
      );
    }
    
    public OnParameterCount():void{
      this.parameterApiService.ParameterCount().subscribe(
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
          this.OnParameterCount();
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
  
    public readID(index:number, item:IParameter):number | undefined{
      return item.ParameterID;
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
      this.OnParameterCompanyRead(this.DataPagination().Page, this.DataPagination().Quantity);
    }
  
    public OnPaginateQuantity():void{
        this.OnParameterCount();
    }
    
    //modal (abrir y enviar data a destino)
    public OnClickCreate(){
      this.openModalCreate();
    }
  
  
  public openModalCreate() {
    this.dscModalService.open(this.idCreate,{data:''});
  }
  
  public openModalUpdate(item:IParameter) {
    this.dscModalService.open(this.idUpdate,{data:item});
  }
  
  public openModalDelete(item:IParameter) {
    this.dscModalService.open(this.idDelete,{data:item});
  }
  
  /******************************************************************
  propiedades
  ******************************************************************/
  
  
  
  
  }
  
  
  
  
  
  
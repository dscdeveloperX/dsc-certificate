import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalComponent } from 'src/app/dsc/dsc-modal/dsc-modal.component';
import { FormsModule } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { MaritalStatusCreateComponent } from '../marital-status-create/marital-status-create.component';
import { MaritalStatusUpdateComponent } from '../marital-status-update/marital-status-update.component';
import { MaritalStatusDeleteComponent } from '../marital-status-delete/marital-status-delete.component';
import { MaritalStatusApiService } from 'src/app/Core/Services/marital-status-api.service';
import { IMaritalStatus } from 'src/app/Core/Models/Entity/IMaritalStatus';
import { IPagination } from 'src/app/Core/Models/Entity/IPagination';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-marital-status-panel',
  standalone: true,
  imports: [CommonModule,DscModalComponent,FormsModule,MaritalStatusCreateComponent, MaritalStatusUpdateComponent, MaritalStatusDeleteComponent],
  templateUrl: './marital-status-panel.component.html',
  styleUrls: ['./marital-status-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class MaritalStatusPanelComponent implements OnInit, AfterViewInit {


  

  /********************************************************************
    variables de entrada y salida */
    
  /********************************************************************
    inject*/
    private maritalStatusApiService = inject(MaritalStatusApiService);
    //modal injectar servicio
    private dscModalService = inject(DscModalService);
  
  /**********************************************************************
    variables*/
    public filterPaginateQuantity:number=0;
    //modal id
    public idCreate:string="modal-create";
    public idUpdate:string="modal-update";
    public idDelete:string="modal-delete";
    public Data:WritableSignal<IMaritalStatus[]> = signal<IMaritalStatus[]>([]);
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
      this.OnMaritalStatusCount();
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
    
  public OnMaritalStatusRead(page:number, quantity:number):void{
    this.maritalStatusApiService.MaritalStatusRead(null, null,page,quantity).subscribe(
      {
        next:(data:IDataResponse<IMaritalStatus>)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }

    
    public OnMaritalStatusCount():void{
      this.maritalStatusApiService.MaritalStatusCount().subscribe(
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
          this.OnMaritalStatusCount();
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
  
    public readID(index:number, item:any):number | undefined{
      return index;//item.ParameterID;
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
      this.OnMaritalStatusRead(this.DataPagination().Page, this.DataPagination().Quantity);
    }
  
    public OnPaginateQuantity():void{
        this.OnMaritalStatusCount();
    }
    
    //modal (abrir y enviar data a destino)
    public OnClickCreate(){
      this.openModalCreate();
    }
  
  
  public openModalCreate() {
    this.dscModalService.open(this.idCreate,{data:''});
  }
  
  public openModalUpdate(item:IMaritalStatus) {
    this.dscModalService.open(this.idUpdate,{data:item});
  }
  
  public openModalDelete(item:IMaritalStatus) {
    this.dscModalService.open(this.idDelete,{data:item});
  }



  
  

}

import { Component, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef, inject, OnInit, OnChanges, EventEmitter, Output, Input, WritableSignal, signal, SimpleChanges, AfterViewInit, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DscModalComponent } from 'src/app/dsc/dsc-modal/dsc-modal.component';
import { FormsModule } from '@angular/forms';
import { EmployeeCreateComponent } from '../employee-create/employee-create.component';
import { EmployeeUpdateComponent } from '../employee-update/employee-update.component';
import { EmployeeDeleteComponent } from '../employee-delete/employee-delete.component';
import { EmployeeApiService } from 'src/app/Core/Services/employee-api.service';
import { IEmployee } from 'src/app/Core/Models/Entity/IEmployee';
import { IPagination } from 'src/app/Core/Models/Entity/IPagination';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { urlHostBase } from 'src/app/Core/Constans/api-params';

@Component({
  selector: 'app-employee-panel',
  standalone: true,
  imports: [CommonModule, DscModalComponent,FormsModule,EmployeeCreateComponent,EmployeeUpdateComponent,EmployeeDeleteComponent],
  templateUrl: './employee-panel.component.html',
  styleUrls: ['./employee-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class EmployeePanelComponent implements OnInit, AfterViewInit {



  /********************************************************************
    variables de entrada y salida */
    @ViewChild('searchValue') searchValue!:ElementRef;
  /********************************************************************
    inject*/
    private employeeApiService = inject(EmployeeApiService);
    //modal injectar servicio
    private dscModalService = inject(DscModalService);
     /**********************************************************************
    variables*/
    public filterPaginateQuantity:number=0;
    //modal id
    public idCreate:string="modal-create";
    public idUpdate:string="modal-update";
    public idDelete:string="modal-delete";
    public Data:WritableSignal<IEmployee[]> = signal<IEmployee[]>([]);
    public DataPagination:WritableSignal<IPagination>=signal<IPagination>({
      Previous:false, 
      Pages:0, 
      Page:1, 
      Next:true,
      Quantity:0,//inicia en 0
      Total:0
    });
    public Host:string = urlHostBase;
    public math=Math;
    public PersonName:string='';
    private CompanyID!:number;
  
  
    ngOnInit(): void {
      //inicializamos paginacion a 5
      this.filterPaginateQuantity = 5;
      this.CompanyID = 1;//reemplazar session
      //
      this.OnEmployeeCount();
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
    
    public OnEmployeeRead(page:number, quantity:number):void{
      this.employeeApiService.EmployeeReadSearch(this.PersonName,this.CompanyID,page,quantity).subscribe(
        {
          next:(data:IDataResponse<IEmployee>)=>{
            this.Data?.set(data.Data);
            console.log(this.Data());
          }
        }
      );
    }
  
    /*public OnParameterCompanyRead(page:number, quantity:number):void{
      this.parameterApiService.ParameterCompanyRead('', null,page,quantity).subscribe(
        {
          next:(data:IData)=>{
            this.Data?.set(data.Data);
            //console.log(this.Data());
          }
        }
      );
    }*/
    
    public OnEmployeeCount():void{
      this.employeeApiService.EmployeeCount(this.PersonName,this.CompanyID).subscribe(
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
          this.OnEmployeeCount();
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
  
  public OnSearch(option:string){
    if(option==='search'){
      this.PersonName = this.searchValue.nativeElement.value;
    }else if(option==='search-all'){
      this.searchValue.nativeElement.value = '';
      this.PersonName = this.searchValue.nativeElement.value;
    }
    this.OnEmployeeCount();
    
  }
  


    public readID(index:number, item:IEmployee):number | undefined{
      return item.EmployeeID;
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
      this.OnEmployeeRead(this.DataPagination().Page, this.DataPagination().Quantity);
    }
  
    public OnPaginateQuantity():void{
        this.OnEmployeeCount();
    }
    
    //modal (abrir y enviar data a destino)
    public OnClickCreate(){
      this.openModalCreate();
    }
  
  
  public openModalCreate() {
    this.dscModalService.open(this.idCreate,{data:''});
  }
  
  public openModalUpdate(item:IEmployee) {
    this.dscModalService.open(this.idUpdate,{data:item});
  }
  
  public openModalDelete(item:IEmployee) {
    this.dscModalService.open(this.idDelete,{data:item});
  }
  
  /******************************************************************
  propiedades
  ******************************************************************/
  


/**********************************************************************************
 *********************************************************************************
 ********************************************************************************
 **********************************************************************************/


  

  
  

 
}

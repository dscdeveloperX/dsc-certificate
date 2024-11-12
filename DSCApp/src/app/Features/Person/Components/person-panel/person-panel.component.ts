import { Component, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef, inject, OnInit, OnChanges, EventEmitter, Output, Input, WritableSignal, signal, SimpleChanges, AfterViewInit, ElementRef, Renderer2 } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { DscModalComponent } from 'src/app/dsc/dsc-modal/dsc-modal.component';
import { FormsModule } from '@angular/forms';
import { PersonCreateComponent } from '../person-create/person-create.component';
import { PersonUpdateComponent } from '../person-update/person-update.component';
import { PersonDeleteComponent } from '../person-delete/person-delete.component';
import { PersonUpdatePhotoComponent } from '../person-update-photo/person-update-photo.component';
import { PersonUpdateSignatureComponent } from '../person-update-signature/person-update-signature.component';
import { PersonApiService } from 'src/app/Core/Services/person-api.service';
import { IPerson } from 'src/app/Core/Models/Entity/IPerson';
import { IPagination } from 'src/app/Core/Models/Entity/IPagination';
import { urlHostBase } from 'src/app/Core/Constans/api-params';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-person-panel',
  standalone: true,
  imports: [CommonModule, DscModalComponent,FormsModule,PersonCreateComponent,PersonUpdateComponent, PersonDeleteComponent,PersonUpdatePhotoComponent,PersonUpdateSignatureComponent],
  templateUrl: './person-panel.component.html',
  styleUrls: ['./person-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class PersonPanelComponent  implements OnInit, AfterViewInit {



  /********************************************************************
    variables de entrada y salida */
    @ViewChild('searchValue') searchValue!:ElementRef;
  /********************************************************************
    inject*/
    private personApiService = inject(PersonApiService);
    //modal injectar servicio
    private dscModalService = inject(DscModalService);
    private renderer2 = inject(Renderer2);
    private changeDetectorRef = inject(ChangeDetectorRef);
  /**********************************************************************
    variables*/
    public filterPaginateQuantity:number=0;
    //modal id
    public idCreate:string="modal-create";
    public idUpdate:string="modal-update";
    public idUpdatePhoto:string="modal-update-photo";
    public idUpdateSignature:string="modal-update-signature";
    public idDelete:string="modal-delete";
    public Data:WritableSignal<IPerson[]> = signal<IPerson[]>([]);
    public DataPagination:WritableSignal<IPagination>=signal<IPagination>({
      Previous:false, 
      Pages:0, 
      Page:1, 
      Next:true,
      Quantity:0,//inicia en 0
      Total:0
    });
    public Host:string = urlHostBase;
    //parametro que le asignaremos a la url de la imagen para que  
    //este sea refrescado por el navegador a pesar de tener el mismo nombre
    public math = Math;
    public PersonName:string='';


    ngOnInit(): void {
      //inicializamos paginacion a 5
      this.filterPaginateQuantity = 5;
      //
      this.OnPersonCount();
    }
  
  
     //modal (recibe data de destino)
     ngAfterViewInit(): void {
      //create
      this.ReciveActionCreate();
      //update
      this.ReciveActionUpdate();
      //update-photo
      this.ReciveActionUpdatePhoto();
      //update-signature
      this.ReciveActionUpdateSignature();
      //delete
      this.ReciveActionDelete();
    }
    
  
  /***********************************************************************
    servicios
  ***********************************************************************/
    public OnPersonRead(page:number, quantity:number):void{
      this.personApiService.PersonReadSearch(this.PersonName,page,quantity).subscribe(
        {
          next:(data:IDataResponse<IPerson>)=>{
            this.Data?.set(data.Data);
            console.log(this.Data());
          }
        }
      );
    }
  
    
    
    public OnSearch(option:string){
      if(option==='search'){
        this.PersonName = this.searchValue.nativeElement.value;
      }else if(option==='search-all'){
        this.searchValue.nativeElement.value = '';
        this.PersonName = this.searchValue.nativeElement.value;
      }
      this.OnPersonCount();
      
    }

    public OnPersonCount():void{
      this.personApiService.PersonCount(this.PersonName).subscribe(
        {
          next:(data:any)=>{
            this.InitialPagination(data.Data[0], this.filterPaginateQuantity, 0);
            
          }
        }
      );
    }

    //**************************************************** */

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
    
    /*public OnParameterCount():void{
      this.parameterApiService.ParameterCount().subscribe(
        {
          next:(data:any)=>{
            this.InitialPagination(data.Data[0], this.filterPaginateQuantity, 0);
            
          }
        }
      );
    }*/
    
  
    private ReciveActionCreate(){
      this.dscModalService.dataOut(this.idCreate)?.subscribe(
        (x)=>{
          if(x.data == 'action-create'){
          this.OnPersonCount();
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
    
    private ReciveActionUpdatePhoto(){
      this.dscModalService.dataOut(this.idUpdatePhoto)?.subscribe(
        (x)=>{
          if(x.data == 'action-update-photo'){
          this.InitialPagination(this.DataPagination().Total, this.DataPagination().Quantity, 0);
          }
        });
    }

    private ReciveActionUpdateSignature(){
      this.dscModalService.dataOut(this.idUpdateSignature)?.subscribe(
        (x)=>{
          if(x.data == 'action-update-signature'){
            //habilitamos scroll
          //this.renderer2.setStyle(this.PersonList.nativeElement,'overflow','auto');
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
  
    public readID(index:number, item:IPerson):number | undefined{
      return index;//item.PersonID;
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
      this.OnPersonRead(this.DataPagination().Page, this.DataPagination().Quantity);
    }
  
    public OnPaginateQuantity():void{
        this.OnPersonCount();
    }
    
    //modal (abrir y enviar data a destino)
    public OnClickCreate(){
      this.openModalCreate();
    }
  
  
  public openModalCreate() {
    this.dscModalService.open(this.idCreate,{data:''});
  }
  
  public openModalUpdate(item:IPerson) {
    this.dscModalService.open(this.idUpdate,{data:item});
  }

  public openModalUpdatePhoto(item:IPerson) {
    this.dscModalService.open(this.idUpdatePhoto,{data:item});
  }

  public openModalUpdateSignature(item:IPerson) {
    //quitar los scroll para evitar problemas al dibujar con touch
    //this.renderer2.setStyle(this.PersonList.nativeElement,'overflow','hidden');
    //mostrar popup
    this.dscModalService.open(this.idUpdateSignature,{data:item});
  }
  
  public openModalDelete(item:IPerson) {
    this.dscModalService.open(this.idDelete,{data:item});
  }
  
  /******************************************************************
  propiedades
  ******************************************************************/
  
  
  
  
  }
  
  
  
  
  
  
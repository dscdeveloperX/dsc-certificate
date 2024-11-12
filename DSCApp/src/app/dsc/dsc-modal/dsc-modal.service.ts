import { Injectable } from '@angular/core';
import { DscModalComponent } from './dsc-modal.component';


@Injectable({
  providedIn: 'root'
})
export class DscModalService {

  private modals:DscModalComponent[]=[];

  constructor() { }

  //agregar modal a coleccion
  public add(modal:DscModalComponent):void{
      this.modals.push(modal);
  }  

  //eliminar modal de coleccion
  public remove(id:string):void{
    this.modals = this.modals.filter(x=>x.Id !== id);
  }

  public open(id:string, data:any){
    const modal = this.modals.find(x=>x.Id===id);
    modal?.open(data);
  }

  public close(id:string, data:any){
    const modal = this.modals.find(x=>x.Id===id);
    modal?.close(data);
  }

  public dataIn(id:string){
    const modal = this.modals.find(x=>x.Id===id);
    return modal?.DataIn;
  }

  public dataOut(id:string){
    const modal = this.modals.find(x=>x.Id===id);
    return modal?.DataOut;
  }

}

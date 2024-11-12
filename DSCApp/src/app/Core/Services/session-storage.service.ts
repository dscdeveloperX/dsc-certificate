import { EventEmitter, Injectable, Output } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageService {

  @Output() eventoMenu = new EventEmitter<any>();

  constructor() { }


  get(key:string):any{
    try{
      return JSON.parse( sessionStorage.getItem(key)!);
    }
    catch(e){
    console.error("session: " + e);
    }
    
  }
  set(key:string, data:any){
    try{
      sessionStorage.setItem(key, JSON.stringify(data));
    }
    catch(e){
    console.error("session: " + e);
    }
  }
  remove(key:string){
    try{
      sessionStorage.removeItem(key);
    }
    catch(e){
    console.error("session: " + e);
    }
  }
  clear(){
    try{
      sessionStorage.clear();
    }
    catch(e){
    console.error("session: " + e);
    }
  }

}
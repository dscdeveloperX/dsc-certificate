import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DscModalWsService {

  constructor() { }


  private Action:BehaviorSubject<string> = new BehaviorSubject<string>('');

  public getAction():Observable<string>{
 return this.Action.asObservable();
  }

  public setAction(data:string):void{
    this.Action.next(data);
  }

}

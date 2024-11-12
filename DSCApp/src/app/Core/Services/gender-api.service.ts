import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IGender } from '../Models/Entity/IGender';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { IGenderRequest } from '../Models/Request/IGenderRequest';

@Injectable({
  providedIn: 'root'
})
export class GenderApiService {

  private http = inject(HttpClient);
  constructor() { }

  public GenderRead(genderID:any, genderActive:any, page:number, quantity:number ):Observable<IDataResponse<IGender>>{
    return this.http.get<IDataResponse<IGender>>(`${urlBase}/gender/read?genderID=${genderID == null?'':genderID}&genderActive=${genderActive}&page=${page}&quantity=${quantity}`);
  }

  public GenderCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/gender/count`);
  }


  public GenderCreate(data:IGenderRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/gender/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public GenderUpdate(data:IGenderRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/gender/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public GenderDelete(genderID:string):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/gender/delete/${genderID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }

}

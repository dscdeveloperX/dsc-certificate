import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { urlBase } from '../Constans/api-params';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { IMaritalStatus } from '../Models/Entity/IMaritalStatus';
import { IMaritalStatusRequest } from '../Models/Request/IMaritalStatusRequest';

@Injectable({
  providedIn: 'root'
})
export class MaritalStatusApiService {
  private http = inject(HttpClient);
  constructor() { }
  
  public MaritalStatusRead(maritalStatusID:any, maritalStatusActive:any, page:number, quantity:number ):Observable<IDataResponse<IMaritalStatus>>{
    return this.http.get<IDataResponse<IMaritalStatus>>(`${urlBase}/marital-status/read?maritalStatusID=${maritalStatusID == null?'':maritalStatusID}&maritalStatusActive=${maritalStatusActive}&page=${page}&quantity=${quantity}`);
  }

  public MaritalStatusCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/marital-status/count`);
  }


  public MaritalStatusCreate(data:IMaritalStatusRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/marital-status/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public MaritalStatusUpdate(data:IMaritalStatusRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/marital-status/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public MaritalStatusDelete(maritalStatusID:string):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/marital-status/delete/${maritalStatusID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}

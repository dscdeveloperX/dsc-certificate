import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { IParameter } from '../Models/Entity/IParameter';
import { IParameterRequest } from '../Models/Request/IParameterRequest';


@Injectable({
  providedIn: 'root'
})
export class ParameterApiService {
  private http = inject(HttpClient);
  constructor() { }

  public ParameterRead(parameterID:any, parameterActive:any, page:number, quantity:number):Observable<IDataResponse<IParameter>>{
    return this.http.get<IDataResponse<IParameter>>(`${urlBase}/parameter/read?parameterID=${parameterID}&parameterActive=${parameterActive}&page=${page}&quantity=${quantity}`);
  }

  public ParameterCompanyRead(companyID:any, parameterActive:any, page:number, quantity:number ):Observable<IDataResponse<IParameter>>{
    return this.http.get<IDataResponse<IParameter>>(`${urlBase}/parameter/parameter-company-read?companyID=${companyID==null?'':companyID}&parameterActive=${parameterActive}&page=${page}&quantity=${quantity}`);
  }

  public ParameterCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/parameter/count`);
  }

  public ParameterCreate(data:IParameterRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/parameter/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public ParameterUpdate(data:IParameterRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/parameter/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public ParameterDelete(parameterID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/parameter/delete/${parameterID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}

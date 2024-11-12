import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { urlBase } from '../Constans/api-params';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { IOccupation } from '../Models/Entity/IOccupation';
import { IOccupationRequest } from '../Models/Request/IOccupationRequest';

@Injectable({
  providedIn: 'root'
})
export class OccupationApiService {

  private http = inject(HttpClient);
  constructor() { }

  public OccupationRead(occupationID:any, occupationActive:any, page:number, quantity:number ):Observable<IDataResponse<IOccupation>>{
    return this.http.get<IDataResponse<IOccupation>>(`${urlBase}/occupation/read?occupationID=${occupationID== null?'':occupationID}&occupationActive=${occupationActive}&page=${page}&quantity=${quantity}`);
  }

  public OccupationCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/occupation/count`);
  }


  public OccupationCreate(data:IOccupationRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/occupation/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public OccupationUpdate(data:IOccupationRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/occupation/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public OccupationDelete(occupationID:string):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/occupation/delete/${occupationID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}

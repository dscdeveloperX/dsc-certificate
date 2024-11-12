import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { urlBase } from '../Constans/api-params';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { ICity } from '../Models/Entity/ICity';
import { ICityRequest } from '../Models/Request/ICityRequest';


@Injectable({
  providedIn: 'root'
})
export class CityApiService {

  private http = inject(HttpClient);
  constructor() { }

  public CityRead(cityID:any, cityActive:any, page:number, quantity:number ):Observable<IDataResponse<ICity>>{
    return this.http.get<IDataResponse<ICity>>(`${urlBase}/city/read?cityID=${cityID}&cityActive=${cityActive}&page=${page}&quantity=${quantity}`);
  }

  
  public CityCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/city/count`);
  }


  public CityCreate(data:ICityRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/city/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public CityUpdate(data:ICityRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/city/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public CityDelete(cityID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/city/delete/${cityID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}

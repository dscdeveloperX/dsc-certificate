import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { IProvince } from '../Models/Entity/IProvince';
import { ICity } from '../Models/Entity/ICity';
import { IProvinceRequest } from '../Models/Request/IProvinceRequest';
import { IProvinceCity } from '../Models/Entity/IProvinceCity';


@Injectable({
  providedIn: 'root'
})
export class ProvinceApiService {

  private http = inject(HttpClient);
  constructor() { }

  public ProvinceRead(cityID:any, cityActive:any, page:number, quantity:number ):Observable<IDataResponse<IProvinceCity>>{
    return this.http.get<IDataResponse<IProvinceCity>>(`${urlBase}/province/read?provinceID=${cityID}&provinceActive=${cityActive}&page=${page}&quantity=${quantity}`);
  }

  public ProvinceCityRead(provinceID:any, provinceActive:any , page:number, quantity:number):Observable<IDataResponse<IProvinceCity>>{
    return this.http.get<IDataResponse<IProvinceCity>>(`${urlBase}/province/province-city-read?provinceID=${provinceID}&provinceActive=${provinceActive}&page=${page}&quantity=${quantity}`);
  }

  
  public ProvinceCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/province/count`);
  }


  public ProvinceCreate(data:IProvinceRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/province/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public ProvinceUpdate(data:IProvinceRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/province/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public ProvinceDelete(provinceID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/province/delete/${provinceID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}

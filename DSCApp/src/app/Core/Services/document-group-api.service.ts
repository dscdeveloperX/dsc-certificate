import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { urlBase } from '../Constans/api-params';
import { IDocumentGroup } from '../Models/Entity/IDocumentGroup';
import { IDataResponse } from '../Models/Response/IDataResponse';


@Injectable({
  providedIn: 'root'
})
export class DocumentGroupApiService  {

  private http = inject(HttpClient);
  constructor() { }

  public DocumentGroupRead(companyID:any, documentGroupType:string, documentGroupDateYear:any, documentGroupActive:any, page:number, quantity:number ):Observable<IDataResponse<IDocumentGroup>>{
    return this.http.get<IDataResponse<IDocumentGroup>>(`${urlBase}/document-group/read?companyID=${companyID}&documentGroupType=${documentGroupType}&documentGroupDateYear=${documentGroupDateYear}&documentGroupActive=${documentGroupActive}&page=${page}&quantity=${quantity}`);
  }

  


}

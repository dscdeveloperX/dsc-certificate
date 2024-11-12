import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupDocumentApiService } from 'src/app/Core/Services/group-document-api.service';
import { IGroupDocument } from 'src/app/Core/Models/Entity/IGroupDocument';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-group-document-create',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './group-document-create.component.html',
  styleUrls: ['./group-document-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class GroupDocumentCreateComponent {
  private groupDocumentApiService = inject(GroupDocumentApiService);

  public OnGroupDocumentCreate():void{
  let data:IGroupDocument = {
    CompanyID:'1000000000001',
    GroupDocumentType:'cert-721',
    GroupDocumentDate:new Date(1977,6,21),
    GroupDocumentDescription:'Rol mes N0 1 desde el 1 al 31 de enero 2023',
    GroupDocumentActive:true
  };
  this.groupDocumentApiService.GroupDocumentCreate(data).subscribe(
    {
      next:(data:IDataResponse<IGroupDocument>)=>{
        console.log(data);
      }
    }
  );
}
}
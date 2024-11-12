import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupDocumentApiService } from 'src/app/Core/Services/group-document-api.service';
import { IGroupDocument } from 'src/app/Core/Models/Entity/IGroupDocument';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-group-document-update',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './group-document-update.component.html',
  styleUrls: ['./group-document-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class GroupDocumentUpdateComponent {
  private groupDocumentApiService = inject(GroupDocumentApiService);

  public OnGroupDocumentUpdate():void{
  let data:IGroupDocument = {
    GroupDocumentID:1,
    CompanyID:'1000000000001',
    GroupDocumentType:'cert-777',
    GroupDocumentDate:new Date(1977,6,21),
    GroupDocumentDescription:'Rol mes N0 1 desde el 1 al 31 de enero 2023',
    GroupDocumentActive:true
  };
  this.groupDocumentApiService.GroupDocumentUpdate(data).subscribe(
    {
      next:(data:IDataResponse<IGroupDocument>)=>{
        console.log(data);
      }
    }
  );
}
}
import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupDocumentApiService } from 'src/app/Core/Services/group-document-api.service';
import { IGroupDocument } from 'src/app/Core/Models/Entity/IGroupDocument';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-group-document-read',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './group-document-read.component.html',
  styleUrls: ['./group-document-read.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class GroupDocumentReadComponent {
  private groupDocumentApiService = inject(GroupDocumentApiService);

  public Data:WritableSignal<IGroupDocument[]> = signal<IGroupDocument[]>([]);
  

  public OnGrupDocumentRead():void{
    this.groupDocumentApiService.GroupDocumentRead(null, null,1,1).subscribe(
      {
        next:(data:IDataResponse<IGroupDocument>)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }
  public OnGroupDocumentCompanyRead():void{
    this.groupDocumentApiService.GroupDocumentCompanyRead('1000000000001', null,1,1).subscribe(
      {
        next:(data:IDataResponse<IGroupDocument>)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }

 

}

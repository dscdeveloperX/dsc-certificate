import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupDocumentApiService } from 'src/app/Core/Services/group-document-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-group-document-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './group-document-delete.component.html',
  styleUrls: ['./group-document-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class GroupDocumentDeleteComponent {
  private groupDocumentApiService = inject(GroupDocumentApiService);

  public OnGroupDocumentDelete():void{
  this.groupDocumentApiService.GroupDocumentDelete(1).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        console.log(data);
      }
    }
  );
}
}

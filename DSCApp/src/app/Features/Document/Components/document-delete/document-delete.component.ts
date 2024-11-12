import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-document-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './document-delete.component.html',
  styleUrls: ['./document-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentDeleteComponent {
  private documentApiService = inject(DocumentApiService);

  public OnDocumentDelete():void{
  this.documentApiService.DocumentDelete(1).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        console.log(data);
      }
    }
  );
}
}

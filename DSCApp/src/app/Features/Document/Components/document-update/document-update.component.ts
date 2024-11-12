import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { IDocument } from 'src/app/Core/Models/Entity/IDocument';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-document-update',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './document-update.component.html',
  styleUrls: ['./document-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentUpdateComponent {
  private documentApiService = inject(DocumentApiService);

  public OnDocumentUpdate():void{
  let data:IDocument = {
    DocumentID:1,
    GroupDocumentID:1,
    DocumentType:'cert-777',
    PersonID:'0000000001',
    DocumentDateCreation:new Date(1977,6,21),
    DocumentActive:true
  };
  this.documentApiService.DocumentUpdate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        console.log(data);
      }
    }
  );
}
}
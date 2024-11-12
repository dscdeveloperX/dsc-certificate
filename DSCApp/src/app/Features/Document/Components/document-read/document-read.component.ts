import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { IDocument } from 'src/app/Core/Models/Entity/IDocument';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-document-read',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './document-read.component.html',
  styleUrls: ['./document-read.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentReadComponent {
  private documentApiService = inject(DocumentApiService);

  public Data:WritableSignal<IDocument[]> = signal<IDocument[]>([]);
  

  public OnDocumentRead():void{
    this.documentApiService.DocumentRead(null, null,1,1).subscribe(
      {
        next:(data:IDataResponse<IDocument>)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }
  

}

import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentTypePanelComponent } from '../../Components/document-type-panel/document-type-panel.component';


@Component({
  selector: 'app-document-type-index',
  standalone: true,
  imports: [CommonModule, DocumentTypePanelComponent],
  templateUrl: './document-type-index.component.html',
  styleUrls: ['./document-type-index.component.css']
})
export class DocumentTypeIndexComponent {

}

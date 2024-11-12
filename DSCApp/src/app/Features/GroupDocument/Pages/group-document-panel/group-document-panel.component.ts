import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupDocumentReadComponent } from '../../Components/group-document-read/group-document-read.component';
import { GroupDocumentCreateComponent } from '../../Components/group-document-create/group-document-create.component';
import { GroupDocumentDeleteComponent } from '../../Components/group-document-delete/group-document-delete.component';
import { GroupDocumentUpdateComponent } from '../../Components/group-document-update/group-document-update.component';


@Component({
  selector: 'app-group-document-panel',
  standalone: true,
  imports: [CommonModule, GroupDocumentReadComponent, GroupDocumentCreateComponent, GroupDocumentDeleteComponent, GroupDocumentUpdateComponent],
  templateUrl: './group-document-panel.component.html',
  styleUrls: ['./group-document-panel.component.css']
})
export class GroupDocumentPanelComponent {

}

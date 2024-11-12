import { Component, ComponentRef, ElementRef, OnInit, ViewChild, ViewContainerRef, ViewRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { TemplateHeaderComponent } from './Shared/Template/Pages/template-header/template-header.component';
import { TemplateBodyComponent } from './Shared/Template/Pages/template-body/template-body.component';
import { DocumentUserPanelComponent } from './Features/Document/Pages/document-user-panel/document-user-panel.component';
import { XmlGeneratePanelComponent } from './Features/Document/Pages/xml-generate-panel/xml-generate-panel.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet,TemplateHeaderComponent, TemplateBodyComponent, DocumentUserPanelComponent, XmlGeneratePanelComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
 
}

import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyPanelComponent } from '../../Components/company-panel/company-panel.component';



@Component({
  selector: 'app-company-index',
  standalone: true,
  imports: [CommonModule, CompanyPanelComponent],
  templateUrl: './company-index.component.html',
  styleUrls: ['./company-index.component.css']
})
export class CompanyIndexComponent {

}

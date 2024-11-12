import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyPersonCreateComponent } from '../../Components/company-person-create/company-person-create.component';
import { CompanyPersonDeleteComponent } from '../../Components/company-person-delete/company-person-delete.component';
import { CompanyPersonReadComponent } from '../../Components/company-person-read/company-person-read.component';
import { CompanyPersonUpdateComponent } from '../../Components/company-person-update/company-person-update.component';

@Component({
  selector: 'app-company-person-panel',
  standalone: true,
  imports: [CommonModule, CompanyPersonCreateComponent, CompanyPersonDeleteComponent, CompanyPersonReadComponent, CompanyPersonUpdateComponent],
  templateUrl: './company-person-panel.component.html',
  styleUrls: ['./company-person-panel.component.css']
})
export class CompanyPersonPanelComponent {

}

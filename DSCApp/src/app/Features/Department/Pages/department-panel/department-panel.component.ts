import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentCreateComponent } from '../../Components/department-create/department-create.component';
import { DepartmentDeleteComponent } from '../../Components/department-delete/department-delete.component';
import { DepartmentReadComponent } from '../../Components/department-read/department-read.component';
import { DepartmentUpdateComponent } from '../../Components/department-update/department-update.component';

@Component({
  selector: 'app-department-panel',
  standalone: true,
  imports: [CommonModule, DepartmentCreateComponent, DepartmentDeleteComponent, DepartmentReadComponent, DepartmentUpdateComponent],
  templateUrl: './department-panel.component.html',
  styleUrls: ['./department-panel.component.css']
})
export class DepartmentPanelComponent {

}

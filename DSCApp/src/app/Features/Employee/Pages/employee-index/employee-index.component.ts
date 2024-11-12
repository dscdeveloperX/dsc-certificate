import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeePanelComponent } from '../../Components/employee-panel/employee-panel.component';

@Component({
  selector: 'app-employee-index',
  standalone: true,
  imports: [CommonModule, EmployeePanelComponent],
  templateUrl: './employee-index.component.html',
  styleUrls: ['./employee-index.component.css']
})
export class EmployeeIndexComponent {

}

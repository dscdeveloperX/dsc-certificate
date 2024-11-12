import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaritalStatusPanelComponent } from '../../Components/marital-status-panel/marital-status-panel.component';

@Component({
  selector: 'app-marital-status-index',
  standalone: true,
  imports: [CommonModule, MaritalStatusPanelComponent],
  templateUrl: './marital-status-index.component.html',
  styleUrls: ['./marital-status-index.component.css']
})
export class MaritalStatusIndexComponent {

}

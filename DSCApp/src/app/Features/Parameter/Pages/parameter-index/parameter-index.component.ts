import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ParameterPanelComponent } from '../../Components/parameter-panel/parameter-panel.component';

@Component({
  selector: 'app-parameter-index',
  standalone: true,
  imports: [CommonModule, ParameterPanelComponent],
  templateUrl: './parameter-index.component.html',
  styleUrls: ['./parameter-index.component.css']
})
export class ParameterIndexComponent {

}

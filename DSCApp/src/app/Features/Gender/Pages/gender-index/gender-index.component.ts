import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GenderPanelComponent } from '../../Components/gender-panel/gender-panel.component';


@Component({
  selector: 'app-gender-index',
  standalone: true,
  imports: [CommonModule, GenderPanelComponent],
  templateUrl: './gender-index.component.html',
  styleUrls: ['./gender-index.component.css']
})
export class GenderIndexComponent {

}

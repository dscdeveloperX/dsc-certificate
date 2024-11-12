import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OccupationPanelComponent } from '../../Components/occupation-panel/occupation-panel.component';

@Component({
  selector: 'app-occupation-index',
  standalone: true,
  imports: [CommonModule, OccupationPanelComponent],
  templateUrl: './occupation-index.component.html',
  styleUrls: ['./occupation-index.component.css']
})
export class OccupationIndexComponent {

}

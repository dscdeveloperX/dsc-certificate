import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonPanelComponent } from '../../Components/person-panel/person-panel.component';

@Component({
  selector: 'app-person-index',
  standalone: true,
  imports: [CommonModule,PersonPanelComponent],
  templateUrl: './person-index.component.html',
  styleUrls: ['./person-index.component.css']
})
export class PersonIndexComponent {

}

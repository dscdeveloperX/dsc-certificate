import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityReadComponent } from '../../Components/city-read/city-read.component';
import { CityCreateComponent } from '../../Components/city-create/city-create.component';
import { CityUpdateComponent } from '../../Components/city-update/city-update.component';
import { CityDeleteComponent } from '../../Components/city-delete/city-delete.component';


@Component({
  selector: 'app-city-panel',
  standalone: true,
  imports: [CommonModule, CityReadComponent, CityCreateComponent, CityUpdateComponent, CityDeleteComponent],
  templateUrl: './city-panel.component.html',
  styleUrls: ['./city-panel.component.css']
})
export class CityPanelComponent {

}

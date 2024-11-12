import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProvinceReadComponent } from '../../Components/province-read/province-read.component';
import { ProvinceCreateComponent } from '../../Components/province-create/province-create.component';
import { ProvinceDeleteComponent } from '../../Components/province-delete/province-delete.component';
import { ProvinceUpdateComponent } from '../../Components/province-update/province-update.component';

@Component({
  selector: 'app-province-panel',
  standalone: true,
  imports: [CommonModule, ProvinceReadComponent, ProvinceCreateComponent, ProvinceDeleteComponent, ProvinceUpdateComponent],
  templateUrl: './province-panel.component.html',
  styleUrls: ['./province-panel.component.css']
})
export class ProvincePanelComponent {

}

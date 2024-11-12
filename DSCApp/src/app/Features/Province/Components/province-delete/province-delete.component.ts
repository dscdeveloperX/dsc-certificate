import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProvinceApiService } from 'src/app/Core/Services/province-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-province-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './province-delete.component.html',
  styleUrls: ['./province-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ProvinceDeleteComponent {
  private provinceApiService = inject(ProvinceApiService);

    public OnProvinceDelete():void{
    this.provinceApiService.ProvinceDelete(228).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          console.log(data);
        }
      }
    );
  }
}

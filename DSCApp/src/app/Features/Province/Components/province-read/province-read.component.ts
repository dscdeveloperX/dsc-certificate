import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProvinceApiService } from 'src/app/Core/Services/province-api.service';
import { IProvince } from 'src/app/Core/Models/Entity/IProvince';
import { IProvinceCity } from 'src/app/Core/Models/Entity/IProvinceCity';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-province-read',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './province-read.component.html',
  styleUrls: ['./province-read.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ProvinceReadComponent {

  private provinceApiService = inject(ProvinceApiService);

  public DataProvince:WritableSignal<IProvince[]> = signal<IProvince[]>([]);
  public DataProvinceCity:WritableSignal<IProvinceCity[]> = signal<IProvinceCity[]>([]);


  public OnProvinceRead():void{
    this.provinceApiService.ProvinceRead(null, null,1,100).subscribe(
      {
        next:(data:IDataResponse<IProvince>)=>{
          this.DataProvince?.set(data.Data);
          console.log(this.DataProvince());
        }
      }
    );
  }

  public OnProvinceCityRead():void{
    this.provinceApiService.ProvinceCityRead(null, null,1,100).subscribe(
      {
        next:(data:IDataResponse<IProvinceCity>)=>{
          this.DataProvinceCity?.set(data.Data);
          console.log(this.DataProvinceCity());
        }
      }
    );
  }

}

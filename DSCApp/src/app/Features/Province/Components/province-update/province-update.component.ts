import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProvinceApiService } from 'src/app/Core/Services/province-api.service';
import { IProvince } from 'src/app/Core/Models/Entity/IProvince';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-province-update',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './province-update.component.html',
  styleUrls: ['./province-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ProvinceUpdateComponent {
  private provinceApiService = inject(ProvinceApiService);

  public OnProvinceUpdate():void{
  let data:IProvince = {
    ProvinceID:224,
    CityID:1,
    ProvinceName:'Laura City dscdsc',
    ProvinceActive:false
  };
  this.provinceApiService.ProvinceUpdate(data).subscribe(
    {
      next:(data:IDataResponse<IProvince>)=>{
        console.log(data);
      }
    }
  );
}
}

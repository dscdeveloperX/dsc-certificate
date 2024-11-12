import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProvinceApiService } from 'src/app/Core/Services/province-api.service';
import { IProvince } from 'src/app/Core/Models/Entity/IProvince';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-province-create',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './province-create.component.html',
  styleUrls: ['./province-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ProvinceCreateComponent {

  private provinceApiService = inject(ProvinceApiService);

  public OnProvinceCreate():void{
  let data:IProvince = {
    CityID:1,
    ProvinceName:'Laura province',
    ProvinceActive:false
  };
  this.provinceApiService.ProvinceCreate(data).subscribe(
    {
      next:(data:IDataResponse<IProvince>)=>{
        console.log(data);
      }
    }
  );
}
}

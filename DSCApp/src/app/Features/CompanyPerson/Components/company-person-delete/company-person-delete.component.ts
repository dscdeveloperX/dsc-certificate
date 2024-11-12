import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyPersonApiService } from 'src/app/Core/Services/company-person-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-company-person-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-person-delete.component.html',
  styleUrls: ['./company-person-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CompanyPersonDeleteComponent {
  private companyPersonApiService = inject(CompanyPersonApiService);

  public OnCompanyPersonDelete():void{
  this.companyPersonApiService.CompanyPersonDelete(2).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        console.log(data);
      }
    }
  );
}
}

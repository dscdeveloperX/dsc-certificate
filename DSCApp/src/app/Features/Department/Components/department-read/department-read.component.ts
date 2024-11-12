import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyApiService } from 'src/app/Core/Services/company-api.service';
import { DepartmentApiService } from 'src/app/Core/Services/department-api.service';
import { ICompany } from 'src/app/Core/Models/Entity/ICompany';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { IDepartment } from 'src/app/Core/Models/Entity/IDepartment';


@Component({
  selector: 'app-department-read',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './department-read.component.html',
  styleUrls: ['./department-read.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DepartmentReadComponent {
  private companyApiService = inject(CompanyApiService);
  private departmentApiService = inject(DepartmentApiService);

  public Data:WritableSignal<ICompany[]> = signal<ICompany[]>([]);
  

  public OnDepartmentRead():void{
    this.departmentApiService.DepartmentRead(null, null,1,1).subscribe(
      {
        next:(data:IDataResponse<ICompany>)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }


  /* PENDIENTE DE REVICION
  public OnCompanyDepartmentRead():void{
    this.companyApiService.CompanyDepartmentRead(null, null,1,1).subscribe(
      {
        next:(data:IData)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }
*/

  public OnCompanyRead():void{
    this.companyApiService.CompanyRead(null, null,1,1).subscribe(
      {
        next:(data:IDataResponse<ICompany>)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }

}

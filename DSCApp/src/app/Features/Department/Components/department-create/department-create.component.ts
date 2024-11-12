import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentApiService } from 'src/app/Core/Services/department-api.service';
import { IDepartment } from 'src/app/Core/Models/Entity/IDepartment';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-department-create',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './department-create.component.html',
  styleUrls: ['./department-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DepartmentCreateComponent {
  private departmentApiService = inject(DepartmentApiService);

  public OnDepartmentCreate():void{
  let data:IDepartment = {
    CompanyID:'0918753454001',
    DepartmentName:'Wilson Department',
    DepartmentActive:true
  };
  this.departmentApiService.DepartmentCreate(data).subscribe(
    {
      next:(data:IDataResponse<IDepartment>)=>{
        console.log(data);
      }
    }
  );
}
}
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TemplateMenuComponent } from '../template-menu/template-menu.component';
import { SessionStorageService } from 'src/app/Core/Services/session-storage.service';

@Component({
  selector: 'app-template-header',
  standalone: true,
  imports: [CommonModule, TemplateMenuComponent],
  templateUrl: './template-header.component.html',
  styleUrls: ['./template-header.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class TemplateHeaderComponent implements OnInit {

  public dataNavegacion_:Array<any> = [];
  public dataNavegacion:Array<any> = [];
  public onOffMenu:boolean=false;

  private router: Router;

  
  constructor(router:Router, private sessionStorageService:SessionStorageService, private changeDetectorRef:ChangeDetectorRef) { 
    this.router = router;
  }

  ngOnInit():void{}

  cargarMmenu():void{
    this.dataNavegacion = [];
    let sessionData = this.sessionStorageService.get('autorizacion');
    //inicio session
    if(sessionData){
      this.dataNavegacion =this.sessionStorageService.get('menu');
    }else{
      this.dataNavegacion = [];
    }    
  }

  navegarItemsMenu(enlace:string):void{
    this.onOffMenu = false;
    this.router.navigate([enlace]);
   
 }


 onOffMenuPrincipal():boolean{
  this.onOffMenu = !this.onOffMenu;
  if(this.onOffMenu){
    this.cargarMmenu();
  }
  return this.onOffMenu;
  this.changeDetectorRef.detectChanges();
  
 }


}
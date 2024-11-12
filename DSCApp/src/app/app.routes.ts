import { Routes } from '@angular/router';
import { DocumentPanelComponent } from './Features/Document/Pages/document-panel/document-panel.component';
import { XmlGeneratePanelComponent } from './Features/Document/Pages/xml-generate-panel/xml-generate-panel.component';

export const routes: Routes = [
    /*{
        path:'**',
        redirectTo:'auth'
    },*/
    {
        path:'',
        component: XmlGeneratePanelComponent,
        pathMatch:'full'
            
    }
/*
{
        path:'',
        component: PanelMenuComponent,
        pathMatch:'full'
            
    },    
    {
        path:'mobile-build/main',
        //component:InicioPanelComponent
        loadComponent:()=>import('./mobile-build/main/page/panel-main/panel-main.component').then(x=>x.PanelMainComponent)
    }
*/

];

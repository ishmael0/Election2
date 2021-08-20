import { APP_INITIALIZER, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { WebSelectorComponent } from '../../../../Santel/Core/ClientApp/src/app/components/web-selector/web-selector.component';
import { LoginComponent } from '../../../../Santel/Core/ClientApp/src/app/components/login/login.component';
import { SharedModule } from '../../../../Santel/Core/ClientApp/src/app/shared.module';
import { WebSiteService } from '../../../../Santel/Core/ClientApp/src/app/services/website.service';



const routes: Routes = [
  { path: '', redirectTo: 'vote', pathMatch: 'full' },
  {
    path: 'myaccelection2', loadChildren: () => import('../../../../Santel/Core/ClientApp/src/app/myacc/myacc.module').then(m => m.MyAcc),
    data: {
      key: 'MyAccElection2', label: 'مدیریت کاربران', isAcc: true
    }
  },
  {
    path: 'electiondb2', loadChildren: () => import('./Election2.module').then(m => m.Election2Module),
    data: { key: 'ElectionDB2', label: ' مدیریت وب سایت' }
  },
  { path: 'webselector', component: WebSelectorComponent },
  { path: 'login', component: LoginComponent },
  { path: 'vote', loadChildren: () => import('./vote.module').then(m => m.VoteModule)},
];

 

@NgModule({
  declarations: [
    AppComponent,
   ],
  imports: [
    RouterModule.forRoot(routes),
    SharedModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(wss: WebSiteService) {
    wss.logInDesc = '  وب سايت اخذ رای      ';
 }
}



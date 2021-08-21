import { NgModule } from '@angular/core';
import { buildPath, TemplateModule } from '../../../../Santel/Core/ClientApp/src/app/template/template.module';
import { RouterModule, Routes } from '@angular/router';
import { AuthService } from '../../../../Santel/Core/ClientApp/src/app/services/auth.service';
import { WebSiteService } from '../../../../Santel/Core/ClientApp/src/app/services/website.service';
import { ComponentTypes, EntityConfiguration, PropertyConfiguration, WebSitesConfiguration } from '../../../../Santel/Core/ClientApp/src/app/services/utils';
import { EngineersComponent } from './engineers.component';
import { CreateProperty, defaultPropertyConfiguration, defaultPropertyWithTitleConfiguration, IdProperty } from '../../../../Santel/Core/ClientApp/src/app/services/properties';
import { FormGroup, Validators } from '@angular/forms';
import { CityComponent } from './city.component';
import { StaticsComponent } from './statics/statics.component';
import { VotesComponent } from './votes/votes.component';


export const config: WebSitesConfiguration = new WebSitesConfiguration('ElectionDB2', 'مدیریت   ', '',
  [
 
    new EntityConfiguration(StaticsComponent, "Statics", "آمار ", {
      componentType: ComponentTypes.none,
    }),
    new EntityConfiguration(EngineersComponent, 'Engineer', "مهندسین", {
      componentType: ComponentTypes.lazytable,
      icon: '',
       getTitle: (item: FormGroup) => {
        let x = (item.controls.FirstName.value ? item.controls.FirstName.value : "") + " " + (item.controls.LastName.value ? item.controls.LastName.value : "")
        if (x && x != " ") return x;
        return "جدید";
      },
      propertiesConfigurations: [
        IdProperty,
        new PropertyConfiguration('Create', 'تاریخ ایجاد ', {
          Type: 'datetime',
          Validators: [],
          InPicker: false,
          InTable: false,
        }),
        new PropertyConfiguration('Status', ' وضعیت ', {
          InPicker: true,
          sortable: true,
          InTable: false,
          InSearch: false,
          Type: 'enum',
          value: 'Active',
        }),
        new PropertyConfiguration('FirstName', ' نام', { Type: 'string', Validators: [Validators.required], InTable: true }),
        new PropertyConfiguration('LastName', ' نام خانوادگی', { Type: 'string', Validators: [Validators.required], InTable: true }),
        new PropertyConfiguration('Shenasname', 'شماره شناسنامه ', { Type: 'string', Validators: [ ], InTable: true }),
        new PropertyConfiguration('Phone', 'شماره تماس ', { Type: 'string', Validators: [ ], InTable: true }),
        new PropertyConfiguration('Ozviat', 'شماره عضویت ', { Type: 'string', Validators: [ ], InTable: true }),
        new PropertyConfiguration('Parvane', ' شماره پروانه اشتغال', { Type: 'string', Validators: [ ], InTable: true }),
        new PropertyConfiguration('Licence', '   مدرک', { Type: 'string', Validators: [], InTable: true }),
        new PropertyConfiguration('IsOK', '   رای داده است؟', { Type: 'bool', Validators: [], InTable: true }),
      ]
    }),
    //new EntityConfiguration(EngineersComponent, 'Candidate', "Candidate", {
    //  componentType: ComponentTypes.table,
    //  icon: '',
    //  getTitle: (item: FormGroup) => {
    //    let x = (item.controls.FirstName.value ? item.controls.FirstName.value : "") + " " + (item.controls.LastName.value ? item.controls.LastName.value : "")
    //    if (x && x != " ") return x;
    //    return "جدید";
    //  },
    //  propertiesConfigurations: [
    //    IdProperty,
    //    new PropertyConfiguration('Create', 'تاریخ ایجاد ', {
    //      Type: 'datetime',
    //      Validators: [],
    //      InPicker: false,
    //      InTable: false,
    //    }),
    //    new PropertyConfiguration('Status', ' وضعیت ', {
    //      InPicker: true,
    //      sortable: true,
    //      InTable: false,
    //      InSearch: false,
    //      Type: 'enum',
    //      value: 'Active',
    //    }),
    //    new PropertyConfiguration('FirstName', ' نام', { Type: 'string', Validators: [Validators.required], InTable: true }),
    //    new PropertyConfiguration('LastName', ' نام خانوادگی', { Type: 'string', Validators: [Validators.required], InTable: true }),
    //    new PropertyConfiguration('Licence', '   Field', { Type: 'string', Validators: [], InTable: true }),
    //  ]
    //}),
    new EntityConfiguration(VotesComponent, 'Vote', "Vote", {
      componentType: ComponentTypes.lazytable,
      icon: '',
      canAdd: false, canEdit: false, canDelete: false, canSave:false,
      getTitle: (item: FormGroup) => {
        let x = (item.controls.FirstName.value ? item.controls.FirstName.value : "") + " " + (item.controls.LastName.value ? item.controls.LastName.value : "")
        if (x && x != " ") return x;
        return "جدید";
      },
      propertiesConfigurations: [
        IdProperty,
        new PropertyConfiguration('Create', 'تاریخ ایجاد ', {
          Type: 'datetime',
          Validators: [],
          InPicker: false,
          InTable: false,
        }),
        new PropertyConfiguration('Status', ' وضعیت ', {
          InPicker: true,
          sortable: true,
          InTable: false,
          InSearch: false,
          Type: 'enum',
          value: 'Active',
        }),
        new PropertyConfiguration('FirstName', ' نام', { Type: 'string', Validators: [Validators.required], InTable: true }),
        new PropertyConfiguration('LastName', ' نام خانوادگی', { Type: 'string', Validators: [Validators.required], InTable: true }),
        new PropertyConfiguration('EngineerId', ' شناسه', { Type: 'number', Validators: [Validators.required], InTable: true }),
        new PropertyConfiguration('CandidaesId1', ' آرای اعضای اصلی ', { Type: 'list', Validators: [Validators.required], InTable: true }),
        new PropertyConfiguration('CandidaesId2', ' آرای بازرسین ', { Type: 'list', Validators: [Validators.required], InTable: true }),
      ]
    }),



  ]
);
const routes: Routes = [
  ...config.entitiesConfiguration.map(c => ({ path: c.key.toLowerCase(), component: c.component, canActivate: [AuthService] }))
];


@NgModule({
  declarations: [
    CityComponent, EngineersComponent, StaticsComponent, VotesComponent
  ],
  imports: [
    TemplateModule, 
    RouterModule.forChild(buildPath(routes, true))
  ]
})
export class Election2Module {
  constructor(wss: WebSiteService) {
    wss.websites.push(config);
    wss.selectedWebsite = config;
  }
}
//FirstName
//LastName  
//Gender  
//RegisterDate  
//Ofice  
//MelliCode  
//RegisterCode  
//PhoneNumber  
//ParvaneCode  
//RegisterType  

import { ChangeDetectionStrategy, Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../Santel/Core/ClientApp/src/app/template/base/base.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-city',
  templateUrl: './city.component.html',
  styles: [
  ]
})
export class CityComponent extends BaseComponent {

  constructor(public injector: Injector) {
    super(injector, 'City');
  }
}


import { ChangeDetectionStrategy, Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../../Santel/Core/ClientApp/src/app/template/base/base.component';

@Component({
  selector: 'app-votes',
  templateUrl: './votes.component.html',
  styles: [
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VotesComponent extends BaseComponent {

  constructor(public injector: Injector) {
    super(injector, 'Vote');
  }
}

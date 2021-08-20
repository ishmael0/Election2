import { ChangeDetectionStrategy, Component, Injector } from "@angular/core";
import { HTTPTypes, NZNotificationTypes, RequestPlus } from "../../../../Santel/Core/ClientApp/src/app/services/utils";
import { BaseComponent } from "../../../../Santel/Core/ClientApp/src/app/template/base/base.component";

@Component({
  selector: 'app-engineers',
  templateUrl: './engineers.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EngineersComponent extends BaseComponent {
  constructor(public injector: Injector) {
    super(injector, 'Engineer');
  }
 
  async ResetVote() {
    this.loading = true;
    let x = await this.http.AddAndTry(new RequestPlus(HTTPTypes.POST, this.key(), {
      action: "ResetVote",
       formData: this.selectedForm().getRawValue(),
      onSuccess: (m, d) => {
        this.selectedForm().controls.IsOK.setValue(false);
        this.loading = false;
        this.http.createNotification(NZNotificationTypes.success, "تایید", "با موفقیت ذخیره شد");
      },
      onError: (m) => {
        this.http.createNotification(NZNotificationTypes.error, "خطا", m?.message);
        this.loading = false;
      }
    }));
    this.cdr.detectChanges();
  }
}

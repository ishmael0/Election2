import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { LegendPosition } from '@swimlane/ngx-charts';
import { HttpRequestService } from '../../../../../Santel/Core/ClientApp/src/app/services/http-request.service';
import { HTTPTypes, RequestPlus } from '../../../../../Santel/Core/ClientApp/src/app/services/utils';
var tb = `<!DOCTYPE html>
<html>
<head>
<link href="http://cdn.font-store.ir/yekan.css" rel="stylesheet">
<style>
*{font-family: yekan}



h2{text-align:center;}
table {
direction:rtl;
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td, th {
  border: 1px solid #dddddd;
  padding: 8px;
}

tr:nth-child(even) {
  background-color: #dddddd;
}
</style>
</head>
<body>

<h2>   
   آمار  رای گیری سازمان نظام کاردانی ساختمان استان ایلام
</h2>
<table>
   <tr>
        <th>رديف</th>
        <th>
          نام
        </th>
        <th>
          نام خانوادگی
        </th>
        <th>
          رشته
        </th>
        <th>
          تعداد آرا
        </th>
      </tr>
{{0}}
</table>

</body>
</html>
`;
@Component({
  selector: 'app-statics',
  templateUrl: './statics.component.html',
  styles: [
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StaticsComponent implements OnInit {

  print() {
    let newWin = window.open();
    let x = "";
    for (var i = 0; i < this.dataSet1.length; i++) {
      x = x + "<tr><td>" + this.dataSet1[i].Id + "</td><td>" + this.dataSet1[i].FirstName + "</td><td>" + this.dataSet1[i].LastName + "</td><td>" + this.dataSet1[i].Field + "</td><td>" + this.dataSet1[i].Count + "</td></tr>"
    }
    x = x + "<tr><td colspan = '4'> جمع کل </td><td>" + this.all1+"</td></tr> ";
    for (var i = 0; i < this.dataSet2.length; i++) {
      x = x + "<tr><td>" + this.dataSet2[i].Id + "</td><td>" + this.dataSet2[i].FirstName + "</td><td>" + this.dataSet2[i].LastName + "</td><td>" + this.dataSet2[i].Field + "</td><td>" + this.dataSet2[i].Count + "</td></tr>"
    }
    x = x + "<tr><td colspan = '4'> جمع کل </td><td>" + this.all2 + "</td></tr> ";
    newWin.document.write(tb.replace("{{0}}",x));
    newWin.print();
    //newWin.close();
  }



  async ngOnInit() {
    await this.get();
  }
  async get() {
    this.ok = false;
    await this.http.AddAndTry(new RequestPlus(HTTPTypes.GET, 'electiondb2/Statics', {
      action: 'View', onSuccess: (m, d) => {
        for (var i = 0; i < d.candidates1.length; i++) {
          d.candidates1[i].Count = d.res1.find(c => c.key == d.candidates1[i].Id)?.Count
        }
        for (var i = 0; i < d.candidates2.length; i++) {
          d.candidates2[i].Count = d.res2.find(c => c.key == d.candidates2[i].Id)?.Count
        }
        this.all1 = d.res1.reduce((sum, c) => sum + c.Count, 0);
        this.all2 = d.res2.reduce((sum, c) => sum + c.Count, 0);
        this.dataSet1 = d.candidates1;
        this.dataSet2 = d.candidates2;

        
        this.ok = true;
        this.cdr.detectChanges();
      }
    }));  

  }
  dataSet1 = [];
  dataSet2 = [];
  all1 = 0;
  all2 = 0;
  ok = false;

  constructor(public http: HttpRequestService, public cdr: ChangeDetectorRef) {
    //  Object.assign(this, { single });
  }

  onSelect(data): void {
   // console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data): void {
    //console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data): void {
    //console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }
}


import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { HttpRequestService } from '../../../../../Santel/Core/ClientApp/src/app/services/http-request.service';
import { HTTPTypes, RequestPlus } from '../../../../../Santel/Core/ClientApp/src/app/services/utils';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';

interface IForm {
  level: number;
  ozviyat: string;
  FirstName: string;
  LastName: string;
  token: string;
  code: string;
  phone: string;
  message: string;
  type: any;
  candidates1: any[];
  candidates2: any[];
  max1: number;
  max2: number;
}
@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styles: [
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VoteComponent implements OnInit {

  constructor(public http: HttpClient, public cdr: ChangeDetectorRef) { }
  ngOnInit(): void {
    this.clock();
    let x = localStorage.getItem('___data');
    if (x) {
      this.form = JSON.parse(x);
    }
  }
  saveStorage() {
    let x = localStorage.setItem('___data', JSON.stringify(this.form));
  }
  match() {
    if (!this.form.ozviyat) return false;
    if (this.form.ozviyat.length < 6) return false;
    if (!this.form.ozviyat.match(/^[0-9]{6,14}$/g)) return false;
    // if (!this.form.ozviyat.match(/^[0-9-]{6,14}$/g)) return false;
    return true;
  }
  exit() {
    this.form.level = 0;
    this.form.level = 0;
    this.form.phone = '';
    this.form.FirstName = '__';
    this.form.LastName = '__';
    this.form.token = '';
    this.form.code = '';
    this.form.message = '';
    this.form.type = '';
    this.form.ozviyat = '';
    this.form.candidates1 = [];
    this.form.candidates2 = [];
    this.form.max1 = 7;
    this.form.max2 = 1
  }
  counter: { min: number, sec: number }
  startTimer() {
    this.counter = { min: 2, sec: 0 } // choose whatever you want
    let intervalId = setInterval(() => {
      if (this.counter.sec - 1 == -1) {
        this.counter.min -= 1;
        this.counter.sec = 59
      }
      else this.counter.sec -= 1
      if (this.counter.min === 0 && this.counter.sec == 0) {
        clearInterval(intervalId);
        this.form.type = "error";
        this.form.message = "اعتبار پیامک منقضی شده است، لطفا مجددا تلاش کنید";
        this.exit();
      }
    }, 1000)
  }
  async goto1() {
    if (this.loading) return;
    this.loading = true;
    let d: any = await this.http.get("api/Election/l1?o=" + this.form.ozviyat).toPromise();
    if (d && d.status == 200) {
      this.form.type = d.data.type;
      this.form.message = d.data.message;
      if (this.form.type == "success") {
        this.form.level = 1;
        this.form.phone = d.data.phone;
        this.startTimer();
      }
      this.loading = false;
    }
    else {
      this.form.type = "error";
      this.form.message = "مشکلی روی داده است، وضعیت اینترنت خود را بررسی کنید";
    }
    this.cdr.detectChanges();
  }


  async goto2() {
    if (this.loading) return;
    this.loading = true;
    let d: any = await this.http.get("api/Election/l2?o=" + this.form.ozviyat + "&m=" + this.form.code).toPromise();
    if (d && d.status == 200) {
      this.form.type = d.data.type;
      this.form.message = d.data.message;
      if (this.form.type == "success") {
        this.form.token = d.data.token;
        this.form.candidates1 = d.data.candidates1;
        this.form.candidates2 = d.data.candidates2;
        this.form.max1 = d.data.max1;
        this.form.max2 = d.data.max2;
        this.form.FirstName = d.data.FirstName;
        this.form.LastName = d.data.LastName;
        this.form.level = 2;
      }
      this.loading = false;
    }
    else {
      this.form.type = "error";
      this.form.message = "مشکلی روی داده است، وضعیت اینترنت خود را بررسی کنید";
    }
    this.cdr.detectChanges();
  }
  oktovote() {
    if (this.form.candidates1.filter(c => c.checked == true).length > this.form.max1) return false;
    if (this.form.candidates2.filter(c => c.checked == true).length > this.form.max2) return false;
    return true;
  }
  async goto3() {
    if (this.loading) return;
    this.loading = true;
    let d: any = await this.http.post("api/Election/l3?o=" + this.form.ozviyat + "&m=" + this.form.code + "&token=" + this.form.token, {
      v1: this.form.candidates1.filter(c => c.checked == true).map(c => c.Id),
      v2: this.form.candidates2.filter(c => c.checked == true).map(c => c.Id),
    }).toPromise();
    if (d && d.status == 200) {
      this.form.type = d.data.type;
      this.form.message = d.data.message;
      if (this.form.type == "success") {
        this.form.level = 3;
      }
      this.loading = false;
    }
    else {
      this.form.type = "error";
      this.form.message = "مشکلی روی داده است، وضعیت اینترنت خود را بررسی کنید";
    }
    this.confirmDialog = false;
    this.cdr.detectChanges();


  }
  maxSelected(t) {
    return this.form["candidates" + t].filter(c => c.checked == true).length;
  }

  confirmDialog = false;
  form: IForm = {
    level: 0,
    phone: '',
    FirstName: '__',
    LastName: '__',
    token: '',
    code: '',
    message: '',
    type: '',
    ozviyat: '',
    candidates1: [],
    candidates2: [],
    max1: 7,
    max2: 1
  };
  clock() {
    setInterval(() => {
      let temp = new Date();
      let date = temp.toLocaleDateString('fa')
      this.time =
        temp.getHours().toString().padStart(2, '0') + ":"
        + temp.getMinutes().toString().padStart(2, '0') + ":"
        + temp.getSeconds().toString().padStart(2, '0');
      this.cdr.detectChanges();
      console.log(0);
    }, 1000);
  }
  time = "";
  loading = false;
}

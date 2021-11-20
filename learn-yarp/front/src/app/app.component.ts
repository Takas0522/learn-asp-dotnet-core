import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { PublicClientApplication } from '@azure/msal-browser';
import { environment } from 'src/environments/environment';
import { MsalService } from './msal.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {



  constructor(
    private httpClinet: HttpClient,
    private msalService: MsalService
  ) {
    this.msalService.login();
  }


  get() {
    this.httpClinet.get('/api/proxy-one-api/proxyone').subscribe(s => {
      console.log(s)
    })
  }

  getQuery() {
    this.httpClinet.get('/api/proxy-one-api/proxyone/withquery', { params: { 'name': 'hoge' } }).subscribe(s => {
      console.log(s)
    })
  }

  getId() {
    this.httpClinet.get('/api/proxy-one-api/proxyone/2').subscribe(s => {
      console.log(s)
    })
  }

  post() {
    const sendData = { hoge: 'a', fuga: 'b', piyo: 'c' }
    this.httpClinet.post('/api/proxy-one-api/proxyone', sendData).subscribe(s => {
      console.log(s)
    })
  }

  postFormData() {
    const sendData = new FormData();
    sendData.append('hoge', 'a');
    sendData.append('fuga', 'b');
    sendData.append('piyo', 'c');
    // const headees = new HttpHeaders();
    // headees.append('Content-Type', 'multipart/form-data');
    this.httpClinet.post('/api/proxy-one-api/proxyone/withform', sendData).subscribe(s => {
      console.log(s)
    })
  }

  postFileData(target: any) {
    const items: FileList = target.files;
    if (items.length <= 0) return;
    const sendData = new FormData();
    const len = items.length;
    for (let index = 0; index < len; index++) {
      const d = items[index]
      if (d != null) {
        sendData.append('datas', d, d.name);
      }
    }
    this.httpClinet.post('/api/proxy-one-api/proxyone/withfile', sendData).subscribe(s => {
      console.log(s)
    })
  }

  postFileAndParamData(target: any) {
    const items: FileList = target.files;
    if (items.length <= 0) return;
    const sendData = new FormData();
    const len = items.length;
    for (let index = 0; index < len; index++) {
      const d = items[index]
      if (d != null) {
        sendData.append('datas', d, d.name);
      }
    }
    sendData.append('hoge', 'a');
    sendData.append('fuga', 'b');
    sendData.append('piyo', 'c');
    this.httpClinet.post('/api/proxy-one-api/proxyone/files-and-param', sendData).subscribe(s => {
      console.log(s)
    })
  }

  putData() {
    const sendData = { hoge: 'a', fuga: 'b', piyo: 'c' }
    this.httpClinet.put('/api/proxy-one-api/proxyone', sendData).subscribe(s => {
      console.log(s)
    })
  }

  delete() {
    this.httpClinet.delete('/api/proxy-one-api/proxyone/1').subscribe(s => {
      console.log(s)
    })
  }

  deleteWithBody() {
    const sendData = { hoge: 'a', fuga: 'b', piyo: 'c' }
    this.httpClinet.request('delete', '/api/proxy-one-api/proxyone/multi', { body: sendData }).subscribe(s => {
      console.log(s)
    });
  }

}

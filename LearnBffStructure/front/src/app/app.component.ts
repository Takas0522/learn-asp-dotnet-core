import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountInfo, PublicClientApplication } from '@azure/msal-browser';
import { AdalService } from 'adal-angular4';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'FrontApp';

  private msalClient = new PublicClientApplication({
    auth: {
      clientId: '<V2アプリのClientId>',
      authority: 'https://login.microsoftonline.com/<TenantID>',
      redirectUri: 'http://localhost:4200/',
    }
  });

  private account: null | AccountInfo = null;
  constructor(
    private httpClient: HttpClient
  ) {}

  ngOnInit(): void {
    this.applicationInit();
  }

  private async applicationInit(): Promise<void> {
    const res = await this.msalClient.handleRedirectPromise();
    if (!res) {
      this.msalClient.loginRedirect();
    }
    if (res) {
      this.account = res.account;
    }
  }

  async accessWebApiOne(): Promise<void> {
    if (this.account) {
      const res = await this.msalClient.acquireTokenPopup({account: this.account, scopes: ['api://4a5e0de5-79b6-4890-b81b-a647678d7b30/access']});
      const header = new HttpHeaders().append('Authorization', `Bearer ${res.accessToken}`);
      this.httpClient.get('api/bff/one/one', { headers: header }).subscribe(x => {
        console.log(x);
      });
    }
  }

  async accessWebApiTwo(): Promise<void> {
    if (this.account) {
      const res = await this.msalClient.acquireTokenPopup({account: this.account, scopes: ['api://4a5e0de5-79b6-4890-b81b-a647678d7b30/access']});
      const header = new HttpHeaders().append('Authorization', `Bearer ${res.accessToken}`);
      this.httpClient.get('api/bff/two/two', { headers: header }).subscribe(x => {
        console.log(x);
      });
    }
  }

}

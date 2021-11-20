import { Injectable } from '@angular/core';
import { AccountInfo, PublicClientApplication } from '@azure/msal-browser';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MsalService {

  private msalClient = new PublicClientApplication(environment.msalConfig);
  private account: AccountInfo | null = null;

  async login() {
    const res = await this.msalClient.handleRedirectPromise();
    if (res == null) {
      this.msalClient.loginRedirect({ scopes: environment.scopes });
      return;
    }
    console.log(res.account)
    this.account = res.account;
  }

  async acquireToken() {
    if (this.account == null) {
      const popres = await this.msalClient.acquireTokenPopup({ scopes: environment.scopes })
      return popres.accessToken;
    }
    const res = await this.msalClient.acquireTokenSilent({ scopes: environment.scopes, account: this.account })
    return res.accessToken;
  }
}
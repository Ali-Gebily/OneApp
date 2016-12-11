import { Injectable, EventEmitter } from '@angular/core';
import { AuthenticationData } from '../models'
import { OneAppUIService } from './oneAppUI.service';
import { RuleEntityScope } from '../../../pages/styles/models'
@Injectable()
export class OneAppAuthenticationService {
  /**
   *
   */
  authenticationChanged: EventEmitter<AuthenticationData> = new EventEmitter<AuthenticationData>();

  constructor( private oneAppUIService: OneAppUIService) {
  }
  private localStorageService = window.localStorage;
  private storageKey = 'authorizationData';
  private _authentication: AuthenticationData = new AuthenticationData();


  public setAuthentication(authenticationData: AuthenticationData): void {
    if (authenticationData) {
      this._authentication.isAuthenticated = authenticationData.isAuthenticated;
      this._authentication.username = authenticationData.username;
      this._authentication.access_token = authenticationData.access_token;
      this._authentication.token_type = authenticationData.token_type;
      this._authentication.expires_in = authenticationData.expires_in;

      localStorage.setItem(this.storageKey, JSON.stringify(this._authentication));
    }
    else {
      this._authentication.isAuthenticated = false;
      this._authentication.username = null;
      this._authentication.access_token = null;
      this._authentication.token_type = null;
      this._authentication.expires_in = null;

      this.localStorageService.removeItem(this.storageKey);
      this.oneAppUIService.removeUserStyleIfExists(RuleEntityScope.User)


    }
    this.authenticationChanged.emit(this._authentication);

  }
  public loadData(): boolean {
    var authenticationDataStr = localStorage.getItem(this.storageKey);//returns string
    if (authenticationDataStr != null) {
      var authenticationData: AuthenticationData = JSON.parse(authenticationDataStr);//parse string to object
      this.setAuthentication(authenticationData);
      return true;
    }
    else {
      this.setAuthentication(null);
      return false;
    }

  }

  public getAuthenticationData(): AuthenticationData {
    return this._authentication
  };


}
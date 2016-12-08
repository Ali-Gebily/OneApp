import { Injectable } from '@angular/core';
import { AuthenticationData } from '../models/authenticationData'
import { OneAppNavigationService } from './oneAppNavigation.service';

@Injectable()
export class OneAppAuthenticationService {
  /**
   *
   */
  constructor(private oneAppNavigationService: OneAppNavigationService) {
  }
  private localStorageService = window.localStorage;
  private storageKey = 'authorizationData';
  private _authentication: AuthenticationData = new AuthenticationData();

  clearAuthenticationData(): void {
    this.localStorageService.removeItem(this.storageKey);
    this._authentication.isAuth = false;
    this._authentication.username = null;
    this._authentication.access_token = null;
    this._authentication.token_type = null;
    this._authentication.expires_in = null;

  };


  public setAuthentication(authenticationData: AuthenticationData): void {
    if (authenticationData) {
      this._authentication.isAuth = authenticationData.isAuth;
      this._authentication.username = authenticationData.username;
      this._authentication.access_token = authenticationData.access_token;
      this._authentication.token_type = authenticationData.token_type;
      this._authentication.expires_in = authenticationData.expires_in;

      localStorage.setItem(this.storageKey, JSON.stringify(this._authentication));
    }
    else {
      this.clearAuthenticationData();
    }

  }
  public loadData(): void {
    var authenticationDataStr = localStorage.getItem(this.storageKey);//returns string
    if (authenticationDataStr != null) {
      var authenticationData: AuthenticationData = JSON.parse(authenticationDataStr);//parse string to object

      this._authentication.isAuth = authenticationData.isAuth;
      this._authentication.username = authenticationData.username;
      this._authentication.access_token = authenticationData.access_token;
      this._authentication.token_type = authenticationData.token_type;
      this._authentication.expires_in = authenticationData.expires_in;
    }
  }
private LoadStyle(){
  
}
  public getAuthenticationData(): AuthenticationData {
    return this._authentication
  };


}
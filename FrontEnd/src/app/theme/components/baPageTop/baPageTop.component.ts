import { Component, ViewEncapsulation } from '@angular/core';

import { GlobalState } from '../../../global.state';

import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppNavigationService,OneAppUIService }  
from '../../../common/oneAppProxy/services';
import {BaPageTopService} from '../../services/baPageTop'


@Component({
  selector: 'ba-page-top',
  styles: [require('./baPageTop.scss')],
  template: require('./baPageTop.html'),
  encapsulation: ViewEncapsulation.None
})
export class BaPageTop {

  public isScrolled: boolean = false;
  public isMenuCollapsed: boolean = false;

  constructor(private _state: GlobalState,
    private oneAppAuthenticationService: OneAppAuthenticationService,
    private oneAppNavigationService: OneAppNavigationService,
    private baPageTopService: BaPageTopService
  ) {
    this._state.subscribe('menu.isCollapsed', (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });

  }
  public authentication = this.oneAppAuthenticationService.getAuthenticationData();

  public toggleMenu() {
    this.isMenuCollapsed = !this.isMenuCollapsed;
    this._state.notifyDataChanged('menu.isCollapsed', this.isMenuCollapsed);
    return false;
  } 
  public scrolledChanged(isScrolled) {
    this.isScrolled = isScrolled;
  }
  public logout($event) {
    $event.preventDefault();
   this.baPageTopService.logout();
  }
  public changePassword($event) {
    $event.preventDefault();
    this.oneAppNavigationService.NavigateTo("/pages/accounts/changePassword");

  }


}

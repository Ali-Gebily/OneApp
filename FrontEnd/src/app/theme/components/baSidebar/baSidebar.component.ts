import { Component, ElementRef, HostListener, ViewEncapsulation } from '@angular/core';
import { GlobalState } from '../../../global.state';
import { layoutSizes } from '../../../theme';
import { MENU } from '../../../../app/app.menu';
import * as _ from 'lodash';
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppUIService }
  from '../../../common/oneAppProxy/services';

import { AuthenticationData } from '../../../common/oneAppProxy/models';


@Component({
  selector: 'ba-sidebar',
  encapsulation: ViewEncapsulation.None,
  styles: [require('./baSidebar.scss')],
  template: require('./baSidebar.html')
})
export class BaSidebar {

  // here we declare which routes we want to use as a menu in our sidebar

  public routes = null;// _.cloneDeep(MENU); // we're creating a deep copy since we are going to change that object

  public menuHeight: number;
  public isMenuCollapsed: boolean = false;
  public isMenuShouldCollapsed: boolean = false;
  subscription: any;
  constructor(private _elementRef: ElementRef, private _state: GlobalState,
    private oneAppAuthenticationService: OneAppAuthenticationService) {

    if (this.authentication.isAuthenticated) {
      this.routes = _.cloneDeep(MENU);
    }
    else { this.routes = []; }


    this._state.subscribe('menu.isCollapsed', (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
    this.subscription = this.oneAppAuthenticationService.authenticationChanged
      .subscribe((item: AuthenticationData) => {
        if (item.isAuthenticated) {
          this.routes = _.cloneDeep(MENU);
        }
        else { this.routes = []; }
      }
      );

  }
  public authentication = this.oneAppAuthenticationService.getAuthenticationData();

  public ngOnInit(): void {
    if (this._shouldMenuCollapse()) {
      this.menuCollapse();
    }
  }

  public ngAfterViewInit(): void {
    setTimeout(() => this.updateSidebarHeight());
  }

  @HostListener('window:resize')
  public onWindowResize(): void {

    var isMenuShouldCollapsed = this._shouldMenuCollapse();

    if (this.isMenuShouldCollapsed !== isMenuShouldCollapsed) {
      this.menuCollapseStateChange(isMenuShouldCollapsed);
    }
    this.isMenuShouldCollapsed = isMenuShouldCollapsed;
    this.updateSidebarHeight();
  }

  public menuExpand(): void {
    this.menuCollapseStateChange(false);
  }

  public menuCollapse(): void {
    this.menuCollapseStateChange(true);
  }

  public menuCollapseStateChange(isCollapsed: boolean): void {
    this.isMenuCollapsed = isCollapsed;
    this._state.notifyDataChanged('menu.isCollapsed', this.isMenuCollapsed);
  }

  public updateSidebarHeight(): void {
    // TODO: get rid of magic 84 constant
    this.menuHeight = this._elementRef.nativeElement.childNodes[0].clientHeight - 84;
  }

  private _shouldMenuCollapse(): boolean {
    return window.innerWidth <= layoutSizes.resWidthCollapseSidebar;
  }
}

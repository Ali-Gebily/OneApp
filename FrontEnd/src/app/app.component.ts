import './app.loader.ts';
import { Component, ViewEncapsulation, ViewContainerRef } from '@angular/core';
import { GlobalState } from './global.state';
import { BaImageLoaderService, BaThemePreloader, BaThemeSpinner } from './theme/services';
import { layoutPaths } from './theme/theme.constants';
import { BaThemeConfig } from './theme/theme.config';
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppUIService } from './common/oneAppProxy/services';
import { RuleEntityScope } from './pages/styles/models'
/*
 * App Component
 * Top Level Component
 */
@Component({
  selector: 'app',
  encapsulation: ViewEncapsulation.None,
  styles: [require('normalize.css'), require('./app.scss')],
  template: `
    <main [ngClass]="{'menu-collapsed': isMenuCollapsed}" baThemeRun>
      <div class="additional-bg"></div>
      <router-outlet></router-outlet>
    </main>
  `,
  providers: []
})
export class App {

  isMenuCollapsed: boolean = false;

  constructor(private _state: GlobalState,
    private _imageLoader: BaImageLoaderService,
    private _spinner: BaThemeSpinner,
    private _config: BaThemeConfig,
    private viewContainerRef: ViewContainerRef,
    private oneAppAuthenticationService: OneAppAuthenticationService, 
    private oneAppHttpService: OneAppHttpService,
    private oneAppConfigurationService: OneAppConfigurationService,
    private oneAppUIService: OneAppUIService
  ) {

    this._loadImages();

    this._state.subscribe('menu.isCollapsed', (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
    oneAppUIService.configureNavigation();
    this.oneAppUIService.loadStyle(RuleEntityScope.Global,null, this.oneAppHttpService, this.oneAppConfigurationService);

    if (oneAppAuthenticationService.loadData()) {
      this.oneAppUIService.loadStyle(RuleEntityScope.User,null, this.oneAppHttpService, this.oneAppConfigurationService);
    }

  }

  public ngAfterViewInit(): void {
    // hide spinner once all loaders are completed
    BaThemePreloader.load().then((values) => {
      this._spinner.hide();
    });
  }

  private _loadImages(): void {
    // register some loaders
    BaThemePreloader.registerLoader(this._imageLoader.load(layoutPaths.images.root + 'sky-bg.jpg'));
  }
}

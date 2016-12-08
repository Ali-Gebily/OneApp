import './app.loader.ts';
import { Component, ViewEncapsulation, ViewContainerRef } from '@angular/core';
import { GlobalState } from './global.state';
import { BaImageLoaderService, BaThemePreloader, BaThemeSpinner } from './theme/services';
import { layoutPaths } from './theme/theme.constants';
import { BaThemeConfig } from './theme/theme.config'; 
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppNavigationService, OneAppUIService } from './common/oneAppProxy/services';

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
    private oneAppNavigationService: OneAppNavigationService,
    private oneAppHttpService: OneAppHttpService,
    private oneAppConfigurationService: OneAppConfigurationService
  ) {

    this._loadImages();

    this._state.subscribe('menu.isCollapsed', (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
    oneAppAuthenticationService.loadData();
    oneAppNavigationService.configureNavigation();
  BaThemePreloader.registerLoader(oneAppHttpService.get("/api/appStyle/getFormattedAppStyle?base_url="+
  this.oneAppConfigurationService.getCSSImageDownloadUrl()).then(function name(result: any) {
      console.log(result);
      var style = document.createElement('style');
      style.type = 'text/css';
      style.innerHTML = result;
      style.id="oneAppStyle";
      document.head.appendChild(style);
    }))
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

import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';

import { OneAppAuthenticationService } from './services/oneAppAuthentication.service';
import { OneAppHttpService } from './services/oneAppHttp.service';
import { OneAppNavigationService } from './services/oneAppNavigation.service';
import { OneAppConfigurationService } from './services/oneAppConfiguration.service';
import { OneAppUIService } from './services/oneAppUI.service';
import { NgaModule } from '../../theme/nga.module';


@NgModule({
  imports: [RouterModule, HttpModule,NgaModule],
  declarations: [],
  providers: [OneAppAuthenticationService, OneAppHttpService,
    OneAppNavigationService, OneAppConfigurationService,OneAppUIService]
})
export class OneAppProxyModule { }

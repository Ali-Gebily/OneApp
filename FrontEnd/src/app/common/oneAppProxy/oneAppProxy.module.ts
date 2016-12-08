import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router'; 
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppNavigationService, 
  OneAppUIService }
    from './services';

import { NgaModule } from '../../theme/nga.module';


@NgModule({
  imports: [RouterModule, HttpModule,NgaModule],
  declarations: [],
  providers: [OneAppAuthenticationService, OneAppHttpService,
    OneAppNavigationService, OneAppConfigurationService,OneAppUIService]
})
export class OneAppProxyModule { 
 
}

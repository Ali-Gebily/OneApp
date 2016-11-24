import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import { routing }       from './pages.routing';
import { NgaModule } from '../theme/nga.module';
import { OneAppProxyModule } from '../common/oneAppProxy/oneAppProxy.module';

import { Pages } from './pages.component';

@NgModule({
  imports: [CommonModule, NgaModule, routing, OneAppProxyModule],
  declarations: [Pages]
})
export class PagesModule {
}

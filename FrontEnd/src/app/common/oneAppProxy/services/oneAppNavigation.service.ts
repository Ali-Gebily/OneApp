import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalState } from '../../../global.state'
import { OneAppConfigurationService } from './oneAppConfiguration.service';

@Injectable()
export class OneAppNavigationService {
    /**
     *
     */
    constructor(private router: Router, private _state: GlobalState,
        private oneAppConfigurationService: OneAppConfigurationService) {
    }
    public NavigateToHome(): void {
        this.router.navigateByUrl(this.oneAppConfigurationService.HomeUrl);
    }
    public NavigateToLogin(): void {
        this.router.navigateByUrl(this.oneAppConfigurationService.LoginUrl);
    }

    public NavigateTo(url: string): void {
        this.router.navigateByUrl(url);
    }

    public configureNavigation(): void {
        this.router.events.subscribe(event => {
            console.log(event);
            if (event.constructor.name === 'NavigationStart') {
                // alert("navigate")

            }
            else if (event.constructor.name === 'NavigationEnd') {
                //close side bar
                this._state.notifyDataChanged('menu.isCollapsed', true);

            }
        });

    }



}
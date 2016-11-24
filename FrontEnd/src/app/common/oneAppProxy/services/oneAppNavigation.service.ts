import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { OneAppConfigurationService } from './oneAppConfiguration.service';
@Injectable()
export class OneAppNavigationService {
    /**
     *
     */
    constructor(private router: Router,
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
            if (event.constructor.name === 'NavigationStart') {
                // alert("navigate")
                console.log(event);

            }
        });
      
    }



}
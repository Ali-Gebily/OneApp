import { Injectable } from '@angular/core';

import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppNavigationService,OneAppUIService }  
from '../../../common/oneAppProxy/services';


@Injectable()
export class BaPageTopService {
    /**
     * 
     */
    constructor(private oneAppAuthenticationService: OneAppAuthenticationService,
        private oneAppNavigationService: OneAppNavigationService,
        private oneAppHttpService: OneAppHttpService) {

    }


    public logout(): void {
        var service = this;
        this.oneAppHttpService.post('api/account/logout', null).then(function () {
            service.oneAppAuthenticationService.clearAuthenticationData();
            service.oneAppNavigationService.NavigateToHome();
        });

    }
    public search() {

    }

}
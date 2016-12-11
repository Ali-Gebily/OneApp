import { Injectable } from '@angular/core';

import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService,OneAppUIService }  
from '../../../common/oneAppProxy/services';


@Injectable()
export class BaPageTopService {
    /**
     *  
     */
    constructor(private oneAppAuthenticationService: OneAppAuthenticationService,
        private oneAppUIService: OneAppUIService,
        private oneAppHttpService: OneAppHttpService) {

    }


    public logout(): void {
        var service = this;
        this.oneAppHttpService.post('api/accounts/logout', null).then(function () {
            service.oneAppAuthenticationService.setAuthentication(null);
            service.oneAppUIService.NavigateToHome();
        });

    }
    public search() {

    }

}
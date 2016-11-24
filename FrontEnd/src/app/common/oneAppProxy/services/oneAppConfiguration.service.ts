import { Injectable } from '@angular/core'; 
@Injectable()
export class OneAppConfigurationService {
    constructor() {
    }

    public BackEndServicePath: string ='http://localhost:55475/';// 'http://aligebily-001-site1.dtempurl.com/';//http://localhost:55475/';
     public HomeUrl: string ='/pages/home';
     public LoginUrl: string ='/pages/accounts/login';
}
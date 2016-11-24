import { Injectable } from '@angular/core';
import { Headers, Http, Response, Request } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { OneAppAuthenticationService } from './oneAppAuthentication.service';
import { OneAppConfigurationService } from './oneAppConfiguration.service';
import { OneAppNavigationService } from './oneAppNavigation.service';
import { OneAppUIService } from './oneAppUI.service';


@Injectable()
export class OneAppHttpService {
    constructor(
        private http: Http,
        private oneAppAuthenticationService: OneAppAuthenticationService,
        private oneAppConfigurationService: OneAppConfigurationService,
        private oneAppNavigationService: OneAppNavigationService,
        private oneAppUIService: OneAppUIService) {
    }

    private basePath = this.oneAppConfigurationService.BackEndServicePath;// '/'; 

    private makeRequest(method: string, url: string, data: any, options?: any): Promise<any> {

        method = method.toLowerCase();

        //start with the uri
        url = this.basePath + url;
        if (!options) {
            options = {};
        }


        /*The response object has these properties:
       data – {string|Object} – The response body transformed with the transform functions.
       status – {number} – HTTP status code of the response.
       headers – {function([headerName])} – Header getter function.
       options – {Object} – The optionsuration object that was used to generate the request.
       statusText – {string} – HTTP status text of the response.
       */
        //A response status code between 200 and 299 is considered a success status and will result in the success callback being called. 


        if (!options) {
            options = {};
        }
        if (!options.headers) {
            options.headers = {};
        }
        var authenticationData=this.oneAppAuthenticationService.getAuthenticationData();
        if (authenticationData.access_token) {
            options.headers['Authorization'] = authenticationData.token_type +
                ' ' + authenticationData.access_token;
        }

        var requestArgs: any = {};
        requestArgs.url = url;
        requestArgs.method = method;
        requestArgs.body = data;
        requestArgs.headers = options.headers;
        var request = new Request(requestArgs);
        var service = this;
       
        this.oneAppUIService.showLoading();
        return this.http.request(request, options).toPromise()
            .then(function handleSuccess(response: any): Promise<any> {
                service.oneAppUIService.hideLoading();
                console.log(response);
                var data = response.json();
                //in all cases except login, we well have response object with result field even it's null
                //so we will pass the result except in login, we will return full response
                if (data.result !== undefined) {
                    data = data.result;
                }
                return Promise.resolve(data);
            })
            .catch(function handleError(response: Response | any) {
                service.oneAppUIService.hideLoading();
                console.log(response);
                if (response.status == 401) {//UnAuthorized
                    service.oneAppAuthenticationService.clearAuthenticationData();
                    service.oneAppNavigationService.NavigateToLogin();
                }
                else {
                    var errorMessage = null;;
                    if (response.status == 0) { //
                        errorMessage = "Unexpected error: please check your connection to the server";
                    } else {
                        var data = response.json();
                        if (data) {
                            if (data.errors) {//normal errors
                                errorMessage = data.errors.map(function (elem) {
                                    return elem.message;
                                }).join("\n");
                            }
                            else if (data.error) {//invalid username or password: /token
                                errorMessage = data.error;
                            }
                            else if (data.Message) {//UnAuthorized
                                errorMessage = data.Message;
                            }
                        }
                        if (!errorMessage) {
                            errorMessage = "unexpected error:" + response.statusText + ", status=" + response.status;
                        }
                    }
                    service.oneAppUIService.showError(errorMessage);
                    return Promise.reject(response.message || response);
                }
            });
    }
    public get(url: string, options?: any): Promise<any> {
        return this.makeRequest('get', url, options);
    }
    public post(url: string, data: any, options?: any): Promise<any> {
        return this.makeRequest('post', url, data, options);
    }
    public put(url: string, data: any, options?: any): Promise<any> {
        return this.makeRequest('put', url, data, options);
    }
    public delete(url: string, options?: any): Promise<any> {
        return this.makeRequest('delete', url, options);
    }
    
};




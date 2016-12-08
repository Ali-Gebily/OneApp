// import { Injectable } from '@angular/core';
// import { Observable } from 'rxjs/Observable';
// import 'rxjs/add/operator/share';
// import { OneAppUIService, OneAppConfigurationService } from '../services'

// @Injectable()
// export class OneAppFileUploadService {
//     /**
//      * @param Observable<number>
//      */
//     private progress$: Observable<number>;

//     /**
//      * @type {number}
//      */
//     private progress: number = 0;

//     private progressObserver: any;

//     constructor(private oneAppUIService: OneAppUIService, private oneAppConfigurationService: OneAppConfigurationService) {
//         this.progress$ = new Observable<number>(observer => {
//             this.progressObserver = observer
//         });
//     }

//     /**
//      * @returns {Observable<number>}
//      */
//     public getObserver(): Observable<number> {
//         return this.progress$;
//     }

//     /**
//      * Upload files through XMLHttpRequest
//      *
//      * @param url
//      * @param files
//      * @returns {Promise<T>}
//      */
//     public upload(url: string, files: File[], customData: any): Promise<any> {
//         return new Promise((resolve, reject) => {
//             let formData: FormData = new FormData(),
//                 xhr: XMLHttpRequest = new XMLHttpRequest();

//             for (let i = 0; i < files.length; i++) {
//                 formData.append("uploads[]", files[i], files[i].name);
//             }
//             if (typeof (customData) == 'object') {
//                 formData.append("customData", JSON.stringify(customData))
//             }
//             else {
//                 formData.append("customData", customData)

//             }
//             let service: OneAppFileUploadService = this;
//             xhr.onreadystatechange = () => {
//                 console.log(xhr);
//                 service.oneAppUIService.hideLoading();
//                 if (xhr.readyState === 4) {
//                     if (xhr.status === 200) {
//                         var data = JSON.parse(xhr.response)
//                         //in all cases except login, we well have response object with result field even it's null
//                         //so we will pass the result except in login, we will return full response
//                         if (data.result !== undefined) {
//                             data = data.result;
//                         }
//                         resolve(data);
//                     } else {
//                         service.oneAppUIService.showError(xhr.response);//should be like http service
//                         reject(xhr.response);
//                     }
//                 }
//             };

//             OneAppFileUploadService.setUploadUpdateInterval(500);

//             xhr.upload.onprogress = (event) => {
//                 this.progress = Math.round(event.loaded / event.total * 100);
//                 //this.progressObserver.next(this.progress);
//             };

//             xhr.open('POST', url, true);
//             xhr.send(formData);
//             service.oneAppUIService.showLoading();
//         });
//     }

//     /**
//      * Set interval for frequency with which Observable inside Promise will share data with subscribers.
//      *
//      * @param interval
//      */
//     private static setUploadUpdateInterval(interval: number): void {
//         setInterval(() => { }, interval);
//     }
// }
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BaThemeSpinner } from '../../../theme/services';

@Injectable()
export class OneAppUIService {
    /**
     *
     */
    constructor(private baThemeSpinner: BaThemeSpinner) {

    }
    private _resolver;
    public showLoading() {
     this.baThemeSpinner.show();  
     
    }
    public hideLoading() {
     this.baThemeSpinner.hide();
    }
    public showMessage(msg: string) {
        alert(msg);
    }
    public showError(error: string) {
        this.showMessage(error);
    }
}
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BaThemeSpinner } from '../../../theme/services';
import { OneAppHttpService, OneAppConfigurationService } from "../services"
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

    private _styleSheetId: string= "oneAppStyle";
    public createStyleElement(id, innerHtml) {
        var style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = innerHtml;
        style.id = id;
        document.head.appendChild(style);
    }
    public updateStyleRule(selector, formattedRule) {
        var styleSheetId = this._styleSheetId;
        var oneAppStyleSheet = document.styleSheets[styleSheetId];
        if (oneAppStyleSheet == null) {
            this.createStyleElement(styleSheetId, formattedRule)
        } else {
            for (var i = 0, len = oneAppStyleSheet.cssRules.length; i < len; i++) {
                var cssRule = oneAppStyleSheet.cssRules[i];
                if (cssRule.selectorText == selector) {
                    oneAppStyleSheet.deleteRule(i);
                    console.log("deleted rule: " + cssRule.selectorText);
                    console.log(cssRule);
                    len = oneAppStyleSheet.cssRules.length;
                }
            }
            oneAppStyleSheet.insertRule(formattedRule, oneAppStyleSheet.cssRules.length);
            console.log(formattedRule);
        }
    }

    public loadStyle(oneAppHttpService: OneAppHttpService, oneAppConfigurationService: OneAppConfigurationService) {
        var service = this;
        oneAppHttpService.get("/api/appStyle/getFormattedAppStyle?base_url=" +
            oneAppConfigurationService.getCSSImageDownloadUrl()).then(function name(result: any) {
                console.log(result);
                service.createStyleElement(service._styleSheetId, result)
            });
    }
}
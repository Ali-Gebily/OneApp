import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalState } from '../../../global.state'
import { BaThemeSpinner } from '../../../theme/services';
import { OneAppConfigurationService } from "./oneAppConfiguration.service";
import { OneAppHttpService } from "./oneAppHttp.service"; 
import { RuleEntityScope } from "../../../pages/styles/models"

@Injectable()
export class OneAppUIService {
    /**
     *
     */
    constructor(private router: Router,
        private _state: GlobalState,
        private oneAppConfigurationService: OneAppConfigurationService,
        private baThemeSpinner: BaThemeSpinner) {

    }
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

    private getStyleSheetId(ruleEntityScope: RuleEntityScope) {
        return "oneAppStyle_" + ruleEntityScope;
    }
    public removeUserStyleIfExists(ruleEntityScope: RuleEntityScope) {
        let styleSheetId = this.getStyleSheetId(ruleEntityScope);;
        var oneAppStyleElement = document.getElementById(styleSheetId);
        if (oneAppStyleElement != null) {
            document.head.removeChild(oneAppStyleElement);
        }
    }
    private updateStyleElement(ruleEntityScope: RuleEntityScope, innerHtml: string) {
        this.removeUserStyleIfExists(ruleEntityScope);
        var style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = innerHtml;
        style.id = this.getStyleSheetId(ruleEntityScope);
        document.head.appendChild(style);
    }

    public updateStyleRule(ruleEntityScope: RuleEntityScope, selector: string, formattedRule) {
        var styleSheetId = this.getStyleSheetId(ruleEntityScope);
        var oneAppStyleSheet = document.styleSheets[styleSheetId];
        if (oneAppStyleSheet == null) {
            this.updateStyleElement(ruleEntityScope, formattedRule)
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

    public loadStyle(ruleEntityScope: RuleEntityScope, entityId: string, oneAppHttpService: OneAppHttpService, oneAppConfigurationService: OneAppConfigurationService) {
        var service = this;
        oneAppHttpService.get("/api/styles/getFormattedStyles?scope=" + ruleEntityScope + "&entity_id=" + (entityId ? entityId : "") + "&base_url=" +
            oneAppConfigurationService.getCSSImageDownloadUrl()).then(function name(result: any) {
                console.log(result);
                service.updateStyleElement(ruleEntityScope, result)
            });
    }
}
import { Injectable } from '@angular/core';
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppNavigationService, OneAppUIService }
    from '../../../common/oneAppProxy/services';
import { RuleModel } from '../models'

@Injectable()
export class AppStyleService {
    constructor(private oneAppHttpService: OneAppHttpService,
        private oneAppConfigurationService: OneAppConfigurationService) {
    }

    getRulesSummary(): Promise<any> {
        return this.oneAppHttpService.get("/api/appStyle/getRulesSummary");
    }
    getRule(id: number): Promise<any> {
        return this.oneAppHttpService.get("/api/appStyle/getRule/" + id);
    }
    updateRuleStyle(rule: RuleModel): Promise<any> {

        let formData: FormData = new FormData();
        for (var key in rule.style.files) {
            let file: File = rule.style.files[key];
            formData.append(key, file, file.name);

        }
        formData.append("rule", JSON.stringify(rule));
        formData.append("base_url", this.oneAppConfigurationService.getCSSImageDownloadUrl());
        return this.oneAppHttpService.postFormData("/api/appStyle/updateRuleStyle", formData);

    }
    getImage(id: string) {
        if (id == null || id == "") {
            return null;
        }
        return this.oneAppConfigurationService.getCSSImageDownloadUrl() + "/" + id;
    }
}
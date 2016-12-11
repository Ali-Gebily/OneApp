import { Injectable } from '@angular/core';
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppUIService }
    from '../../../common/oneAppProxy/services';
import { RuleModel, RuleEntityScope } from '../models'

@Injectable()
export class StylesService {
    constructor(private oneAppHttpService: OneAppHttpService,
        private oneAppConfigurationService: OneAppConfigurationService) {
    }

    getRulesSummary(scope: RuleEntityScope): Promise<any> {
        return this.oneAppHttpService.get("/api/styles/getRulesSummary?scope=" + scope);
    }
    getRuleDetails(id: number, entityId: string): Promise<any> {
        return this.oneAppHttpService.get("/api/styles/getRuleDetails?id=" + id + "&entity_id=" + (entityId ? entityId : ""));
    }
    updateRuleStyle(rule: RuleModel, entityId: string): Promise<any> {

        let formData: FormData = new FormData();
        for (var key in rule.style.files) {
            let file: File = rule.style.files[key];
            formData.append(key, file, file.name);

        }
        formData.append("rule", JSON.stringify(rule));
        formData.append("entity_id", entityId ? entityId : "");
        formData.append("base_url", this.oneAppConfigurationService.getCSSImageDownloadUrl());
        return this.oneAppHttpService.postFormData("/api/styles/updateRuleStyle", formData);

    }
    getImage(id: string) {
        if (id == null || id == "") {
            return null;
        }
        return this.oneAppConfigurationService.getCSSImageDownloadUrl() + "/" + id;
    }
}
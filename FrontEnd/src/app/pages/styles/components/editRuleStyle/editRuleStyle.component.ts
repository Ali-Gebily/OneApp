import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ColorPickerService } from 'angular2-color-picker';
import * as _ from 'lodash';

import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppUIService }
  from '../../../../common/oneAppProxy/services';

import { RuleModel, CSSValueType, StyleModel } from '../../models'
import { StylesService } from '../../services/styles.service'



@Component({
  selector: 'editRuleStyle',
  templateUrl: 'editRuleStyle.html',
  styleUrls: ['editRuleStyle.scss']
})
export class EditRuleStyleComponent implements OnInit {
  @Input() rule: RuleModel;
  static DefaultColor: string = "rgba(0,0,0,0)";
  CSSValueType: any = CSSValueType;
  EditRuleStyleComponent: any = EditRuleStyleComponent;

  constructor(
    private stylesService: StylesService,
    private route: ActivatedRoute,
    private cpService: ColorPickerService,
    private oneAppUIService: OneAppUIService) {
  }
  //uploader
  public uploaderOptions: any = {
    filterExtensions: true,
    allowedExtensions: ['image/png', 'image/jpg']

  };
  defaultPicture: string;
  entityId: string = null;
  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
       let id: number = null;
      if (params['id'] !== undefined) {
         id = +params['id'];

      } else {
        throw new Error("You have to specify id of the rule")
      }
      if (params['entityId'] != undefined && params['entityId'] !== "" ) {
        this.entityId = params['entityId'];

      }
      this.stylesService.getRuleDetails(id, "")
        .then(rule => {
          this.extendAttribute(rule);
          this.rule = rule;
        });
    });
  }
  private clone(obj): any {
    var cloneObj: any = {};
    for (var key in obj) {
      if (typeof obj[key] === "object") {
        cloneObj[key] = this.clone(cloneObj[key]);
      } else {
        cloneObj[key] = obj[key];
      }
    }
    return cloneObj;
  }
  /*
 null -> property is not included in style, so it won't be handled
 "" -> property is included in style but without default value
 */
  private extendAttribute(rule) {
    rule.initial_style =<StyleModel> _.cloneDeep(rule.style);

    for (var key in rule.style) {
      if (this.getCSSValueType(key) == CSSValueType.Color
        && (rule.style[key] == "")) {
        rule.style[key] = EditRuleStyleComponent.DefaultColor;
      }

    }

  }
  private removeAttributeExtensions() {
    for (var key in this.rule.style) {
      if (this.getCSSValueType(key) == CSSValueType.Color
        && this.rule.style[key] == EditRuleStyleComponent.DefaultColor) {
        this.rule.style[key] = "";
      }
    }
  }
  private getCSSValueType(cssPropertyKey: string) {
    if (cssPropertyKey == 'color' || cssPropertyKey == 'background_color') {
      return CSSValueType.Color;
    }
    if (cssPropertyKey = "background_image") {
      return CSSValueType.File;;
    }
    return CSSValueType.NotSet;
  }
  save(): void {
    this.removeAttributeExtensions();

    this.stylesService.updateRuleStyle(this.rule, this.entityId)
      .then(formattedRule => {
        this.oneAppUIService.updateStyleRule(this.rule.scope, this.rule.selector, formattedRule);
        console.log(formattedRule);
        this.goBack();
      }).catch(error => { this.extendAttribute(this.rule); });

  }

  goBack(savedRule: RuleModel = null): void {
    window.history.back();
  }
  filesSelected(key: string, files: File[]) {
    if (this.rule.style.files == undefined) {
      this.rule.style.files = {};
    }
    if (files != null && files.length > 0) {
      this.rule.style.files[key] = files[0]
    } else {
      if (this.rule.style.files) {
        delete this.rule.style.files[key];
      }
      this.rule.style[key] = 0;
    }
  }
  clearColorValue(key: string) {
    this.rule.style[key] = EditRuleStyleComponent.DefaultColor;
  }
  resetColorValue(key: string) {
    this.rule.style[key] = this.rule.initial_style[key];
  }
  getImage(value: string) {
    return this.stylesService.getImage(value);
  }
}


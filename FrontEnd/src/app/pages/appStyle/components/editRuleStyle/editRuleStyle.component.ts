import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ColorPickerService } from 'angular2-color-picker';

import { RuleModel, CSSValueType } from '../../models'
import { AppStyleService } from '../../services/appStyle.service'



@Component({
  selector: 'editRuleStyle',
  templateUrl: 'editRuleStyle.html',
  styleUrls: ['editRuleStyle.scss']
})
export class EditRuleStyleComponent implements OnInit {
  @Input() rule: RuleModel;
  @Output() close = new EventEmitter();
  static DefaultColor: string = "rgba(0,0,0,0)";
  navigated = false; // true if navigated here
  CSSValueType: any = CSSValueType;
  EditRuleStyleComponent: any = EditRuleStyleComponent;
  Date: any = Date;
  constructor(
    private appStyleService: AppStyleService,
    private route: ActivatedRoute,
    private cpService: ColorPickerService) {
  }
  //uploader
  public uploaderOptions: any = {
    filterExtensions: true,
    allowedExtensions: ['image/png', 'image/jpg']

  };
  defaultPicture: string;
  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      if (params['id'] !== undefined) {
        let id = +params['id'];
        this.navigated = true;
        this.appStyleService.getRule(id)
          .then(rule => {
            this.extendAttribute(rule);
            this.rule = rule;
          });
      } else {
        throw new Error("You have to specify id of the rule")
      }
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
    rule.initial_style = this.clone(rule.style);

    for (var key in rule.style) {
      if (this.getCSSValueType(key) == CSSValueType.Color
        && (rule.style[key] == "")) {
        rule.style[key] = EditRuleStyleComponent.DefaultColor;
      }

    }

  }
  private removeAttributeExtension() {
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
    this.removeAttributeExtension();
    this.appStyleService.updateRuleStyle(this.rule)
      .then(formattedRule => {
        var oneAppStyleSheet = document.styleSheets["oneAppStyle"];
        if (oneAppStyleSheet == null) {
          var style = document.createElement('style');
          style.type = 'text/css';
          style.id = "oneAppStyleRule_" + this.rule.id;
          style.innerHTML = formattedRule;
          document.head.appendChild(style);
        } else {
          this.extendAttribute(this.rule);
          var oneAppStyleSheet = document.styleSheets["oneAppStyle"];
          var selector = this.rule.selector;
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
        this.goBack();
      }).catch(error => { this.extendAttribute(this.rule); });

  }

  goBack(savedRule: RuleModel = null): void {
    this.close.emit(savedRule);
    if (this.navigated) { window.history.back(); }
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
      this.rule.style[key] = "";
    }
  }
  clearColorValue(key: string) {
    this.rule.style[key] = EditRuleStyleComponent.DefaultColor;
  }
  resetColorValue(key: string) {
    this.rule.style[key] = this.rule.initial_style[key];
  }
  getImage(value: string) {
    return this.appStyleService.getImage(value);
  }
}


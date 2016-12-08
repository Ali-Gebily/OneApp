import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ColorPickerService } from 'angular2-color-picker';

import { RuleModel, AttributeModel, CSSValueType } from '../../models'
import { AppStyleService } from '../../services/appStyle.service'


const defaultColor: string = "rgba(0,0,0,0)";

@Component({
  moduleId: module.id,
  selector: 'editRuleStyle',
  templateUrl: 'editRuleStyle.html',
  styleUrls: ['editRuleStyle.scss']
})
export class EditRuleStyleComponent implements OnInit {
  @Input() rule: RuleModel;
  @Output() close = new EventEmitter();
  navigated = false; // true if navigated here
  CSSValueType: any = CSSValueType;
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
            this.rule = rule;
            this.extendAttribute();
          });
      } else {
        throw new Error("You have to specify id of the rule")
      }
    });
  }
  private extendAttribute() {
    for (var key in this.rule.style) {
      let attr: AttributeModel = this.rule.style[key];
      if (attr != null) {
        if (attr.css_value_type == CSSValueType.Color && (attr.value == null || attr.value == "")) {
          attr.value = defaultColor;
        }
        attr.old_value = attr.value //keep initial value to allow undo action
      }
    }

  }
  private removeAttributeExtension() {
    for (var key in this.rule.style) {
      let attr: AttributeModel = this.rule.style[key];
      if (attr != null) {
        if (attr.css_value_type == CSSValueType.Color && attr.value == defaultColor) {
          attr.value = null;
        }
        attr.old_value = attr.value //keep initial value to allow undo action
      }
    }

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
          this.extendAttribute();
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
      }).catch(error => { this.extendAttribute(); });

  }

  goBack(savedRule: RuleModel = null): void {
    this.close.emit(savedRule);
    if (this.navigated) { window.history.back(); }
  }
  filesSelected(files: File[], attribute: AttributeModel) {
    if (files != null && files.length > 0) {
      attribute.file = files[0];
    } else {
      attribute.file = null;
      attribute.value = null;
    }


  }
  clearValue(attribute: AttributeModel) { 
    attribute.value = defaultColor;
  }
  resetValue(attribute: AttributeModel) {
    attribute.value = attribute.old_value;
  }
  getImage(attribute: AttributeModel) {
    return this.appStyleService.getImage(attribute.value);
  }
}


import { StyleModel, RuleEntityScope } from '../models'

export class RuleModel {
    id: number;
    selector: string;
    name: string;
    hint: string;
    category: string;
    scope: RuleEntityScope;
    
    style: StyleModel
    initial_style: StyleModel;//used at reseting to initial value during editing
}
import {StyleModel} from './style.model'

export class RuleModel {
    id: number;
    selector: string;
    name: string;
    hint: string;
    tag: string;

    style: StyleModel

}
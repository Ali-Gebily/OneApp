import { Component, ViewEncapsulation, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'ba-menu-item',
  encapsulation: ViewEncapsulation.None,
  styles: [require('./baMenuItem.scss')],
  template: require('./baMenuItem.html')
})
export class BaMenuItem {
  /**
   *
   */
  constructor(private router: Router) {


  }
  @Input() menuItem: any;
  @Input() child: boolean = false;

  @Output() itemHover = new EventEmitter<any>();
  @Output() toggleSubMenu = new EventEmitter<any>();

  public onHoverItem($event, item): void {
    this.itemHover.emit($event);
  }

  public onToggleSubMenu($event, item): boolean {
    $event.item = item;
    this.toggleSubMenu.emit($event);
    return false;
  }
 
}

import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AppStyleService } from '../../services/appStyle.service'
import { LocalDataSource } from 'ng2-smart-table';


@Component({
  selector: 'listRules',
  encapsulation: ViewEncapsulation.None,
  styles: [require('./listRules.scss')],
  template: require('./listRules.html'),
})
export class ListRulesComponent {
  /**
   *
   */
  query: string = '';

  settings = {
    mode: 'external',
    edit: {
      editButtonContent: '<i class="ion-edit"></i>'
    },
    delete: {
      deleteButtonContent: '<i class="ion-trash-a"></i>',
      confirmDelete: true
    },
    columns: {
      name: {
        title: 'Name',
        type: 'string'
      },
      category: {
        title: 'Category',
        type: 'string'
      },
      description: {
        title: 'Description',
        type: 'string'
      }
    },
    actions: {
      add: false,
      edit: true,
      delete: false

    }
  };

  source: LocalDataSource = new LocalDataSource();

  constructor(protected service: AppStyleService,
    private router: Router) {
    this.service.getRulesSummary().then((data) => {
      this.source.load(data);
    });
  }

  onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }
  onEdit($event): void {
    console.log($event);
    this.router.navigate(['/pages/appStyle/editRuleStyle', $event.data.id]);
    event.preventDefault();
  }

}

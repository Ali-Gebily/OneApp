import { Component, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { StylesService } from '../../services/styles.service'
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

  constructor(protected stylesService: StylesService,
    private router: Router,
    private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      if (params['scope'] !== undefined) {
        let id = +params['scope'];
        this.stylesService.getRulesSummary(id)
          .then(data => {
            this.source.load(data);
          });
      } else {
        throw new Error("You have to specify scope paramter")
      }
    });
  }

  onEdit($event): void {
    //entityId should be handled later
    this.router.navigate(['/pages/styles/editRuleStyle', $event.data.id, ""]);
    event.preventDefault();
  }

}

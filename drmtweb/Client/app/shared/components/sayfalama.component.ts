import {
    Component,
    Input,
    Output,
    EventEmitter
} from '@angular/core';
import { OnChanges } from '@angular/core';

@Component({
    selector: 'pagination',
    templateUrl: './sayfalama.component.html',
    styleUrls: ['./sayfalama.component.scss']
})
export class PaginationComponent implements OnChanges {
    @Input('pager') pager: any = {};
    @Output('page-changed') pageChanged = new EventEmitter();
    pages: any[];
    ngOnChanges() {
    }
    setPage(page:number) {
        this.pager.currentPage = page;
        this.pageChanged.emit(page);
    }
}
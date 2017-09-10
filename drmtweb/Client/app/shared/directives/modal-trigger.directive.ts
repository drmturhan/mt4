import { Directive, OnInit, Inject, ElementRef, Input } from '@angular/core';
import { JQ_TOKEN } from "../services/jQueryService";
@Directive({
    selector: '[sor]'
})
export class ModalTriggerDirective implements OnInit {
    @Input('sor') modalId: string;

    private el: HTMLElement;
    constructor(el: ElementRef, @Inject(JQ_TOKEN) private $: any) {
        this.el = el.nativeElement;
    }
    ngOnInit() {
        this.el.addEventListener('click', e => {
            if (this.modalId !== undefined) {
                this.$(`#${this.modalId}`).modal({});
            }
        }
        );
    }

}

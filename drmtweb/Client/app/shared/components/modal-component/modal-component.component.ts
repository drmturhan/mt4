import { Component,  Input, Output, ViewChild, ElementRef, Inject, EventEmitter } from '@angular/core';
import { JQ_TOKEN } from "../../services/jQueryService";

@Component({
    selector: 'basit-modal',
    templateUrl: './modal-component.component.html',
    styleUrls: ['./modal-component.component.scss']
})
export class ModalComponent   {
    @ViewChild('modalContainer') containerElement: ElementRef
    @Input() en: number = 400;
    @Input() boy: number = 400;
    @Input() title: string;
    @Input() elementId: string;
    @Input() kapatTitle: string;
    @Input() evetTitle: string = "Evet";
    @Output() kapatTiklandi = new EventEmitter();
    @Output() tamamTiklandi = new EventEmitter();
    constructor( @Inject(JQ_TOKEN) private $: any) { }
    kapat() {
        this.$(this.containerElement.nativeElement).modal('hide');
        this.kapatTiklandi.emit('kapat');
    }
    tamam() {
        this.$(this.containerElement.nativeElement).modal('hide');
        this.tamamTiklandi.emit('tamam');
    }
}

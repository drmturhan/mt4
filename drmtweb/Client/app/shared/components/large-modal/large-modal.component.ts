import { Component, OnInit, Input, Output, ViewChild, ElementRef, Inject, EventEmitter } from '@angular/core';
import { JQ_TOKEN } from "../../services/jQueryService";



@Component({
  selector: 'large-modal',
  templateUrl: './large-modal.component.html',
  styleUrls: ['./large-modal.component.scss']
})
export class LargeModalComponent implements OnInit {

    @ViewChild('modalContainer') containerElement: ElementRef
    @Input() en: number = 400;
    @Input() boy: number = 400;
    @Input() title: string;
    @Input() elementId: string;
    @Input() kapatTitle: string = "";
    @Input() evetTitle: string = "";
    @Output() kapatTiklandi = new EventEmitter();
    @Output() tamamTiklandi = new EventEmitter();
    constructor( @Inject(JQ_TOKEN) private $: any) { }

   

    ngOnInit() {

    }

    kapat() {
        this.$('containerElement').modal('hide');
        this.kapatTiklandi.emit('kapat');
    }
    tamam() {
        this.$('containerElement').modal('hide');
        this.tamamTiklandi.emit('tamam');
    }

}

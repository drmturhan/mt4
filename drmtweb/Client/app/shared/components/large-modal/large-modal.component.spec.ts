import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LargeModalComponent } from './large-modal.component';

describe('LargeModalComponent', () => {
  let component: LargeModalComponent;
  let fixture: ComponentFixture<LargeModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LargeModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LargeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

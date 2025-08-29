import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-cbutton',
  standalone: true,
  templateUrl: './cbutton.component.html',
  styleUrls: ['./cbutton.component.scss'],
})
export class CButtonComponent {
  @Input() type: 'button' | 'submit' | 'reset' = 'button';
  @Input() disabled: boolean = false;
  @Input() fullWidth: boolean = false;
  @Output() buttonClick = new EventEmitter<Event>();
  @Input() theme: 'primary' | 'secondary' = 'primary';

  onClick(event: Event) {
    this.buttonClick.emit(event);
  }

}
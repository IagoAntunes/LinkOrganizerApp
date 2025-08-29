import { Component, Input, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Component({
    selector: 'app-cinput',
    templateUrl: './cinput.component.html',
    styleUrls: ['./cinput.component.scss'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => CinputComponent),
            multi: true,
        },
    ],
    standalone: true,
})
export class CinputComponent implements ControlValueAccessor {
    @Input() placeholder: string = '';
    @Input() type: string = 'text';
    @Input() disabled: boolean = false;

    value: string = '';
    onChange = (_: any) => {};
    onTouch = () => {};

    writeValue(value: any): void {
        this.value = value ?? '';
    }

    registerOnChange(fn: any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouch = fn;
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    onInput(event: Event): void {
        const value = (event.target as HTMLInputElement).value;
        this.value = value;
        this.onChange(value);
        this.onTouch();
    }
}
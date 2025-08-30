import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class SnackbarService {
    private readonly defaultConfig = {
        duration: 3000,
        verticalPosition: 'top' as const,
        horizontalPosition: 'right' as const,
    };

    constructor(private snackBar: MatSnackBar) {}

    success(message: string) {
        this.snackBar.open(message, 'Fechar', {
            ...this.defaultConfig,
            panelClass: ['snackbar-success']
        });
    }

    error(message: string) {
        this.snackBar.open(message, 'Fechar', {
            ...this.defaultConfig,
            panelClass: ['snackbar-error']
        });
    }

    info(message: string) {
        this.snackBar.open(message, 'Fechar', {
            ...this.defaultConfig,
            panelClass: ['snackbar-info']
        });
    }

    warning(message: string) {
        this.snackBar.open(message, 'Fechar', {
            ...this.defaultConfig,
            panelClass: ['snackbar-warning']
        });
    }
}
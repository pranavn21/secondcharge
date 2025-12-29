import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { SignInDialogComponent } from '../../sign-in-dialog/sign-in-dialog.component';


@Component({
  selector: 'app-toolbar',
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatDialogModule,
    RouterLink
  ],
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.css'
})
export class ToolbarComponent {
  constructor(private dialog: MatDialog) {}

  openSignInDialog(): void {
    this.dialog.open(SignInDialogComponent, {
      width: '450px',
      disableClose: false
    });
  }
}
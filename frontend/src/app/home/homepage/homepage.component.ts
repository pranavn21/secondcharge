import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { VehicleListComponent } from '../../vehicle-list/vehicle-list.component';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { UserComponent } from '../../user/user.component';
import { MatInputModule, MatFormField } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatLabel } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule, MatOption } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { ToolbarComponent } from '../../shared/toolbar/toolbar.component';

@Component({
  selector: 'app-homepage',
  imports: [
    MatCardModule,
    MatButtonModule,
    VehicleListComponent,
    RouterLink,
    RouterLinkActive,
    UserComponent,
    RouterOutlet,
    MatInputModule,
    FormsModule,
    MatFormField,
    MatLabel,
    MatIconModule,
    MatTabsModule,
    MatSelectModule,
    MatOption,
    MatChipsModule,
    ToolbarComponent
  ],
  
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {}
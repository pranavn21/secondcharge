import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { VehicleListComponent } from '../../vehicle-list/vehicle-list.component';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { UserComponent } from '../../user/user.component';
import { MatInputModule, MatFormField } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatLabel } from '@angular/material/form-field'; // Import MatLabel if you want to use labels with inputs
import { MatIconModule } from '@angular/material/icon';
import { MatTabBody, MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule, MatOption } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips'; // Import MatChipModule if you want to use chips

@Component({
  selector: 'app-homepage',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    VehicleListComponent,
    RouterLink,
    RouterLinkActive,
    UserComponent,
    RouterOutlet,
    MatInputModule,
    FormsModule, // Import FormsModule to use ngModel for two-way data binding
    MatFormField,
    MatLabel,
    MatIconModule,
    MatTabsModule,
    MatSelectModule,
    MatOption,
    MatChipsModule
  ],
  
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {

}

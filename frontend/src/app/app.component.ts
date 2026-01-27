import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CarListComponent } from './car-list/car-list.component';
import { UserComponent } from './user/user.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import {MatInput} from '@angular/material/input';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet, 
    RouterLink, 
    RouterLinkActive, 
    CarListComponent, 
    UserComponent,
    MatToolbarModule,
    MatButtonModule,
    MatInput
  ],

  templateUrl: './app.component.html', 
  standalone: true,
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'secondcharge';
}

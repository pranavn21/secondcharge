import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { UserComponent } from './user/user.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive, VehicleListComponent, UserComponent
  ],
  template: '<router-outlet></router-outlet>',
  standalone: true,
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'secondcharge';
}

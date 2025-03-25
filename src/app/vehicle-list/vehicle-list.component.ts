import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-vehicle-list',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css'],
  standalone: true
})
export class VehicleListComponent {

}

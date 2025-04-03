import { Component, Input} from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-vehicle-list',
  imports: [
    MatIconModule,
    MatCardModule,
    MatButtonModule,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css'],
  standalone: true
})

export class VehicleListComponent {
  // Example inputs; adjust or remove as needed based on your actual data model
  @Input() price: string = '$75,000';
  @Input() title: string = '2025 Tesla Cybertruck';
  @Input() mileage: string = '16,000 mi';
  @Input() location: string = 'Highland Park, TX';
  @Input() ownerName: string = 'Abby Arce';
  @Input() imageUrl: string = 'https://cleantechnica.com/wp-content/uploads/2023/01/2023.12-tesla-cybertruck-fremont-california-foundation-edition-KYLE-01-scaled.jpg'; // placeholder image

}

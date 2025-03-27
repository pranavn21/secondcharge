import { Component, Input, OnInit} from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { NgFor } from '@angular/common'; 

interface Vehicle {
  id: number;
  price: string;
  title: string;
  mileage: string;
  location: string;
  ownerName: string;
  imageUrl: string;
}

@Component({
  selector: 'app-vehicle-list',
  imports: [
    MatIconModule,
    MatCardModule,
    MatButtonModule,
    RouterLink,
    RouterLinkActive,
    NgFor
  ],
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css'],
  standalone: true
})

export class VehicleListComponent implements OnInit {
  vehicles: Vehicle[] = [];

  ngOnInit(): void {
        // For demo purposes, using static data; ideally, this comes from a service.
        this.vehicles = [
          {
            id: 1,
            price: '$75,000',
            title: '2025 Tesla Cybertruck',
            mileage: '16,000 mi',
            location: 'Highland Park, TX',
            ownerName: 'Abby Arce',
            imageUrl: 'https://cleantechnica.com/wp-content/uploads/2023/01/2023.12-tesla-cybertruck-fremont-california-foundation-edition-KYLE-01-scaled.jpg'
          },
          {
            id: 2,
            price: '$250,000',
            title: '2025 Lucid Air Sapphire',
            mileage: '2,000 mi',
            location: 'Colts Neck, NJ',
            ownerName: 'Pranav Nair',
            imageUrl: 'https://hips.hearstapps.com/hmg-prod/images/2024-lucid-air-sapphire-124-64cd3bf6945b0.jpg'
          },
          {
            id: 3,
            price: '$50,000',
            title: '2025 Kia EV9',
            mileage: '24,520 mi',
            location: 'Plano, TX',
            ownerName: 'Joseph Tinnelly',
            imageUrl: 'https://www.courant.com/wp-content/uploads/2024/07/IMG_6594.jpg'
          },
          // Add more vehicles as needed...
        ];
  }

  // // Example inputs; adjust or remove as needed based on your actual data model
  // @Input() price: string = '$75,000';
  // @Input() title: string = '2025 Tesla Cybertruck';
  // @Input() mileage: string = '16,000 mi';
  // @Input() location: string = 'Highland Park, TX';
  // @Input() ownerName: string = 'Abby Arce';
  // @Input() imageUrl: string = 'https://cleantechnica.com/wp-content/uploads/2023/01/2023.12-tesla-cybertruck-fremont-california-foundation-edition-KYLE-01-scaled.jpg'; // placeholder image

}

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ToolbarComponent } from "../shared/toolbar/toolbar.component";
import { VehicleListingService } from '../services/vehicle-listing.service';
import { VehicleListing } from '../models/vehicle-listing.model';

@Component({
  selector: 'app-car-list',
  imports: [
    CommonModule,
    MatIconModule,
    MatCardModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    RouterLink,
    RouterLinkActive,
    ToolbarComponent,
  ],
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css'],
  standalone: true
})
export class CarListComponent implements OnInit {
  listings: VehicleListing[] = [];
  loading = false;
  error: string | null = null;

  constructor(private vehicleListingService: VehicleListingService) {}

  ngOnInit(): void {
    this.loadListings();
  }

  loadListings(): void {
    this.loading = true;
    this.error = null;

    this.vehicleListingService.getAllListings().subscribe({
      next: (data) => {
        this.listings = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading vehicle listings:', err);
        this.error = 'Failed to load vehicles. Make sure the API is running.';
        this.loading = false;
      }
    });
  }

  // Format price to currency
  formatPrice(price: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0
    }).format(price);
  }

  // Format mileage with commas
  formatMileage(mileage: number): string {
    return mileage.toLocaleString('en-US') + ' mi';
  }

  // Get vehicle title from car model
  getVehicleTitle(listing: VehicleListing): string {
    if (listing.carModel) {
      return `${listing.carModel.make} ${listing.carModel.model}`;
    }
    return 'Vehicle';
  }

  // Get image URL with fallback
  getImageUrl(listing: VehicleListing): string {
    return listing.carModel?.modelImageUrl || 'https://via.placeholder.com/400x300?text=No+Image';
  }
}



import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
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
    ToolbarComponent,
  ],
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css'],
  standalone: true
})
export class CarListComponent implements OnInit {
  listings: VehicleListing[] = [];
  filteredListings: VehicleListing[] = [];
  loading = false;
  error: string | null = null;
  
  // Filter parameters
  searchQuery: string = '';
  makeFilter: string = '';
  modelFilter: string = '';
  priceRangeFilter: string = '';

  constructor(
    private vehicleListingService: VehicleListingService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Subscribe to query parameters
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['search'] || '';
      this.makeFilter = params['make'] || '';
      this.modelFilter = params['model'] || '';
      this.priceRangeFilter = params['priceRange'] || '';
      
      this.loadListings();
    });
  }

  loadListings(): void {
    this.loading = true;
    this.error = null;

    this.vehicleListingService.getAllListings().subscribe({
      next: (data) => {
        this.listings = data;
        this.applyFilters();
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading vehicle listings:', err);
        this.error = 'Failed to load vehicles. Make sure the API is running.';
        this.loading = false;
      }
    });
  }

  applyFilters(): void {
    let filtered = [...this.listings];

    // Apply search query filter (searches in make, model, and color)
    if (this.searchQuery) {
      const query = this.searchQuery.toLowerCase();
      filtered = filtered.filter(listing => {
        const make = listing.carModel?.make?.toLowerCase() || '';
        const model = listing.carModel?.model?.toLowerCase() || '';
        const color = listing.color?.toLowerCase() || '';
        return make.includes(query) || model.includes(query) || color.includes(query);
      });
    }

    // Apply make filter
    if (this.makeFilter) {
      filtered = filtered.filter(listing => 
        listing.carModel?.make === this.makeFilter
      );
    }

    // Apply model filter
    if (this.modelFilter) {
      filtered = filtered.filter(listing => 
        listing.carModel?.model === this.modelFilter
      );
    }

    // Apply price range filter
    if (this.priceRangeFilter) {
      const [min, max] = this.priceRangeFilter.split('-').map(Number);
      filtered = filtered.filter(listing => 
        listing.price >= min && listing.price <= max
      );
    }

    this.filteredListings = filtered;
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

  // Format location information
  formatLocation(listing: VehicleListing): string {
    if (listing.listingLocation) {
      const { zipCode, state, country } = listing.listingLocation;
      return `${zipCode}, ${state}, ${country}`;
    }
    return 'Location unavailable';
  }
}


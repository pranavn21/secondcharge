import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
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
    RouterLink,
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
export class HomepageComponent {
  // Filter properties
  searchQuery: string = '';
  selectedMake: string = '';
  selectedModel: string = '';
  selectedPriceRange: string = '';
  selectedCondition: string = '';
  zipCode: string = '';
  distance: string = '';

  constructor(private router: Router) {}

  onShowMatches(): void {
    const queryParams: any = {};

    // Add search query if provided
    if (this.searchQuery.trim()) {
      queryParams.search = this.searchQuery.trim();
    }

    // Add make filter
    if (this.selectedMake) {
      queryParams.make = this.selectedMake;
    }

    // Add model filter
    if (this.selectedModel) {
      queryParams.model = this.selectedModel;
    }

    // Add price range filter
    if (this.selectedPriceRange) {
      queryParams.priceRange = this.selectedPriceRange;
    }

    // Add condition filter
    if (this.selectedCondition) {
      queryParams.condition = this.selectedCondition;
    }

    // Navigate to vehicles page with query parameters
    this.router.navigate(['/vehicles'], { queryParams });
  }
}
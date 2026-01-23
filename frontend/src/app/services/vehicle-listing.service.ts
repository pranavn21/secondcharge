import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, delay } from 'rxjs';
import { VehicleListing, AddVehicleListingRequest, UpdateVehicleListingRequest } from '../models/vehicle-listing.model';
import { environment } from '../environments/environment.development';
import { MOCK_VEHICLE_LISTINGS } from '../mocks/mock-data';

@Injectable({
  providedIn: 'root'
})
export class VehicleListingService {
  private apiUrl = `${environment.apiBaseUrl}/api/vehiclelistings`;
  private useMockData = true; // Toggle this to switch between mock and real API

  constructor(private http: HttpClient) { }

  // GET all vehicle listings with optional filters
  getAllListings(
    filterOn?: string,
    filterQuery?: string,
    sortBy?: string,
    isAscending: boolean = true,
    pageNumber: number = 1,
    pageSize: number = 1000
  ): Observable<VehicleListing[]> {
    // Return mock data if enabled
    if (this.useMockData) {
      return of(MOCK_VEHICLE_LISTINGS).pipe(delay(500)); // Simulate network delay
    }

    // Real API call
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (filterOn && filterQuery) {
      params = params.set('filterOn', filterOn).set('filterQuery', filterQuery);
    }
    if (sortBy) {
      params = params.set('sortBy', sortBy).set('isAscending', isAscending.toString());
    }

    return this.http.get<VehicleListing[]>(this.apiUrl, { params });
  }

  // GET listing by ID
  getListingById(id: string): Observable<VehicleListing> {
    return this.http.get<VehicleListing>(`${this.apiUrl}/${id}`);
  }

  // POST create new listing
  createListing(listing: AddVehicleListingRequest): Observable<VehicleListing> {
    return this.http.post<VehicleListing>(this.apiUrl, listing);
  }

  // PUT update listing
  updateListing(id: string, listing: UpdateVehicleListingRequest): Observable<VehicleListing> {
    return this.http.put<VehicleListing>(`${this.apiUrl}/${id}`, listing);
  }

  // DELETE listing
  deleteListing(id: string): Observable<VehicleListing> {
    return this.http.delete<VehicleListing>(`${this.apiUrl}/${id}`);
  }
}

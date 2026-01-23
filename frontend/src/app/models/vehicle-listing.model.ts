import { Car } from './car.model';

export interface VehicleListing {
  id: string;
  carId: string;
  carModel?: Car;
  mileage: number;
  color: string;
  listingLocationId: string;
  price: number;
}

export interface AddVehicleListingRequest {
  carId: string;
  mileage: number;
  color: string;
  listingLocationId: string;
  price: number;
}

export interface UpdateVehicleListingRequest {
  carId: string;
  mileage: number;
  color: string;
  listingLocationId: string;
  price: number;
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Car, AddCarRequest, UpdateCarRequest } from '../models/car.model';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private apiUrl = `${environment.apiBaseUrl}/api/cars`;

  constructor(private http: HttpClient) { }

  // GET all cars with optional filters
  getAllCars(
    filterOn?: string,
    filterQuery?: string,
    sortBy?: string,
    isAscending: boolean = true,
    pageNumber: number = 1,
    pageSize: number = 1000
  ): Observable<Car[]> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (filterOn && filterQuery) {
      params = params.set('filterOn', filterOn).set('filterQuery', filterQuery);
    }
    if (sortBy) {
      params = params.set('sortBy', sortBy).set('isAscending', isAscending.toString());
    }

    return this.http.get<Car[]>(this.apiUrl, { params });
  }

  // GET car by ID
  getCarById(id: string): Observable<Car> {
    return this.http.get<Car>(`${this.apiUrl}/${id}`);
  }

  // POST create new car
  createCar(car: AddCarRequest): Observable<Car> {
    return this.http.post<Car>(this.apiUrl, car);
  }

  // PUT update car
  updateCar(id: string, car: UpdateCarRequest): Observable<Car> {
    return this.http.put<Car>(`${this.apiUrl}/${id}`, car);
  }

  // DELETE car
  deleteCar(id: string): Observable<Car> {
    return this.http.delete<Car>(`${this.apiUrl}/${id}`);
  }
}
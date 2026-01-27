export interface Location {
  id: string;
  country: string;
  state: string;
  zipCode: string;
}

export interface AddLocationRequest {
  country: string;
  state: string;
  zipCode: string;
}

export interface UpdateLocationRequest {
  country: string;
  state: string;
  zipCode: string;
}

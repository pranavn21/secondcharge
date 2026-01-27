export interface Car {
  id: string;
  make: string;
  model: string;
  efficiency: number;
  modelImageUrl?: string;
}

export interface AddCarRequest {
  make: string;
  model: string;
  efficiency: number;
  modelImageUrl?: string;
}

export interface UpdateCarRequest {
  make: string;
  model: string;
  efficiency: number;
  modelImageUrl?: string;
}
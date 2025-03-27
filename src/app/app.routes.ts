import { Routes } from '@angular/router';
import { VehicleListComponent } from './vehicle/vehicle-list/vehicle-list.component';
import { UserComponent } from './user/user.component';

export const routes: Routes = [
    {path: '', component: VehicleListComponent},
    {path: 'user', component: UserComponent},
];

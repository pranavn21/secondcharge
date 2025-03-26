import { RouterModule, Routes } from '@angular/router';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { UserComponent } from './user/user.component';

export const routes: Routes = [
    {path: '', redirectTo: '/', pathMatch: 'full'},
    {path: 'vehicles', component: VehicleListComponent},
    {path: 'user', component: UserComponent},
];

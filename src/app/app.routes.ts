import { RouterModule, Routes } from '@angular/router';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { UserComponent } from './user/user.component';
import { HomepageComponent } from './home/homepage/homepage.component';

export const routes: Routes = [
    //{path: '', redirectTo: '/', pathMatch: 'full'},
    {path: '', component: HomepageComponent},
    {path: 'vehicles', component: VehicleListComponent},
    {path: 'user', component: UserComponent},
];

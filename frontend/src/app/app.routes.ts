import { RouterModule, Routes } from '@angular/router';
import { CarListComponent } from './car-list/car-list.component';
import { UserComponent } from './user/user.component';
import { HomepageComponent } from './homepage/homepage.component';

export const routes: Routes = [
    //{path: '', redirectTo: '/', pathMatch: 'full'},
    {path: '', component: HomepageComponent},
    {path: 'vehicles', component: CarListComponent},
    {path: 'user', component: UserComponent},
];

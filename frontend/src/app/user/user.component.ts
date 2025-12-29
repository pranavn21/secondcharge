import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { ToolbarComponent } from "../shared/toolbar/toolbar.component";


@Component({
  selector: 'app-user',
  imports: [
    RouterLink, 
    RouterLinkActive, 
    ToolbarComponent
  ],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  standalone: true
})
export class UserComponent {

}

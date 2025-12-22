import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';


@Component({
  selector: 'app-user',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  standalone: true
})
export class UserComponent {

}

import {Component} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'InfoTrackSEO';

  constructor(private router: Router) {
  }

  goToSearchHistory(): void {
    this.router.navigate(['/search-history']);
  }

  goToSearch(): void {
    this.router.navigate(['/search']);
  }
}

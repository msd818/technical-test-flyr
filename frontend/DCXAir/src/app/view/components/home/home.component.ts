import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  title = 'DCXAir';

  
  constructor(private router: Router) { }
  
  ngOnInit() {}

  viewSearch() {
    this.router.navigate(['/search']);
  }
}

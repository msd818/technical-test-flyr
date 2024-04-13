import { Component, OnInit } from '@angular/core';
import { Airport } from '../../interface/airport';
import { Currency } from '../../interface/currency';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SearchService } from '../../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  title = 'DCXAir';
  searchForm!: FormGroup;
  searchResponse!: any[];

  airports: Airport[] = [
    { value: 'MZL' },
    { value: 'BOG' },
    { value: 'PEI' },
    { value: 'JFK' },
    { value: 'BCN' },
    { value: 'MAD' },
  ];

  currency: Currency[] = [
    { text: "USD", value: 'usd' },
    { text: "EUR", value: 'eur' },
    { text: "COP", value: 'cop' }
  ];


  constructor(private formBuilder: FormBuilder, private _searchService: SearchService) { }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      origin: [''],
      destination: [''],
      typeJourney: [''],
      currency: ['']
    });
  }

  search() {
    const origin = this.searchForm.get('origin')?.value;
    const destination = this.searchForm.get('destination')?.value;
    const currency = this.searchForm.get('currency')?.value;
    const typeJourney = this.searchForm.get('typeJourney')?.value;


    this._searchService.searchFlights(origin, destination, currency, typeJourney)
      .subscribe(
        response => {
          this.searchResponse = response.data;
          console.log(response.data);

        },
        error => {
          alert("Error al consultar vuelo");
          console.error(error);
        }
      );
  }

}

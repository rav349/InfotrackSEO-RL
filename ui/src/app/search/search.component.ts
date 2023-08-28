import {Component} from '@angular/core';
import {SearchService} from "../search.service";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  searchEngineOptions: string[] = ['Google', 'Bing'];
  searchEngine: string = this.searchEngineOptions[0];
  url: string = '';
  keywords: string = '';
  searchResult: any = null;

  constructor(private searchService: SearchService) {
  }

  search(): void {
    this.searchService.search(this.searchEngine, this.url, this.keywords)
      .subscribe(result => {
        this.searchResult = result;
      })
  }
}

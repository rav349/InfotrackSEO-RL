import {Component, OnInit} from '@angular/core';
import {SearchHistoryService} from '../search-history.service';

@Component({
  selector: 'app-search-history',
  templateUrl: './search-history.component.html',
  styleUrls: ['./search-history.component.css']
})
export class SearchHistoryComponent implements OnInit {
  searchHistoryData: any[] = [];

  constructor(private searchHistoryService: SearchHistoryService) {
  }

  ngOnInit(): void {
    this.fetchSearchHistory();
  }

  fetchSearchHistory(): void {
    this.searchHistoryService.getSearchHistory()
      .subscribe(data => {
        this.searchHistoryData = data;
      });
  }
}

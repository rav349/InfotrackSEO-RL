import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchHistoryService {
  private apiUrl = 'https://localhost:44317/api/SearchHistory';

  constructor(private http: HttpClient) {
  }

  getSearchHistory(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}

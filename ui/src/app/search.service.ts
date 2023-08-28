import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private apiUrl = 'https://localhost:44317/api';

  constructor(private http: HttpClient) {
  }

  search(searchEngine: string, url: string, keywords: string): Observable<any> {
    const urlWithParams = `${this.apiUrl}/Search?SearchEngine=${searchEngine}&Url=${url}&Keywords=${keywords}`
    return this.http.get(urlWithParams);
  }
}

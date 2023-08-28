import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {SearchHistoryComponent} from './search-history/search-history.component';
import {SearchComponent} from './search/search.component';

const routes: Routes = [
  {path: '', component: SearchComponent},
  {path: 'search-history', component: SearchHistoryComponent},
  {path: 'search', component: SearchComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

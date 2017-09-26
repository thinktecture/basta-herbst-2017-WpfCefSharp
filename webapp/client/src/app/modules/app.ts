import {NgModule} from '@angular/core';
import {PokemonService} from '../services/pokemon';
import {PokemonInProcService} from '../services/pokemonInProc';
import {ListComponent} from '../components/list/list';
import {DetailComponent} from '../components/detail/detail';
import {BrowserModule} from '@angular/platform-browser';
import {HashLocationStrategy, LocationStrategy} from '@angular/common';
import {HttpModule} from '@angular/http';
import {AppRoutes, appRoutingProviders} from '../components/app/routes';
import {AppComponent} from '../components/app/app';

declare interface IWindow extends Window { hostInterface : any }
declare var window: IWindow;

let pokemonService = (window.hostInterface) ? PokemonInProcService : PokemonService;

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        AppRoutes],
    declarations: [
        AppComponent,
        ListComponent,
        DetailComponent],
    bootstrap: [AppComponent],
    providers: [
        { provide: PokemonService, useClass: pokemonService },
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        appRoutingProviders
    ]
})
export class AppModule {
}

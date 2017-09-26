import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Pokemon} from '../models/pokemon';
import {PokemonService} from './pokemon';

declare interface IWindow extends Window {
   hostInterface : {
       getPokemonList(): Promise<string>,
       getPokemonById(id: number): Promise<string>,
   };
}

declare var window: IWindow;

@Injectable()
export class PokemonInProcService extends PokemonService {

    public getPokemonList(): Observable<Array<Pokemon>> {
         return Observable.fromPromise(window.hostInterface.getPokemonList())
            .flatMap(response => {
                console.log(response);
                return Observable.from(JSON.parse(response));
            })
            .map(json => Pokemon.deserialize(json))
            .toArray();
    }

    public getPokemon(id: number): Observable<Pokemon> {
        return Observable.fromPromise(window.hostInterface.getPokemonById(id))
            .map(res => Pokemon.deserialize(JSON.parse(res)));
    }
}

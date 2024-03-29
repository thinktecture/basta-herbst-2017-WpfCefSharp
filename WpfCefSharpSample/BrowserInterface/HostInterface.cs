﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace WpfCefSharpSample.BrowserInterface
{
    public class HostInterface
    {
        private readonly Window _window;

        public HostInterface(Window window)
        {
            _window = window;
        }

        public void ToggleFullScreen()
        {
            _window?.Dispatcher.Invoke(() =>
            {
                _window.WindowState = (_window.WindowState == WindowState.Normal)
                    ? WindowState.Maximized
                    : WindowState.Normal;
            });
        }

        public void CloseApplication()
        {
            _window?.Dispatcher.Invoke(() => _window.Close());
        }

        public string GetPokemonById(int id)
        {
            Trace.WriteLine($"Processing request for single Pokémon by id {id}...");

            IEnumerable<dynamic> pokemonList = (IEnumerable<dynamic>)JsonConvert.DeserializeObject(GetPokemonList());
            var pokemon = pokemonList.FirstOrDefault(p => p.id == id);

            return JsonConvert.SerializeObject(pokemon);
        }

        public string GetPokemonList()
        {
            Trace.WriteLine($"Processing request for complete Pokémon list...");

            string pokelist = @"[
    {
        id: 1,
        name: 'BASTA Bulbasaur',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/001.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/001.jpg',
        evolvesTo: [2],
        type: ['Grass', 'Poison'],
    },
    {
        id: 2,
        name: 'Ivysaur',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/002.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/002.jpg',
        evolvesTo: [3],
        type: [ 'Grass', 'Poison' ],
    },
    {
        id: 3,
        name: 'Venusaur',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/003.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/003.jpg',
        evolvesTo: [],
        type: [ 'Grass', 'Poison' ],
    },
    {
        id: 4,
        name: 'Charmander',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/004.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/004.jpg',
        evolvesTo: [5],
        type: [ 'Fire' ],
    },
    {
        id: 5,
        name: 'Charmeleon',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/005.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/005.jpg',
        evolvesTo: [6],
        type: [ 'Fire' ],
    },
    {
        id: 6,
        name: 'Charizard',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/006.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/006.jpg',
        evolvesTo: [],
        type: [ 'Fire' ],
    },
    {
        id: 7,
        name: 'Squirtle',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/007.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/007.jpg',
        evolvesTo: [8],
        type: [ 'Water' ],
    },
    {
        id: 8,
        name: 'Wartortle',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/008.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/008.jpg',
        evolvesTo: [9],
        type: [ 'Water' ],
    },
    {
        id: 9,
        name: 'Blastoise',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/008.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/008.jpg',
        evolvesTo: [],
        type: [ 'Water' ],
    },
    {
        id: 25,
        name: 'Pikachu',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/025.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/025.jpg',
        evolvesTo: [9],
        type: [ 'Electric' ],
    },
    {
        id: 26,
        name: 'Raichu',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/026.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/026.jpg',
        evolvesTo: [],
        type: [ 'Electric' ],
    },
    {
        id: 133,
        name: 'Eevee',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/133.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/133.jpg',
        evolvesTo: [134, 135, 136],
        type: [ 'Normal' ],
    },
    {
        id: 134,
        name: 'Vaporeon',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/134.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/134.jpg',
        evolvesTo: [],
        type: [ 'Water' ],
    },
    {
        id: 135,
        name: 'Jolteon',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/135.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/135.jpg',
        evolvesTo: [],
        type: [ 'Electric' ],
    },
    {
        id: 136,
        name: 'Flareon',
        thumbnail: 'http://pokedream.com/pokedex/images/mini/136.png',
        image: 'http://pokedream.com/pokedex/images/sugimori/136.jpg',
        evolvesTo: [],
        type: [ 'Fire' ],
    },
]";

            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(pokelist));
        }
    }
}

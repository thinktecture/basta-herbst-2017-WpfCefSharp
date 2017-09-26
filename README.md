# Wpf CefSharp Sample

Simple sample application that shows how you can embed a web application into a WPF application using CefSharp.

## Webapp

Prerequisites: Have node 6.6.0 installed (other versions may work, but 6.6.0 works for sure).

### Api

A node-based API for the web application. In the api directory, execute `npm i` to install all required node modules, and then `npm run start` to start the API.

## Client

An Angular 2.0 based web application that uses the API to display Pokémon. In the client directory, execute `npm i` to load all required angular modules and the tooling to build the web application, and then `npm run start` to build the web app and have it hosted locally on a node based webserver on port 8000.

With both api and client running, point a browser to localhost:8000 to see the app running, or use the WpfCefSharpSample application to load the Pokédex app and have the data loading from the .NET app through the Cef bridge.
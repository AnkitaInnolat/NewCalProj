/// <reference path="home/home.html" />
var app = angular.module('calApp', ['ngRoute']);


app.config(function ($routeProvider) {

    $routeProvider.otherwise({
        templateUrl: 'home / home.html',
        controller:'homeController'
    })


});
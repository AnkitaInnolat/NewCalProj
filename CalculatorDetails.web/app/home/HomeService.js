app.factory('homeService', function ($http) {


    homeService.getCalculationDetails = function () {
        return $http.get("http://localhost/Innotym.Api/odata/CalculatorDetails");
    }




});




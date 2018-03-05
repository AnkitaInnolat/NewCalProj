app.controller('homeController', function (homeService) {

    $scope.getCalDetails = function () {
        homeService.getCalculationDetails().then(function (results) {


        });
    }





});
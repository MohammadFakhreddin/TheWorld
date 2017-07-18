(function () {
    "use strict";
    //Getting existing module
    angular.module("app-trips")
        .controller("tripsController", tripsController);
    function tripsController($http) {
        var vm = this;
        vm.trips = [];
        //{
        //    name: "Us trip",
        //        created: new Date()
        //}, {
        //    name: "World trip",
        //        created: new Date()
        //}

        vm.newTrip = {};

        vm.errorMessage = "";

        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                angular.copy(response.data, vm.trips);
            }, function (error) {
                vm.errorMessage = "Failed to load data: " + error;
            }).finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function () {
            vm.isBusy = true;
            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, function (error) {
                    vm.errorMessage = "Failed to save new trip";
                }).finally(function () {
                    vm.isBusy = false;     
                });
        }
    }
})();
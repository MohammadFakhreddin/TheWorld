(function () {
    "use strict"
    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);
    function tripEditorController($http,$routeParams) {
        var vm = this;
        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function (res) {
                angular.copy(res.data, vm.stops);
                showMap(vm.stops);
            }, function (err) {
                vm.errorMessage = err.errorMessage.toString();
            }).finally(function () {
                vm.isBusy = false;
            });
        vm.addStop = function () {
            vm.isBusy = true;
            $http.post(url, vm.newStop).then(function (res) {
                vm.stops.push(res.data);
                showMap(vm.stops);
                vm.newStop = {};
            }, function (err) {
                vm.errorMessage = "Failed to add new stop"
            }).finally(function () {
                    vm.isBusy = false;
            });
        };
    }

    function showMap(stops) {
        if (stops && stops.length > 0) {
            //{
            //    lat: 33.748995,
            //        long: -84.387982,
            //            info: "Atlanta, Georgia - Departed Jun 3, 2014"
            //},
            var formatedStops = [];
            for (var i = 0; i < stops.length; i++) {
                formatedStops.push({
                    lat: stops[i].latitude,
                    long: stops[i].longtitude,
                    info: stops.name||"undefined",
                });   
            }
            travelMap.createMap({
                stops: formatedStops,
                selector: "#map",
                currentStop: 1,
                initialZoom:3
            });
        }
    }
})();
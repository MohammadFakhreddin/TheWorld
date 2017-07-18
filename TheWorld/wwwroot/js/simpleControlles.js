(function () {
    "use strict"
    angular.module("simpleControlles", [])
    .directive("waitCursor", waitCursor);
    function waitCursor() {
        return {
            scope: {
               show:"=displayWhen"//Replaces show instead of display when for a scoped
            },
            templateUrl:"/views/waitCursor.html"
        };
    }
})();
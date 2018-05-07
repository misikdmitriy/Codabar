(function () {
    "use strict";
})();

var productApp = angular.module('productApp', []);

productApp.controller("productController", function productController($scope, $http) {
    var ctrl = this;

    $http({
        method: "GET",
        url: "http://localhost:50482/api/products"
    }).then(function(resp) {
        ctrl.products = resp.data;
    });

    ctrl.getCodabar = function(id) {
        window.location.href = "http://localhost:50482/api/products/codabar/" + id
        // $http({
        //     method: "GET",
        //     url: "http://localhost:50482/api/products/codabar/" + id
        // }).then(function(resp) {

        // });
    }
});
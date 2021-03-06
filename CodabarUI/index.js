(function () {
    "use strict";
})();

var productApp = angular.module('productApp', ["ngRoute", "ngFileUpload"]);

productApp.config(function ($routeProvider) {
    $routeProvider.when("/", {
        templateUrl: "items.htm"
    }).when("/add", {
        templateUrl: "add.htm"
    }).when("/decode", {
        templateUrl: "decode.htm"
    });
});

productApp.controller("productController", function productController($scope, $http) {
    var ctrl = this;

    $http({
        method: "GET",
        url: "http://localhost:50482/api/products"
    }).then(function (resp) {
        ctrl.products = resp.data;
    });

    ctrl.getCodabar = function (id) {
        window.location.href = "http://localhost:50482/api/products/codabar/" + id
    }

    ctrl.remove = function (id) {
        $http.delete("http://localhost:50482/api/products/" + id)
            .then(function (resp) {
                var elems = ctrl.products.filter(function (e) {
                    return e.id == id
                });

                if (elems.length > 0) {
                    var index = ctrl.products.indexOf(elems[0]);
                    ctrl.products.splice(index, 1);
                }
            });
    }
});

productApp.controller("productAddController", function productAddController($scope, $http) {
    var ctrl = this;

    ctrl.name = "";
    ctrl.code = "";

    ctrl.submit = function () {
        $http.post("http://localhost:50482/api/products", {
            name: ctrl.name,
            code: ctrl.code
        }).then(function () {
            window.location = "#!";
        });
    }
});

productApp.controller("productDecodeController", function productDecodeController($scope, Upload) {
    var ctrl = this;

    ctrl.submit = function () {
        Upload.upload({
            url: 'http://localhost:50482/api/products/decode',
            data: {
                file: ctrl.file
            }
        }).then(function (resp) {
            ctrl.name = resp.data.name;
            ctrl.isUnrecognised = false;
        }, function(resp) {
            ctrl.name = null;
            ctrl.isUnrecognised = true;
        });
    };
});
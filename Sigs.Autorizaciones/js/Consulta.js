/// <reference path="../Scripts/angular.js" />

app.controller('ConsultaCtrl', function ($scope, $http) {

    $scope.Buscar = function (autorizacionId, carnet,
        fechaAutorizacionDesde, fechaAutorizacionHasta,
        fechaServicioDesde, fechaServicioHasta) {

        var data = {
            autorizacionId: autorizacionId,
            carnet: carnet,
            fechaAutorizacionDesde: fechaAutorizacionDesde,
            fechaAutorizacionHasta: fechaAutorizacionHasta,
            fechaServicioDesde: fechaServicioDesde,
            fechaServicioHasta: fechaServicioHasta
        };

        $http({
            url: 'Consultas/Autorizaciones/' + $scope.Autorizacion.Afiliado.Carnet,
            method: "GET",

        }).success(function (data) {

        }).error(function (data, status, headers, config) {

        });
    }

    $scope.Limpiar = function () {

        $scope.AutorizacionId = "";
        $scope.Carnet = "";
        $scope.FechaAutorizacionDesde = "";
        $scope.FechaAutorizacionHasta = "";

        $scope.FachaServicioDesde = "";
        $scope.FechaServicioHasta = "";

    }
});
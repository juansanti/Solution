/// <reference path="../Scripts/angular.js" />

app.controller('ConsultaCtrl', function ($scope, $http) {

    $scope.Buscar = function (autorizacionId, carnet,
        fechaAutorizacionDesde, fechaAutorizacionHasta,
        fechaServicioDesde, fechaServicioHasta) {

        var params = {
            autorizacionId: autorizacionId,
            carnet: carnet,
            fechaAutorizacionDesde: fechaAutorizacionDesde,
            fechaAutorizacionHasta: fechaAutorizacionHasta,
            fechaServicioDesde: fechaServicioDesde,
            fechaServicioHasta: fechaServicioHasta
        };

        $http({
            url: '/Consultas/Autorizaciones/',
            method: "POST",
            data: params
        }).success(function (data) {

            $scope.Autorizaciones = data;

        }).error(function (data, status, headers, config) {
            toastr.error(data);
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

    $scope.Anular = function (Id, autorizacionId, carnet,
        fechaAutorizacionDesde, fechaAutorizacionHasta,
        fechaServicioDesde, fechaServicioHasta) {

        var anular = confirm('Está segur@ que desea anular esta autorización?')

        if (!anular)
            return;

        $http({
            url: '/Autorizacion/Anular/',
            method: "POST",
            data: { Id: Id }
        }).success(function (data) {

            if (data > 0) {
                toastr.warning('Autorizacion ' + Id + ' anulada satisfactoriamente');

                $scope.Buscar(autorizacionId, carnet, fechaAutorizacionDesde, fechaAutorizacionHasta, fechaServicioDesde, fechaServicioHasta);
            }

        }).error(function (data, status, headers, config) {
            tostr.error(data);
        });

    }

    $scope.Imprimir = function (autorizacionId) {

        var builder = reportBuilder();

        builder.reporte = 1;
        builder.titulo = 'autorización número ' + autorizacionId;
        builder.parametros.push(new NewReportParameter('autorizacionId', autorizacionId));

        Imprimir(builder);
    }

    $scope.TotalAprobado = function (autorizaciones) {

        if (!autorizaciones)
            return;

        var aprobado = 0;
        $.map(autorizaciones, function (a) {

            if (a.Disponible)
                aprobado += a.MontoAprobado;
        })

        return aprobado;
    }
});
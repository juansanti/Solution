/// <reference path="Models.js" />
var myModule = angular.module('autorizaciones', []).config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Iniciar', { templateUrl: 'login.html', controller: 'AuthenticationCtrl' }).
        when('/Autorizar', { templateUrl: 'autorizar.html', controller: 'AutorizarCtrl' }).
        otherwise({ redirectTo: '/Iniciar' });
}]);

var x = [];

var autorizacionesScope;

myModule.controller('AutorizarCtrl', function ($scope, $routeParams, $http) {
    $thescope = $scope;
    $scope.BuscandoAfiliado = false;
    $scope.DisponibleInicial = 0;

    $scope.CreateAfiliado = function (a) {
        var af = {
            Id: 0,
            Carnet: 0,
            Nombre: "",
            Edad: 0,
            Sexo: 0,
            MDMedicamentos: 0,
            PuedeAutoriar: false,
            Impedimentos: "",
            Foto: "",
        }

        if (a != null) {
            af.Id = a.AfiliadoId;
            af.Carnet = a.Carnet;
            af.Nombre = a.NombreCompleto;
            af.Edad = a.Edad;
            af.Sexo = a.Sexo;
            af.MDMedicamentos = 0, //a.MDMedicamentos,
            af.PuedeAutoriar = false, //a.PuedeAutoriar,
            af.Impedimentos = "arreglar aqui", // a.Impedimentos,
            af.Foto = a.Foto
        }

        return af;
    }

    $scope.CreateAutorizacion = function (a) {
        var aut = {
            Id: 0,
            EpisodioId: 0,
            FechaAutorizacion: "",
            FechaServicio: "",
            UsuarioId: 0,
            Usuario: "",
            Afiliado: $scope.CreateAfiliado(),
            Prestaciones: [],
        };

        if (a != null) {
            aut.Id = a.Id;
            aut.EpisodioId = a.EpisodioId;
            aut.Afiliado = $scope.CreateAfiliado(a.Afiliado);
            aut.Prestaciones = $.map(a.Prestaciones, function (item) {
                return $scope.CreatePrestacionAutorizacion(item);
            });

            aut.FechaAutorizacion = a.FechaAutorizacion;
            aut.Usuario = a.Login;
        }
        return aut;
    }
    $scope.CreatePrestacion = function () {
        var obj = {
            Id: 0,
            CoberturaId: 0,
            DetallePlanId: 0,
            Cobertura: "",
            SubGrupoId: "",
            SubGrupo: "",
            Tarifa: 0,
            TarifaSemma: 0,
            PorcentajeCobertura: 0,
            Cantidad: 1
        }

        return obj;
    }
    $scope.CreatePrestacionAutorizacion = function (p) {
        var pr = {
            Id: 0,
            DetallePlanId: 0,
            AutorizacionId: 0,
            DetallePlanId: 0,
            CoberturaId: 0,
            Cantidad: 0,
            Tarifa: 0,
            TarifaSemma: 0,
            CoPago: 0,
            PorcentajeCobertura: 0,
            MontoAprobado: 0,
            Nombre: "",
            FechaCreacion: "",
            Usuario: ""
        }

        if (p != null) {
            pr.Id = p.Id;
            pr.DetallePlanId = p.DetallePlanId;
            pr.AutorizacionId = p.AutorizacionId;
            pr.CoberturaId = p.CoberturaId;
            pr.Cantidad = p.Cantidad;
            pr.Tarifa = p.Tarifa;
            pr.CoPago = p.CoPago;
            pr.PorcentajeCobertura = p.PorcentajeCobertura / 100;
            pr.MontoAprobado = p.MontoAprobado;
            pr.Nombre = p.Nombre;
            pr.FechaCreacion = p.FechaCreacion;
            pr.Usuario = p.Usuario;
        }
        return pr;
    }

    $scope.Prestaciones = [];
    $scope.Prestacion = $scope.CreatePrestacion();

    var GetPrestacionByIndex = function (index) {
        return $scope.Autorizacion.Prestaciones[index];
    }

    $scope.SubTotalPrestacion = function (index) {
        var p = GetPrestacionByIndex(index);
        var result = parseFloat(p.Cantidad) * parseFloat(p.Tarifa);
        return result.toFixed(2);
    }

    $scope.Copago = function (index) {
        var p = GetPrestacionByIndex(index);

        var autorizado = p.MontoAprobado;

        var subtotal = $scope.SubTotalPrestacion(index);

        var result = subtotal - autorizado;
        return result.toFixed(2);
    }

    $scope.Prestacion = new $scope.CreatePrestacion();

    $scope.Autorizacion = new $scope.CreateAutorizacion();

    $scope.TotalGeneral = function () {
        var sum = 0;
        $.map($scope.Autorizacion.Prestaciones, function (item, index) {

            sum += parseFloat($scope.SubTotalPrestacion(index));
        });

        return parseFloat(sum).toFixed(2);
    }

    $scope.AutorizadoTotal = function () {
        var sum = 0;
        $.map($scope.Autorizacion.Prestaciones, function (p, index) {
            sum += p.MontoAprobado;
        });

        return parseFloat(sum.toString()).toFixed(2);
    }

    $scope.CopagoTotal = function () {
        var sum = 0;

        $.map($scope.Autorizacion.Prestaciones, function (p, index) {
            sum += parseFloat($scope.Copago(index));
        });

        return parseFloat(sum).toFixed(2);
    }

    $scope.TotalCantidadPrestaciones = function () {
        var sum = 0;

        $.map($scope.Autorizacion.Prestaciones, function (p, index) {
            sum += parseFloat(p.Cantidad);
        });

        return parseFloat(sum).toFixed(2);
    }

    var getMontoAutorizado = function (cobertura) {
        var montoAutorizar = 0;

        var tarifa = parseFloat(cobertura.Tarifa);
        var cantidad = parseFloat(cobertura.Cantidad);
        var porcentaje = parseFloat(cobertura.PorcentajeCobertura);

        //montoAutorizar = (parseFloat(cobertura.Tarifa) * parseFloat(cobertura.Cantidad)) * .70;

        montoAutorizar = (tarifa * cantidad) * porcentaje;

        var sumAutorizado = parseFloat($scope.AutorizadoTotal());

        var autorizado = 0;

        if (montoAutorizar > $scope.Autorizacion.Afiliado.MDMedicamentos) {
            autorizado = parseFloat($scope.Autorizacion.Afiliado.MDMedicamentos)
        }
        else {
            autorizado = montoAutorizar;
        }

        return autorizado;
    }

    $scope.AgregarCobertura = function () {

        var c = $scope.CreatePrestacionAutorizacion();

        var montoAutorizar = 0;

        c.Cantidad = $scope.Prestacion.Cantidad;
        c.Tarifa = $scope.Prestacion.Tarifa

        c.DetallePlanId = $scope.Prestacion.DetallePlanId;
        c.Nombre = $scope.Prestacion.Nombre;
        c.CoberturaId = $scope.Prestacion.CoberturaId;
        c.PorcentajeCobertura = $scope.Prestacion.PorcentajeCobertura;
        c.MontoAprobado = montoAutorizar = getMontoAutorizado(c);

        if (montoAutorizar == 0) {
            alert('No se puede agregar la autorizacion, monto autorizado es igual a Cero -> (0.00 RD$)');
            return;
        }

        $scope.Prestacion.PrestacionId = 0;
        $scope.typeaheadValue = "";
        $scope.Prestacion.CoberturaId = 0;
        $scope.Prestacion.MontoAprobado = 0;
        $scope.Prestacion.Cantidad = 0;
        $scope.Prestacion.Tarifa = 0;

        $scope.Autorizacion.Afiliado.MDMedicamentos -= c.MontoAprobado;

        $scope.Autorizacion.Prestaciones.push(c);
    }

    $scope.Remover = function (index) {
        var p = GetPrestacionByIndex(index);
        $scope.Autorizacion.Prestaciones.splice(index, 1);

        $scope.RecalcularAprobado();
    }

    $scope.Edit = function (index) {
        var p = GetPrestacionByIndex(index);

        $scope.Prestacion.Id = p.Id;
        $scope.Prestacion.CoberturaId = p.CoberturaId;
        $scope.typeaheadValue = p.Nombre;
        $scope.Prestacion.Cantidad = p.Cantidad;
        $scope.Prestacion.Tarifa = p.Tarifa;

        $scope.Autorizacion.Prestaciones.splice(index, 1);
        $scope.RecalcularAprobado();
        $('#txtTarifa').focus();
    }

    $scope.BuscarAfiliado = function () {

        $scope.BuscandoAfiliado = true;

        $http({
            url: 'api/Afiliado/' + $scope.Autorizacion.Afiliado.Carnet,
            method: "GET",

        }).success(function (data) {
            $scope.DisponibleInicial = data.MDMedicamentos;
            $scope.OldCarnet = data.Carnet;
            $scope.Autorizacion.Prestaciones = [];
            $scope.Autorizacion.Afiliado = data
            //$scope.BuscandoAfiliado = false;

            var args = {
                afiliadoId: data.Id,
                tipoAutorizacionId: 12
            }

            $http({
                url: '/Buscar/GetDetallesPlanPorAfiliado',
                method: "POST",
                data: args
            }).success(function (data) {
                $scope.Prestaciones = data;

                x = $scope.Prestaciones;

                $scope.BuscandoAfiliado = false;
            }).error(function (data, status, headers, config) {
                alert('No hay prestaciones disponibles para el afiliado');
                $scope.BuscandoAfiliado = false;
            });

        }).error(function (data, status, headers, config) {
            $scope.DisponibleInicial = 0;
            $scope.Autorizacion.Afiliado = $scope.CreateAfiliado();
            $scope.BuscandoAfiliado = false;
        });
    }

    $scope.Autorizar = function () {

        var autorizacion = {
            Id: 0,
            Episodio: {
                Id: 0,
                AfiliadoId: $scope.Autorizacion.Afiliado.Id
            },
            Fecha: $scope.Autorizacion.Fecha,
            TipoAutorizacionId: 12,
            Prestaciones: $.map($scope.Autorizacion.Prestaciones, function (p, index) {
                return {
                    Id: 0,
                    DetallePlanId: p.DetallePlanId,
                    Cantidad: p.Cantidad,
                    Tarifa: p.Tarifa,
                    CoPago: $scope.Copago(index),
                    PorcentajeCobertura: p.PorcentajeCobertura * 100,
                    MontoAprobado: p.MontoAprobado
                };
            })
        }

        $http({
            url: 'api/Autorizacion',
            method: "POST",
            data: autorizacion,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (result) {
            alert(result.Message);

            var autorizacion = result.Result;

            $scope.Autorizacion = $scope.CreateAutorizacion(autorizacion);
            $scope.Limpiar();

        });
    }

    $scope.Limpiar = function () {
        $scope.Autorizacion = new $scope.CreateAutorizacion();
    }

    $scope.RecalcularAprobado = function () {
        var sum = 0;
        $scope.Autorizacion.Afiliado.MDMedicamentos = $scope.DisponibleInicial;

        $.map($scope.Autorizacion.Prestaciones, function (p, index) {
            p.MontoAprobado = getMontoAutorizado(p);
            $scope.Autorizacion.Afiliado.MDMedicamentos -= p.MontoAprobado;
        });
    }

    $scope.BalanceDisponibleMedicamentos = function () {
        return $scope.Autorizacion.Afiliado.MDMedicamentos - parseFloat($scope.AutorizadoTotal())
    }

    $scope.typeaheadFn = function (query) {
        return $.map($scope.Prestaciones, function (prestacion) {
            return prestacion.Nombre;
        });
    }

    autorizacionesScope = $scope;

    $scope.Prestadora = "Farma Ralam Express Srl";
});



$(document).ready(function myfunction() {
    $('.typeahead').typeahead({
        source: function (query, process) {
            objects = [];
            map = {};

            var data = x;

            $.each(data, function (i, object) {
                map[object.Nombre] = object;
                objects.push(object.Nombre);
            });
            process(objects);
        },
        updater: function (item) {
            var element = map[item]

            autorizacionesScope.Prestacion.DetallePlanId = element.DetallePlanId;
            autorizacionesScope.Prestacion.PorcentajeCobertura = element.PorcentajeCobertura;
            autorizacionesScope.Prestacion.CoberturaId = element.CoberturaId;
            autorizacionesScope.Prestacion.Cantidad = $scope.Prestacion.Cantidad <= 0 ? 1 : $scope.Prestacion.Cantidad;
            autorizacionesScope.Prestacion.Tarifa = $scope.Prestacion.Tarifa <= 0 ? 0 : $scope.Prestacion.Tarifa;
            autorizacionesScope.Prestacion.Nombre = element.Nombre;

            $('#txtCoberturaId').val(element.CoberturaId);

            return item;
        }
    })

    $('.typeahead').click(function () {
        $(this).select();
    });
});



myModule.controller('GeneralCtrl', function ($scope, $http) {
    $scope.MostrarDesarrollo = false;
    $scope.DbName = "";

    $http({
        url: '/Buscar/CheckConnection',
        method: "GET",
        headers: { 'Content-Type': 'application/json' }
    }).success(function (dbName) {

        if (dbName.indexOf("s-dev00") || dbName.indexOf("DevSemma") > 0) {
            $scope.MostrarDesarrollo = true;
            $scope.DbName = dbName;
        }
        else {
            MostrarDesarrollo = false;
        }
    });
});

function IsNullOrEmpty(value) {
    return (value == null || value == undefined || value == "");
}



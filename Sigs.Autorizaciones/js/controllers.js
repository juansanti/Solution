/// <reference path="Models.js" />
/// <reference path="angular.js" />
/// <reference path="../js/Helpers.js" />


var x = [];
var autorizacionesScope;

app.controller('AutorizarCtrl', function ($scope, $routeParams, $http, $filter) {
    $thescope = $scope;
    $scope.BuscandoAfiliado = false;
    $scope.DisponibleInicial = 0;

    $scope.Prestadora = {
        Id: 1,
        Nombre: "Centro Médico Dominico Cubano"
    };

    $scope.TiposAutorizacion = [
       { Id: 0, Nombre: "Complementario" },
       { Id: 1, Nombre: "Prevención y Promoción" },
       { Id: 2, Nombre: "Atención Ambulatoria" },
       { Id: 3, Nombre: "Servicios Odontológicos" },
       { Id: 4, Nombre: "Emergencia" },
       { Id: 5, Nombre: "Hospitalización" },
       { Id: 6, Nombre: "Partos" },
       { Id: 7, Nombre: "Cirugía" },
       { Id: 8, Nombre: "Apoyo Diagnóstico (Dx) en Internamiento y Ambulatorio" },
       { Id: 9, Nombre: "Atenciones de Alto Costo y de Máximo Nivel de Complejidad" },
       { Id: 10, Nombre: "Rehabilitación" },
       { Id: 11, Nombre: "Hemoterapia" },
       { Id: 12, Nombre: "Medicamentos Ambulatorios" },
    ];

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

    $scope.AgregarCobertura = function () {

        var prestacion = $scope.Prestacion;

        if (!prestacion.Tarifa || isNaN(prestacion.Tarifa)) {
            alert('Favor colocar el monto a solicituar');
            $('#txtTarifa').select();
            return;
        }

        var c = $scope.CreatePrestacionAutorizacion();

        c.Cantidad = prestacion.Cantidad;
        c.Tarifa = prestacion.Tarifa

        c.DetallePlanId = prestacion.DetallePlanId;
        c.Nombre = prestacion.Nombre;
        c.CoberturaId = prestacion.CoberturaId;
        c.PorcentajeCobertura = prestacion.PorcentajeCobertura;

        c.MontoAprobado = 0;

        prestacion.PrestacionId = 0;

        $scope.typeaheadValue = "";

        prestacion.CoberturaId = 0;
        prestacion.MontoAprobado = 0;
        prestacion.Cantidad = 0;
        prestacion.Tarifa = 0;

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
            url: '/Afiliado/Get/',
            method: "POST",
            data: { id: $scope.Autorizacion.Afiliado.Id }
        }).success(function (data) {
            $scope.DisponibleInicial = data.MDMedicamentos;
            $scope.OldCarnet = data.Carnet;
            $scope.Autorizacion.Prestaciones = [];
            $scope.Autorizacion.Afiliado = data

            var args = {
                afiliadoId: data.Id,
                tipoAutorizacionId: 12
            }

            $http({
                url: '/Buscar/GetPrestaciones',
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

        if (!$scope.Autorizacion.FechaServicio) {
            alert('Favor seleccione la fecha de servicio antes de solicitar');
            return;
        }

        var autorizacion = {
            Id: 0,
            AfiliadoId: $scope.Autorizacion.Afiliado.Id,
            FechaServicio: $scope.Autorizacion.FechaServicio,
            TipoAutorizacionId: $scope.TipoAutorizacion.Id,
            PrestadoraId: $scope.Prestadora.Id, //depende de el usuario del 
            Prestaciones: $.map($scope.Autorizacion.Prestaciones, function (p, index) {
                return {
                    Id: 0,
                    PrestacionId: p.DetallePlanId,
                    Cantidad: p.Cantidad,
                    Tarifa: p.Tarifa
                };
            })
        }

        $http({
            url: '/Autorizacion/Solicitar',
            method: "POST",
            data: autorizacion,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (result) {
            $scope.ResultadoAnalisis = result;

            $('#expertResult').modal('show');

        });
    }

    $scope.PersistirAutorizacion = function () {

        if (!$scope.ResultadoAnalisis.Procede) {
            return;
        }

        var autorizacion = $scope.ResultadoAnalisis.Autorizacion;

        autorizacion.DiagnosticoId = null;
        autorizacion.FechaServicio = $scope.Autorizacion.FechaServicio;

        $('#expertResult').modal('hide');

        $http({
            url: '/Autorizacion/Guardar',
            method: "POST",
            data: autorizacion
        }).success(function (result) {

            $scope.Auth = result;
            $('#ModalAutorizacion').modal('show')

        }).error(function (data, status, headers, config) {
            alert(data);
        });
    }

    $scope.Limpiar = function () {
        $scope.Autorizacion = new $scope.CreateAutorizacion();
        $scope.TipoAutorizacion = null;

        $scope.Auth = null;
        $scope.ResultadoAnalisis = null;
    }

    $scope.QuitarTipoAutorizacion = function (autorizacion) {

        var remover = function () {
            $scope.Autorizacion.Prestaciones = [];
            $scope.TipoAutorizacion = null;
        }

        if (autorizacion.Prestaciones.length > 0) {
            var proceguir = confirm("Desea selecionar otro tipo de autorización? se eliminaran los servicios que ha agregado a la solicitud");

            if (proceguir) {
                remover();
            }
        }
        else {
            remover();
        }
    }

    $scope.ContinuarAutorizando = function (autorizacion) {
        $scope.QuitarTipoAutorizacion(autorizacion);

        $scope.Auth = null;
        $scope.ResultadoAnalisis = null;

        $('#ModalAutorizacion').modal('hide');
    }

    $scope.RecalcularAprobado = function () {
        var sum = 0;
        $scope.Autorizacion.Afiliado.MDMedicamentos = $scope.DisponibleInicial;

        $.map($scope.Autorizacion.Prestaciones, function (p, index) {
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

    $scope.Today = Today();

    $scope.SelectionarTipoAutorizacion = function (tipoAutorizacion) {
        x = $filter('filter')($scope.Prestaciones, { GrupoId: tipoAutorizacion.Id }, null /*comparator*/)

        $scope.TipoAutorizacion = tipoAutorizacion;
    }

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

app.controller('GeneralCtrl', function ($scope, $http) {
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



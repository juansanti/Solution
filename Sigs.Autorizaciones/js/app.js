
var app = angular.module('autorizaciones', []);

app.directive('datepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(function () {
                element.datepicker({
                    dateFormat: 'dd/mm/yy',
                    onSelect: function (date) {
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    },
                    regional: "es",
                    maxDate: attrs.maxDate,
                    changeMonth: true,
                    changeYear: true
                });
            });
        }
    }
});

app.directive('onEnter', function () {

    var linkFn = function (scope, element, attrs) {
        element.bind("keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.onEnter);
                });
                event.preventDefault();
            }
        });
    };

    return {
        link: linkFn
    };
});

function reportBuilder() {
    var builder =
        {
            titulo: '',
            reporte: 0,
            parametros: []
        };

    return builder;
}

function NewReportParameter(name, value) {
    var parametro = {
        name: name,
        value: value,
    };

    return parametro;
}

function GetUrl(builder) {
    var url = '/Imprimir.aspx?Reporte=' + builder.reporte

    $.map(builder.parametros, function (p, index) {
        url += '&' + p.name + '=' + p.value;
    });

    return url;
}

function Imprimir(builder) {

    $("#reportWindowUrl").html("<iframe src='' style='width: 95%; height: 700px;'></iframe>");

    var url = GetUrl(builder);

    $("#reportWindowUrl").html("<iframe src='" + url + "' style='width: 95%; height: 700px;'></iframe>");

    $('#reportWindowTitle').html(builder.titulo);
    $('#reportWindowUrl').attr("src", url);

    $('#reportWindow').css({ width: "900px" });
    $('#reportWindow').modal("show");
}

app.controller('NavBarCtrl', function ($scope) {
    $scope.EstoyActivo = function (name, current) {
        if (name == current) {
            return "active"
        }
    }
})

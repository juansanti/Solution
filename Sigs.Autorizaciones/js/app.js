
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
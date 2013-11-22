/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/moment.js" />

function GetErrorList(errors) {
    var html = '<ul>';

    $.map(errors, function (e) {
        html += '<li>';
        html += e;
        html += '</li>';
    });

    html += '</ul>';

    return html;
}

var Today = function () {
    var date = new Date();

    var dia = date.getDate();
    var mes = date.getMonth() + 1;
    var ano = date.getFullYear();

    var str = dia < 10 ? "0" + dia : dia + "/";
    str += mes < 10 ? "0" + mes : mes;
    str += "/" + ano;

    return str;
}

function GetFechaActualString() {
    var fecha = new Date();

    return getStringFromDate(fecha);
}

function getStringFromDate(fecha) {

    var dia = fecha.getDate().toString();
    dia = dia.length == 1 ? "0" + dia : dia;

    var mes = (fecha.getMonth() + 1).toString();
    mes = mes.length == 1 ? "0" + mes : mes;

    var ano = fecha.getFullYear().toString();

    return dia + "/" + mes + "/" + ano;
}

function TotalDays(start, end) {

    var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    var firstDate = start;
    var secondDate = end;

    var diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));

    return diffDays;
}

//Necesita momentjs
function getFechaFromString(fechaString) {
    return moment(fechaString, 'DD-MM-YYYY').toDate();
}

function GenerarLista(collection) {
    var result = "<ul>";

    $(collection).each(function (index, item) {
        result += "<li>" + item + "</li>";
    });

    result += "</ul>";

    return result;
}

function ValidarCedula(cedula) {
    var valido = true;
    var errors = [];

    if (IsNullOrEmpty(cedula)) {
        errors.push('Favor especificar la cédula para buscar');
        valido = false;
    }
    else {
        if (cedula.length != 11) {
            errors.push('La cédula tiene ' + cedula.length + ' dígitos, debe tener 11');
            valido = false;
        }
        else if (isNaN(cedula)) {
            errors.push('Cédula inválida, no puede contener letras, especios y/o caracteres especiales');
            valido = false;
        }
    }

    var result = {
        valido: valido,
        errors: errors
    }

    return result;
}

function FechaComparer(a, b) {
    if (getFechaFromString(a.Fecha) < getFechaFromString(b.Fecha))
        return -1;
    if (getFechaFromString(a.Fecha) > getFechaFromString(b.Fecha))
        return 1;
    return 0;
}


function ValidarRNCEmpleador(rnc) {
    var valido = true;
    var errors = [];

    if (IsNullOrEmpty(rnc)) {
        valido = false;
        errors.push('Favor introducir el RNC');
    }
    else {
        if (isNaN(rnc)) {
            valido = false;
            errors.push('El rnc debe ser un número, no puede contener espacios, guiones, letras y/o caracteres especiales');
        }
        else if (rnc.length != 11 && rnc.length != 9) {
            valido = false;
            errors.push('Este número tiene ' + rnc.length + ' dígitos, debe tener 11 ó 9');
        }
    }

    var result = {
        valido: valido,
        errors: errors
    }

    return result;
}

function getMes(mes) {
    if (mes === 1) {
        return "ENE";
    } else if (mes === 2) {
        return "FEB";
    } else if (mes === 3) {
        return "MAR";
    } else if (mes === 4) {
        return "ABR";
    } else if (mes === 5) {
        return "MAY";
    } else if (mes === 6) {
        return "JUN";
    } else if (mes === 7) {
        return "JUL";
    } else if (mes === 8) {
        return "AGO";
    } else if (mes === 9) {
        return "SEP";
    } else if (mes === 10) {
        return "OCT";
    } else if (mes === 11) {
        return "NOV";
    } else if (mes === 12) {
        return "DIC";
    } else {
        return "Desconocido";
    }
}

function IsNullOrEmpty(value) {
    return value == null || value == undefined || value == "";
}

function IsNullOrEmptyOrCero(value) {
    return IsNullOrEmpty(value) || value == 0;
}
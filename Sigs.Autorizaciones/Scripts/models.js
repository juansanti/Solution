function ctrAutorizacion(a) {
    var aut = {
        Id: 0,
        AfiliadoId: 0,
        Afiliado: ctrAfiliado(),
        Prestaciones: [],
        Fecha: "",
        UsuarioId: 0,
        Usuario: "",
    };

    if (a != null) {
        aut.Id = a.Id;
        aut.AfiliadoId = a.AfiliadoId;
        aut.Afiliado = a.Afiliado;
        aut.Prestaciones = a.Prestaciones;
        aut.Fecha = a.Fecha;
        aut.UsuarioId = a.UsuarioId;
        aut.Usuario = a.Usuario;
    }
    return aut;
}

function ctrAfiliado(a) {
    var af = {
        Id: 0,
        Carnet: 0,
        Nombre: "",
        Edad: 0,
        Sexo: 0,
        MDMedicamentos: 0,
        PuedeAutoriar: false,
        Impedimentos: ""
    }

    if (a != null) {
        af.Id = a.Id;
        af.Carnet = a.Carnet;
        af.Nombre = a.Nombre;
        af.Edad = a.Edad;
        af.Sexo = a.Sexo;
        af.MDMedicamentos = a.MDMedicamentos,
        af.PuedeAutoriar = a.PuedeAutoriar,
        af.Impedimentos = a.Impedimentos
    }

    return af;
}

function ctrPrestacion() {
    var obj = {
        Id: 0,
        CoberturaId: 0,
        Cobertura: "",
        SubGrupoId: "",
        SubGrupo: "",
        Tarifa: 0
    }

    return obj;
}

function ctrPrestacionAutorizacion(p) {
    var pr = {
        Id: 0,
        PrestacionId: 0,
        CoberturaId: 0,
        Tarifa: 100,
        Cantidad: 0,
        Nombre: "",
        FechaCreacion: "",
        Usuario: "",
        Porcentaje: .70
    }

    if (p != null) {
        Id = p.Id;
        PrestacionId = p.PrestacionId;
        CoberturaId = p.CoberturaId;
        Tarifa = p.Tarifa;
        Cantidad = p.Cantidad;
        Cubierto = p.Cubierto;
        Restante = p.Restante;
        Nombre = p.Nombre;
        FechaCreacion = p.FechaCreacion;
        Usuario = p.Usuario;
    }

    pr.Cubierto = function () {
        return pr.Tarifa * pr.Porcentaje;
    }

    pr.Restante = function () {
        return pr.Tarifa - pr.Cubierto();
    }

    return pr;
}

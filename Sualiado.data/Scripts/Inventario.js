
window.onload = function Mostrar() {
    var cantidad = document.getElementById("can").value;
    if (cantidad == "1") {
        Ventas(cantidad);
        $(document).ready(
            function () {
                $('#tablaventas').DataTable({
                    dom: 'frtip',
                    buttons: [
                        'pdfHtml5'
                    ],
                    searching: false,
                    lengthChange: false,
                    language: {
                        info: "Mostrando _END_ de _TOTAL_ Entradas",
                        paginate: {
                            previous: '‹',
                            next: '›'
                        }
                    }

                });
            });
        document.getElementById("ventasBtn").style.backgroundColor = '#ce2552';
        document.getElementById("ventasBtn").style.color = 'white';
    } else if (cantidad == "productos") {
        MostrarInventario(producto);
        $(document).ready(
            function () {
                $('#mitabla').DataTable({
                    dom: 'frtip',
                    buttons: [
                        'pdfHtml5'
                    ],
                    searching: false,
                    lengthChange: false,
                    language: {
                        info: "Mostrando _END_ de _TOTAL_ Entradas",
                        paginate: {
                            previous: '‹',
                            next: '›'
                        }
                    }

                });
            });
        document.getElementById("inventarioBtn").style.backgroundColor = '#ce2552';
        document.getElementById("inventarioBtn").style.color = 'white';
    } else if (cantidad == "entradas") {
        MostrarEntrada();
        document.getElementById("comprasBtn").style.backgroundColor = '#ce2552';
        document.getElementById("comprasBtn").style.color = 'white';
    } else if (cantidad == "proveedores") {
        MostrarProveedor();
        document.getElementById("proveedoresBtn").style.backgroundColor = '#ce2552'; 
        document.getElementById("proveedoresBtn").style.color = 'white';
    } else if (cantidad == "empleados") {
        MostrarEmpleados();
        document.getElementById("empleadosBtn").style.backgroundColor = '#ce2552';
        document.getElementById("empleadosBtn").style.color = 'white';
    } else if (cantidad == "reportes") {
        MostrarGrafica();
    }
    else if (cantidad == "reportesDaños") {
        MostrarReportesDaños("");
        document.getElementById("reportesBtnnn").style.backgroundColor = '#ce2552';
        document.getElementById("reportesBtnnn").style.color = 'white';
    } else if (cantidad == "index") {
        MostrarIndex();
    } else if (cantidad == "inicio") {
        CargarInicio();
        document.getElementById("inicioBtn").style.backgroundColor = '#ce2552';
        document.getElementById("inicioBtn").style.color = 'white';
    } else if (cantidad == "config") {
        MostrarInfoPersonal();
    } else if (cantidad == "tienda") {
        MostrarTienda()
    } else if (cantidad == "agenda") {
        MostrarLista();
        TareasIncompletas();
        TareasCompletas();
        document.getElementById("agendaBtn").style.backgroundColor = '#ce2552';
        document.getElementById("agendaBtn").style.color = 'white';
    } else if (cantidad == "rep") {
        document.getElementById("reportesBtnnn").style.backgroundColor = '#ce2552';
        document.getElementById("reportesBtnnn").style.color = 'white';
    }
}
function MostrarLista() {
    var xhr = new XMLHttpRequest();
    var url = "/Agenda/MostrarEmpleados";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("menu").innerHTML += respuesta;
        }
    };
    xhr.send();
}
function CambiarEstTarea(a) {
    var estado = document.getElementById(a).value;
    if (estado == 0) {
        document.getElementById(a).value=1;
        estado = 1;
    } else {
        document.getElementById(a).value=0;
        estado = 0;
    }
    dataObject = JSON.stringify({
        'estado': estado,
        'id': a,
    });
    var params = dataObject;
    var xhr = new XMLHttpRequest();
    var url = "/Agenda/CambiarEstado";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
        }
    };
    xhr.send(params);
}
function TareasIncompletas() {
    var xhr = new XMLHttpRequest();
    var url = "/Agenda/MostrarIncompletas";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("tareasInc").innerHTML = respuesta;
        }
    };
    xhr.send();
}
function TareasCompletas() {
    var xhr = new XMLHttpRequest();
    var url = "/Agenda/MostrarCompletas";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("tareasCom").innerHTML = respuesta;
        }
    };
    xhr.send();
}
function Asignar() {
    var persona = document.getElementById("menu").value;
    var descripcion = document.getElementById("tarea").value;
    dataObject = JSON.stringify({
        'persona': persona,
        'descripcion': descripcion,
    });
    var params = dataObject;
    var xhr = new XMLHttpRequest();
    var url = "/Agenda/Agregar";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("correctamente")) {
                Swal.fire(
                    respuesta,
                    '',
                    'success')
                TareasIncompletas();
            } else {
                Swal.fire(
                    respuesta,
                    '',
                    'error')
            }

        }
    };
    xhr.send(params);
}

function CambiarEstado(a, b) {
    var dataObject;
    if (b.includes("deshabilitado")) {
        dataObject = JSON.stringify({
            'estado': "deshabilitado",
            'id': a,
        });
    } else {
        dataObject = JSON.stringify({
            'estado': "habilitado",
            'id': a,
        });
    }
    var xhr = new XMLHttpRequest();
    var url = "/Empleado/CambiarEstado";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("true")) {
                Swal.fire(
                    'Se ha cambiado el estado del empleado',
                    '',
                    'success')
                MostrarEmpleados();
            } else {
                Swal.fire(
                    'No se ha podido cambiar el estado del empleado',
                    '',
                    'success')

            }
           
        }
    };
    xhr.send(params);
}
function BuscarPersona(a) {
    var campo = a;
    if (campo =="") {
        return MostrarEmpleados();
    }
    var xhr = new XMLHttpRequest();
    var url = "/Empleado/Buscar";
    xhr.open("POST", url, false);
    var dataObject = JSON.stringify({
        'campo': campo,
    });
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("emp").innerHTML = respuesta;

        }
    };
    xhr.send(params);
}
function MostrarProveedor() {
    var producto = "mostrar";
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'cantidad': producto,
    });
    var url = "/Proveedor/Proveedores";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("Proveedores").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
function MostrarEmpleados() {
    var xhr = new XMLHttpRequest();
    var url = "/Empleado/Mostrar";
    var dataObject = JSON.stringify({
        'pagina': "",
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("emp").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
function MostrarTienda() {
    var xhr = new XMLHttpRequest();
    var url = "/Tienda/Mostrar";
    var dataObject = JSON.stringify({
        'pagina': "",
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("pro").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
function MostrarInfoPersonal() {
    var xhr = new XMLHttpRequest();
    var url = "/Configuracion/Mostrar";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);

            document.getElementById("nombre").value = respuesta[0];
            document.getElementById("apellido").value = respuesta[1];
            document.getElementById("direccion").value = respuesta[2];
            document.getElementById("telefono").value = respuesta[3];
            document.getElementById("cargo").value = respuesta[4];
            document.getElementById("usuario").value = respuesta[5];
            document.getElementById("correo").value = respuesta[6];
            document.getElementById("genero").value = respuesta[7];
        }
    };
    xhr.send();
}
function AgregarEmpleado() {
    var nombre = document.getElementById("nbre").value;
    var apellido = document.getElementById("ape").value;
    var direccion = document.getElementById("dic").value;
    var telefono = document.getElementById("tel").value;
    var cedula = document.getElementById("cedula").value;
    var correo = document.getElementById("correo").value;
    var cargo = document.getElementById("menu").value;
    var genero = document.getElementById("gen").value;
    var dataObject = JSON.stringify({
        'nombre': nombre,
        'apellido': apellido,
        'direccion': direccion,
        'telefono': telefono,
        'cedula': cedula,
        'correo': correo,
        'genero': genero,
        'cargo': cargo,
    });
    var params = dataObject;
    var xhr = new XMLHttpRequest();
    var url = "/Empleado/Agregar";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("fue agregado")) {
                Swal.fire(
                    'Empleado agregado',
                    '',
                    'success')
                document.getElementById("nbre").value = "";
                document.getElementById("ape").value = "";
                document.getElementById("dic").value = "";
                document.getElementById("tel").value = "";
                document.getElementById("cedula").value = "";
                document.getElementById("correo").value = "";
                document.getElementById("menu").value = "";
                document.getElementById("gen").value = "";
                MostrarEmpleados();

            } else if (respuesta.includes("No se pudo")) {
                Swal.fire(
                    respuesta,
                    '',
                    'error')
            } else if (respuesta.includes("base de datos")) {
                Swal.fire(
                    respuesta,
                    '',
                    'error')
            } else if (respuesta.includes("obligatorios")) {
                Swal.fire(
                    respuesta,
                    '',
                    'error')
            }
        }
    };
    xhr.send(params);
}
function EditarInfoPersonal() {
    var nombre = document.getElementById("nombre").value;
    var apellido = document.getElementById("apellido").value;
    var direccion = document.getElementById("direccion").value;
    var telefono = document.getElementById("telefono").value;
    var usuario = document.getElementById("usuario").value;
    var correo = document.getElementById("correo").value;
    var genero = document.getElementById("menu").value;
    if (genero != "seleccionar") {
        var genero = document.getElementById("menu").value;
    } else {
        var genero = document.getElementById("genero").value;
    }
    var dataObject = JSON.stringify({
        'nombre': nombre,
        'apellido': apellido,
        'direccion': direccion,
        'telefono': telefono,
        'usuario': usuario,
        'correo': correo,
        'genero': genero,
    });
    var params = dataObject;
    var xhr = new XMLHttpRequest();
    var url = "/Configuracion/Editar";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta == "TRUE") {
                Swal.fire(
                    'Tus datos se actualizaron correctamente',
                    '',
                    'success')
                MostrarInfoPersonal();
            } else {
                Swal.fire(
                    'No se ha podido actualizar tus datos',
                    '',
                    'error')
            }


        }
    };
    xhr.send(params);
}

function CambiarContraseña() {
    var contraseña = document.getElementById("contra").value;
    var nueva = document.getElementById("contraN").value;
    var confirmacion = document.getElementById("contraC").value;
    var dataObject = JSON.stringify({
        'contraseña': contraseña,
        'nueva': nueva,
        'confirmacion': confirmacion,
    });
    var xhr = new XMLHttpRequest();
    var url = "/Configuracion/Contraseña";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("success")) {
                document.getElementById("contra").value = "";
                document.getElementById("contraN").value = "";
                document.getElementById("contraC").value = "";
            }

            Swal.fire(
                respuesta[1],
                '',
                respuesta[0])

        }
    };
    xhr.send(params);
}

function BuscarHome() {
    var cantidad = document.getElementById("can").value;
    var campo = document.getElementById("campo").value;
    var dataObject = JSON.stringify({
        'cantidad': cantidad,
        'campo': campo,
    });
    var xhr = new XMLHttpRequest();
    var url = "/Home/Buscar";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);

            document.getElementById("pro").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
function Color(a) {
    document.getElementById(a).style.backgroundColor = '#F52338';
}
function Reportar() {
    var refe = document.getElementById("referencia").value;
    var tip = document.getElementById("menuDaño").value;
    var obs = document.getElementById("obs").value;
    var can = document.getElementById("cant").value;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'referencia': refe,
        'tipo': tip,
        'observacion': obs,
        'cantidad': can,
    });
    var params = dataObject;
    var url = "/Daños/Reportar";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta == "TRUE") {
                Swal.fire(
                    'Se ha resgistrado el daño!',
                    '',
                    'success')
                document.getElementById("obs").value = "";
                document.getElementById("cant").value = "";
            } else {
                Swal.fire(
                    'No se ha podido registar el daño!',
                    '',
                    'error')
            }
        }

    };
    xhr.send(params);

}
function MostrarTodo() {
    var xhr = new XMLHttpRequest();
    var url = "/Daños/MostrarReporte";
    var dataObject = JSON.stringify({
        'referencia': "referencia",
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("descripcion").innerHTML += respuesta;
            document.getElementById('repo2').style.display = "block";
            $(document).ready(
                function () {
                    $('#mitabla1').DataTable({
                        dom: 'frtip',
                        buttons: [
                            'pdfHtml5'
                        ],
                        searching: false,
                        lengthChange: false,
                        language: {
                            info: "Mostrando _END_ de _TOTAL_ Entradas",
                            paginate: {
                                previous: '‹',
                                next: '›'
                            }
                        }

                    });
                });

        }

    };
    xhr.send(params);
}
function Ocultar() {
    document.getElementById("descripcion").innerHTML = "";
    document.getElementById('repo2').style.display = "none";
}
function MostrarCampos(a) {
    var referencia = a.id;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'referencia': referencia,
    });
    var params = dataObject;
    var url = "/Daños/TraerCampos";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("referencia").value = respuesta[0][1];
            var x = document.getElementById("menuDaño");
            if (x.length != 0) {
                x.remove(x.option);
                x.remove(x.option);
                x.remove(x.option);
                x.remove(x.option);
            }
            for (var i = 0; i < respuesta[1].length; i++) {
                var option = document.createElement("option");
                option.text = respuesta[2][i];
                option.value = respuesta[1][i];
                x.add(option);
            }
        }

    };
    xhr.send(params);
}
function MostrarReportesDaños(pro) {
    var producto = pro;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'cantidad': producto,
    });
    var url = "/Daños/Mostrar";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("ProductosParaReportar").innerHTML = respuesta;
        }

    };
    xhr.send(params);
}
function MostrarIndex() {
    var xhr = new XMLHttpRequest();
    var url = "/Home/Mostrar";
    var dataObject = JSON.stringify({
        'mostrar': "pagina",
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("products").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
function MostrarContraseña() {
    var tipo = document.getElementById("pass");
    if (tipo.type == "password") {
        tipo.type = "text";
    } else {
        tipo.type = "password";
    }

}
function ConfirmarContraseña() {
    var contraseña = document.getElementById("contraseñaNueva").value;
    var confirmacion = document.getElementById("confirmacion").value;
    var xhr = new XMLHttpRequest();
    var url = "/Login/ConfirmarContraseña";
    var dataObject = JSON.stringify({
        'contraseña': contraseña,
        'confirmacion': confirmacion,
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("Login")) {
                location.href = respuesta;
            } else {
                document.getElementById("mensage").innerHTML = respuesta;
                document.getElementById("mensage").style.color = "red"
            }
        }
    };
    xhr.send(params);

}
function Confirmar() {
    var identificacion = document.getElementById("codigo").value;
    var xhr = new XMLHttpRequest();
    var url = "/Login/ConfirmarCodigo";
    var dataObject = JSON.stringify({
        'codigo': identificacion,
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("Recuperacion")) {
                document.getElementById("login").innerHTML = respuesta;
            } else {
                document.getElementById("mensage").innerHTML = respuesta;
                document.getElementById("mensage").style.color = "red"
            }
        }
    };
    xhr.send(params);
}
function Codigo() {
    var identificacion = document.getElementById("usu").value;
    var xhr = new XMLHttpRequest();
    var url = "/Login/EnviarCorreo";
    var dataObject = JSON.stringify({
        'usuario': identificacion,
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("correo")) {
                document.getElementById("login").innerHTML = respuesta;
            } else {
                document.getElementById("mensage").innerHTML = respuesta;
                document.getElementById("mensage").style.color = "red"
            }
        }
    };
    xhr.send(params);
}
function CambiarClave() {
    identificacion = document.getElementById("usu").value;
    var xhr = new XMLHttpRequest();
    var url = "/Login/Recuperacion";
    var dataObject = JSON.stringify({
        'usuario': identificacion,
    });
    var params = dataObject;
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("login").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
var p = document.getElementById("pass");
p.onpaste = function () {
    return false;
}
function Redireccionar(modulo) {
    var url;
    switch (modulo) {
        case "proveedores":
            url = "/Proveedor/Proveedores";
            break;
        case "ventas":
            url = "/Ventas/Ventas";
            break;
        case "reportes":
            url = "/Reportes/Reportes";
            break;
        case "productos":
            url = "/Producto/Inventario";
            break;
        case "daños":
            url = "/Daños/ReporteDaños";
            break;
        case "empleados":
            url = "/Empleado/Empleado";
            break;
        case "entradas":
            url = "/Entradas/Entrada";
            break;
        case "login":
            url = "/Login/Login";
            break;

    }
    var pagina = "proveedores";
    var xhr = new XMLHttpRequest();

    xhr.open("GET", url, false);
    var params = "{pagina: " + pagina + "}";
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            location.href = url;
        }

    };
    xhr.send(params);
}
function MostrarInfo(evt, nbreReporte) {

    if (document.getElementById(nbreReporte).style.display == "block") {
        document.getElementById(nbreReporte).style.display = "none";
    } else {
        document.getElementById(nbreReporte).style.display = "block";
    }
    evt.currentTarget.className += " active";
}
function AgregarStock() {
    var proId = document.getElementById("proId").value;
    var prove = document.getElementById("prove").value;
    var cant = document.getElementById("canti").value;
    var precioC = document.getElementById("preComp").value;
    var xhr = new XMLHttpRequest();
    var url = "/Entradas/AgregarEntrada";
    xhr.open("POST", url, false);
    var dataObject = JSON.stringify({
        'proveedor': prove,
        'producto': proId,
        'cantidad': cant,
        'precioC': precioC,
    });
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            alerta(respuesta);
            function alerta(confirm) {
                if (confirm == "true") {

                    Swal.fire(
                        'La compra fue exitosa!',
                        '',
                        'success'
                    )
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Campos vacios',
                        text: 'No puedes dejar campos vacios',
                    })

                }
            }
            MostrarInventario();
        }

    };
    xhr.send(params);
}
function AgregarEntrada(a) {
    var campo = a.id;
    var xhr = new XMLHttpRequest();
    var url = "/Entradas/EditarEntrada";
    xhr.open("POST", url, false);
    var dataObject = JSON.stringify({
        'campo': campo,
    });
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("date").value = respuesta[0];
            document.getElementById("proId").value = respuesta[1][0];
            document.getElementById("Id").value = respuesta[1][1];
            document.getElementById("provee2").value = respuesta[1][8];
            document.getElementById("prove").value = respuesta[2][0];

        }
    };
    xhr.send(params);
}
function BuscarInventario() {
    var campo = document.getElementById("campo").value;
    if (campo == "") {
        return MostrarInventario();
    }
    var xhr = new XMLHttpRequest();
    var url = "/Producto/Buscar";
    xhr.open("POST", url, false);
    var dataObject = JSON.stringify({
        'campo': campo,
    });
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("Productos").innerHTML = respuesta;

        }
    };
    xhr.send(params);
}
function Ventas(cant) {
    var cantidad = cant;
    var xhr = new XMLHttpRequest();
    var url = "/Ventas/Ventas";
    xhr.open("POST", url, false);
    var params = "{cantidad: " + cantidad + "}";
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("DatosServer").innerHTML = respuesta;

        }
    };
    xhr.send(params);
}
function MostrarInventario(pro) {
    var producto = pro;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'cantidad': producto,
    });
    var url = "/Producto/Inventario";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("Productos").innerHTML = respuesta;
        }

    };
    xhr.send(params);
}
function Editar(a) {
    var producto = a.id;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'referencia': producto,
    });
    var url = "/Producto/Editar";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("idproEdi").value = respuesta[0][0];
            document.getElementById("ref").value = respuesta[0][1];
            document.getElementById("Nbre").value = respuesta[0][2];
            document.getElementById("precio").value = respuesta[0][3];
            document.getElementById("precioCompra1").value = respuesta[0][4];
            document.getElementById("cant").value = respuesta[0][5];
            document.getElementById("tipo").value = respuesta[0][6];
            document.getElementById("tipoV").value = respuesta[0][9];
            document.getElementById("proveedor").value = respuesta[0][7];
            document.getElementById("proveedorV").value = respuesta[0][8];
            var x = document.getElementById("menu");
            var c = document.getElementById("menuProveedor");
            x.remove(x.option);
            x.remove(x.option);
            for (var i = 0; i < 2; i++) {
                var option = document.createElement("option");
                option.text = respuesta[1][i];
                option.value = respuesta[2][i];
                x.add(option);
            }
            for (var i = 0; i < respuesta[3].length; i++) {
                c.remove(x.option);
            }
            contador = 1;
            for (var i = 0; i < respuesta[3].length; i++) {
                var option = document.createElement("option");
                option.text = respuesta[3][i];
                option.value = contador;
                c.add(option);
                contador++;
            }
        }
    };
    xhr.send(params);
}
function CargarMenus() {
    var producto = "Pb1234";
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'referencia': producto,
    });
    var url = "/Producto/Editar";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            var x = document.getElementById("menuCategoria");
            var c = document.getElementById("menupro");
            x.remove(x.option);
            x.remove(x.option);
            for (var i = 0; i < 2; i++) {
                var option = document.createElement("option");
                option.text = respuesta[1][i];
                option.value = respuesta[2][i];
                x.add(option);
            }
            for (var i = 0; i < respuesta[3].length; i++) {
                c.remove(x.option);
            }
            contador = 1;
            for (var i = 0; i < respuesta[3].length; i++) {
                var option = document.createElement("option");
                option.text = respuesta[3][i];
                option.value = contador;
                c.add(option);
                contador++;
            }
        }
    }
    xhr.send(params);
}
function Limpiar() {
    document.getElementById("referencia").value = "";
    document.getElementById("nbre").value = "";
    document.getElementById("precioVenta").value = "";
    document.getElementById("precioCompra").value = "";
    document.getElementById("menuCategoria").value = "";
    document.getElementById("menupro").value = "";
    document.getElementById("fileImage").files[0] = null;
}
function AgregarProducto() {

    var ref = document.getElementById("referencia").value;
    var nombre = document.getElementById("nbre").value;
    var precio = document.getElementById("precioVenta").value;
    var precioCompra = document.getElementById("precioCompra").value;
    var tipo = document.getElementById("menuCategoria").value;
    var proveedor = document.getElementById("menupro").value;
    var img = document.getElementById("fileImage").files[0];
    var formdata = new FormData();
    formdata.append("img", img);
    formdata.append("refe", ref);
    formdata.append("nombre", nombre);
    formdata.append("precio", precio);
    formdata.append("precioCompra", precioCompra);
    formdata.append("tipo", tipo);
    formdata.append("proveedor", proveedor);
    var xhr = new XMLHttpRequest();

    var url = "/Producto/AgregarProducto";
    xhr.open("POST", url, false);

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            alerta(respuesta);
            function alerta(confirm) {
                if (confirm == "true") {

                    Swal.fire(
                        'Producto Agregado!',
                        '',
                        'success'
                    )
                    document.getElementById("referencia").value = "";
                    document.getElementById("nbre").value = "";
                    document.getElementById("precioVenta").value = "";
                    document.getElementById("precioCompra").value = "";
                    document.getElementById("menuCategoria").value = "";
                    document.getElementById("menupro").value = "";
                    document.getElementById("fileImage").files[0] = null;
                    MostrarInventario("productos");
                } else if (confirm == "false") {
                    Swal.fire({
                        icon: 'error',
                        title: 'Campos vacios',
                        text: 'No puedes dejar campos vacios',
                    })

                } else {

                    Swal.fire({
                        icon: 'error',
                        title: '',
                        text: respuesta,
                    })
                }
            }
            MostrarInventario("productos");
        }
        console.log(this.status)
    };
    xhr.send(formdata);
}
function CambiarImagen() {
    var img = document.getElementById("file").files[0];
    var formdata = new FormData();
    formdata.append("img", img);
    var xhr = new XMLHttpRequest();
    var url = "/Configuracion/CambiarFoto";
    xhr.open("POST", url, false);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta.includes("true")) {
                Swal.fire(
                    'El cambio Fue exitoso!',
                    '',
                    'success'
                )
                setTimeout('location.reload()', 3000);

            } else {

                Swal.fire(
                    'No se ha podido actualizar tu foto de perfil!',
                    '',
                    'error'
                )
            }
        }

    };
    xhr.send(formdata);
}
function Actualizar() {

    var ref = document.getElementById("idproEdi").value;
    var nombre = document.getElementById("Nbre").value;
    var precio = document.getElementById("precio").value;
    var tipo = document.getElementById("tipoV").value;
    var proveedor = document.getElementById("proveedorV").value;
    var img = document.getElementById("file").files[0];
    var formdata = new FormData();
    formdata.append("img", img);
    formdata.append("refe", ref);
    formdata.append("nombre", nombre);
    formdata.append("precio", precio);
    formdata.append("tipo", tipo);
    formdata.append("proveedor", proveedor);
    var xhr = new XMLHttpRequest();

    var url = "/Producto/Actualizar";
    xhr.open("POST", url, false);

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            alerta(respuesta);
            function alerta(confirm) {
                if (confirm == "true") {

                    MostrarInventario();
                    Swal.fire(
                        'El cambio Fue exitoso!',
                        '',
                        'success'
                    )
                } else if (confirm == false) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Campos vacios',
                        text: 'No puedes dejar campos vacios',
                    })

                } else {

                    Swal.fire({
                        icon: 'error',
                        title: '',
                        text: 'El precio no puede ser menor al precio de compra',
                    })
                }


            }
        }

    };
    xhr.send(formdata);
}
function Estado(btn) {
    var estado = btn.value;
    var cambio = 0;
    switch (estado) {

        case "deshabilitado":
            cambio = 1;
            break;
        case "habilitado":
            cambio = 0;
            break;
    }
    var xhr = new XMLHttpRequest();
    var url = "/Producto/Estado";
    xhr.open("POST", url, false);
    var dataObject = JSON.stringify({
        'cambio': cambio,
        'referencia': btn.id,
    });
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta == "habilitado") {
                MostrarInventario();

            } else if (respuesta == "deshabilitado") {
                MostrarInventario();
            } else {
                alerta();
                function alerta() {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo habilitar/deshabilitar este producto'
                    })
                }
            }

        }
    };
    xhr.send(params);

}

function CambiarProveedor(select) {
    var n = select.selectedIndex;
    document.getElementById("proveedorV").value = select.value;
    document.getElementById("proveedor").value = select.options[n].text;
}
function CambiarCategoria(select) {
    var n = select.selectedIndex;

    document.getElementById("tipoV").value = select.value;
    document.getElementById("tipo").value = select.options[n].text;
}
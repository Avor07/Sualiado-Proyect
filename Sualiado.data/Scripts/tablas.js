
function Ventas(cantidad) {
   
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
function Buscar() {
    var cantidad = document.getElementById("can").value;
    var campo = document.getElementById("campo").value;
    var dataObject = JSON.stringify({
        'cantidad': cantidad,
        'campo': campo,
    });
    var xhr = new XMLHttpRequest();
    var url = "/Ventas/Ventas";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);

            document.getElementById("DatosServer").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
function BuscarProveedor() {
  var campo = document.getElementById("campo").value;
    if (campo == "") {
        MostrarProveedor();
    }
    var dataObject = JSON.stringify({
        'campo': campo,
    });
    var xhr = new XMLHttpRequest();
    var url = "/Proveedor/Buscar";
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
function Agregar(btn) {
    var boton = btn.value;
    var campo = document.getElementById("campo").value;
    var dataObject = JSON.stringify({
        'boton': boton,
        'campo': campo,
    });
    var xhr = new XMLHttpRequest();
    var url = "/Ventas/Ventas";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);

            document.getElementById("modalTable").innerHTML = respuesta;
            document.getElementById("confirmacion" + boton).innerHTML = "Agregado¡";
            setInterval(function () { document.getElementById("confirmacion" + boton).innerHTML = ""; }, 3000);
        }
    };
    xhr.send(params);
}
function BorrarProducto(btn) {
    var borrar = btn.value;
    var campo = document.getElementById("campo").value;
    var dataObject = JSON.stringify({
        'borrar': borrar,
        'campo': campo,
    });
    var xhr = new XMLHttpRequest();
    var url = "/Ventas/Ventas";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);

            document.getElementById("modalTable").innerHTML = respuesta;
        }
    };
    xhr.send(params);
}
function Finalizar(btn) {
    var opcion = btn.value;
    var cant = document.getElementsByName("cantidadSolicitada");
    var cantidades = Array();
    for (var i = 0; i < cant.length; i++) {
        cantidades[i] = cant[i].value;
    }
    var dataObject = JSON.stringify({
        'opcion': opcion,
        'campo': campo,
        'cantidades': cantidades,
    });
    var xhr = new XMLHttpRequest();
    var url = "/Ventas/Ventas";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            respuesta[0];
            document.getElementById("modalTable").innerHTML = respuesta[1];
            var confirm = document.getElementById("unidades").value;
            alerta(confirm);
            function alerta(confirm) {
                if (confirm == "true") {

                    Swal.fire(
                        'Buen trabajo!',
                        'La venta fue exitosa',
                        'success'
                    )
                } else if (confirm == "falseNS") {
                    Swal.fire({
                        icon: 'error',
                        title: 'Cantidad no disponible',
                        text: 'No hay suficientes unidades de este producto: ' + respuesta[0],
                    })

                } else if (confirm == "negativos") {
                    Swal.fire({
                        icon: 'error',
                        title: 'Cantidad no disponible',
                        text: 'Los valores no pueden ser negativos',
                    })
                }
                else {
                    Swal.fire({
                        imageUrl: '/content/img/carrito.jpg',
                        text: 'No hay productos en el carrito',
                    })
                }
            }
            setInterval(function () { document.getElementById("alerta").innerHTML = ""; }, 5000);
        }
    };
    xhr.send(params);
}

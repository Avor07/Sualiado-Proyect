
function MostrarEntrada() {
    var producto = "mostrar";
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'mostrar': producto,
    });
    var url = "/Entradas/MostrarEntrada";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("entradas").innerHTML = respuesta;
      
        }
        console.log(this.status)
    };
    xhr.send(params);
}
function BuscarEntrada(date) {
    var producto = date;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'mostrar': producto,
    });
    var url = "/Entradas/Buscar";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta == "") {
                Swal.fire(

                    'Entrada no encontrada!',
                    'no hay entradas en la fecha que indicaste',
                    'info'
                )
                MostrarEntrada();
            } else {

            document.getElementById("entradas").innerHTML = respuesta;
            }

        }
        console.log(this.status)
    };
    xhr.send(params);
}

function AgregarProveedor() {
    var nombre = document.getElementById("NbrePro").value;
    var contacto = document.getElementById("cont").value;
    var telefono = document.getElementById("tel").value;
    var correo = document.getElementById("correo").value;
    var formdata = new FormData();
    formdata.append("nombre", nombre);
    formdata.append("contacto", contacto);
    formdata.append("telefono", telefono);
    formdata.append("correo", correo);
    var xhr = new XMLHttpRequest();
    var url = "/Proveedor/Agregar";
    xhr.open("POST", url, false);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            alerta(respuesta);
            //var producto = document.getElementById("invt").value;
            function alerta(confirm) {
                if (confirm == "true") {

                    Swal.fire(
                        'Proveedor Agregado!',
                        'se ha agregado el provedor a la base de datos',
                        'success'
                    )
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se ha podido agregar el proveedor',
                    })

                }
            }
            MostrarProveedor();
        }

    };
    xhr.send(formdata);
}
function EditarProveedor(a) {
    var producto = a;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'id': producto,
    });
    var url = "/Proveedor/Editar";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("IdProveedor").value = respuesta[0];
            document.getElementById("Nbrepro").value = respuesta[1];
            document.getElementById("contacto").value = respuesta[2];
            document.getElementById("tell").value = respuesta[3];
            document.getElementById("correoo").value = respuesta[4];
        }
        console.log(this.status)
    };
    xhr.send(params);
}
function ActualizarProveedor() {
    var id = document.getElementById("IdProveedor").value;
    var nombre = document.getElementById("Nbrepro").value;
    var contacto = document.getElementById("contacto").value; 
    var telefono = document.getElementById("tell").value; 
    var correo = document.getElementById("correoo").value;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'id': id,
        'nombre': nombre,
        'contacto': contacto,
        'telefono': telefono,
        'correo': correo,
    });
    var url = "/Proveedor/Actualizar";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            alerta(respuesta);
            function alerta(confirm) {
                if (confirm == "true") {

                    Swal.fire(
                        'Proveedor actualizado!',
                        'se ha actualizado el provedor correctamente',
                        'success'
                    )
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se ha podido actualizar el proveedor',
                    })

                }
            }
            
        }
        MostrarProveedor();
    };
    xhr.send(params);
}
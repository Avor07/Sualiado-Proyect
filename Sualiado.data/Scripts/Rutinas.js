let cantidad;
function Aumentar(input) {
    cantidad = document.getElementById(input).value;
    let numero = parseInt(cantidad);
    numero++;
    document.getElementById(input).value = numero;
    var x = document.getElementsByName("mio")[0];
    x.submit();
}
function Disminuir(input) {
    cantidad = document.getElementById(input).value;
    let numero = parseInt(cantidad);
    numero--;
    document.getElementById(input).value = numero;
    var x = document.getElementsByName("mio")[0];
    x.submit();
}
function Inicio() {
    var usuario=document.getElementById("usu").value;
    var contraseña=document.getElementById("pass").value;
    var xhr = new XMLHttpRequest();
    var dataObject = JSON.stringify({
        'usu': usuario,
        'pass': contraseña,
    });
    var params = dataObject;
    var url = "/Login/Login";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            if (respuesta == "TRUE") {
                location.href = "/Inicio/Inicio";
            } else {
                document.getElementById("mensage").innerHTML = respuesta;
                document.getElementById("mensage").style.color = "red";
            }
        }

    };
    xhr.send(params);
}
function CargarInicio() {
    var xhr = new XMLHttpRequest();
    var url = "/Inicio/Mostrar";
    xhr.open("POST", url, false);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
           
            document.getElementById("tareas").innerHTML = respuesta;
            
         
        }
    };
    xhr.send();
}

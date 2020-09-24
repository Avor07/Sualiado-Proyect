
    function DataTable() {
        $('#procam').DataTable({
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
    }

function AbrirReporte(evt, nbreReporte) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(nbreReporte).style.display = "block";
    evt.currentTarget.className += " active";
}
function MostrarGrafica(evt, nbreReporte) {
    var funcion = "hola";
    var xhr = new XMLHttpRequest();
    var url = "/Reportes/Reportes";
    xhr.open("POST", url, false);
    var dataObject = JSON.stringify({
        'funcion': funcion,
    });
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            var chartData = [];
            var chartData2 = [];
            for (var i = 0; i < respuesta[0].length; i++) {
                chartData.push ({

                    label: respuesta[0][i],
                    value: respuesta[1][i]
                })
            }
            for (var i = 0; i < respuesta[2].length; i++) {
                chartData2.push({

                    label: respuesta[2][i],
                    value: respuesta[3][i]
                })
            }
            
            const chartConfig = {
                type: 'line',
                renderAt: 'chart-container',
                width: '100%',
                height: '400',
                dataFormat: 'json',
                dataSource: {
                    // Chart Configuration
                    "chart": {
                        "exportEnabled":"1",
                        "numberPrefix": "$",
                        "formatNumberScale":"0",
                        "caption": "Las utilidades este año",
                        "subCaption": "COP",
                        "xAxisName": "Mes",
                        "yAxisName": "Utilidad",
                        "theme": "fusion",
                    },
                    // Chart Data
                    "data": chartData
                }
            };
            const chartConfig2 = {
                type: 'doughnut2d',
                renderAt: 'chart-container',
                width: '100%',
                height: '400',
                dataFormat: 'json',
                dataSource: {
                    // Chart Configuration
                    "chart": {
                        "exportEnabled": "1",
                        "formatNumberScale": "0",
                        "caption": "Los productos mas vendidos este año",
                        "subCaption": "",
                        "theme": "fusion",
                    },
                    // Chart Data
                    "data": chartData2
                }
            };
            AbrirReporte(evt, nbreReporte);
            FusionCharts.ready(function () {
                var fusioncharts = new FusionCharts(chartConfig);
                fusioncharts.render("ventas");
                var fusioncharts2 = new FusionCharts(chartConfig2);
                fusioncharts2.render("masVendido");
            });
          

        }
    };
    xhr.send(params);
}
function MostrarReporte(evt,nombreReporte) {
    AbrirReporte(evt,nombreReporte);
    var funcion = document.getElementById("rep").value;
    var xhr = new XMLHttpRequest();
    var url = "/Reportes/Productos";
    xhr.open("POST", url, false);
    var params = "{reporte: " + funcion + "}";
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            var temp;
            var pintar = "";
            var pintar2 = "";
            var pintar3 = "";
            for (var i = 0; i < respuesta[0].length; i++) {
                temp = "<li class='todo-list-item'><div class='checkbox'><label for='checkbox-1'>" + respuesta[0][i] + "</label></div></li>";
                pintar += temp;
            }
            
            for (var i = 0; i < respuesta[1].length; i++) {
                temp = "<li class='todo-list-item'><div class='checkbox'><label for='checkbox-1'>" + respuesta[1][i] + "-" + respuesta[2][i]+ "</label></div></li>";
                pintar2 += temp;

            }
            document.getElementById("agotados").innerHTML = pintar;
            document.getElementById("agotar").innerHTML = pintar2;
            if (respuesta[0].length = 0) {
                document.getElementById("agotados").innerHTML = "No hay productos agotados";
            }
            if (respuesta[1].length = 0) {
                document.getElementById("agotados").innerHTML = "No hay productos apunto de agotar";
            }
        }
    };
    xhr.send(params);
}
function MostrarCambios(evt, nbreReporte) {
    AbrirReporte(evt, nbreReporte);
    var dataObject = JSON.stringify({
        'funcion': "funcion",
    });
    var xhr = new XMLHttpRequest();
    var url = "/Reportes/Cambios";
    xhr.open("POST", url, false);
    var params = dataObject;
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var respuesta = JSON.parse(xhr.responseText);
            document.getElementById("preciosCambiados").innerHTML = respuesta;
            DataTable();
        }
    };
    xhr.send(params);
}

////var speedCanvas = document.getElementById("speedChart");
////var meses = document.getElementById("meses").value;
////var valor = new Array(parseInt(meses)); 
////var Mes = document.getElementsByName("mes");
////var Meses = new Array(parseInt(meses));
////var contador = 0;
////Mes.forEach((mes) => {
////    Meses[contador] = (mes.value)
////    contador++;

////});
////for (var i = 0; i < valor.length; i++) {
////    valor[i] = document.getElementById(i).value

////}
////console.log(Mes);

////Chart.defaults.global.defaultFontFamily = "Lato";
////Chart.defaults.global.defaultFontSize = 18;

////var dataFirst = {
////    label: "Ventas por mes",
////    data: valor,
////    borderColor: 'red',
////    backgroundColor:'red'

////};

////var Meses = {
////    labels: Meses,
////    datasets: [dataFirst]
////};

////var chartOptions = {
////    legend: {
////        display: true,
////        position: 'top',
////        labels: {
////            boxWidth: 80,
////            fontColor: 'black'
////        }
////    }
////};

////var lineChart = new Chart(speedCanvas, {
////    type: 'bar',
////    data: Meses,
////    options: chartOptions

////});
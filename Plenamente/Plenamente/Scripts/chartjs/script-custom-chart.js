var graphColors = [];
var graphOutlines = [];
var hoverColor = [];
function generarColores(internalDataLength) {
    for (var i = 0; i < internalDataLength; i++) {
        var R = Math.floor(Math.random() * 230);
        var G = Math.floor(Math.random() * 230);
        var B = Math.floor(Math.random() * 230);
        graphColors.push(`rgb(${R},${G},${B})`);
        graphOutlines.push(`rgb(${R - 80},${G - 80},${B - 80})`);
        hoverColor.push(`rgb(${R + 25},${G + 25},${B + 25})`);
    }
}
var canvasChart = document.getElementsByClassName("chart-container");
for (var i = 0; i < canvasChart.length; i++) {
    var id = canvasChart[i].getAttribute("id");
    var chartRoute = canvasChart[i].getAttribute("chart-route");
    var chartType = canvasChart[i].getAttribute("chart-type");
    Chart_Load(id, chartRoute, chartType);
}
function Chart_Load(nameObject, routeMethod, typeChart) {
    $.ajax({
        type: "POST",
        url: routeMethod,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            generarColores(chData.labels.length);
            var ctx = document.getElementById(nameObject).getContext('2d');
            new Chart(ctx, {
                type: typeChart ? typeChart : 'bar',
                data: {
                    labels: chData.labels,
                    datasets: [{
                        label: chData.label,
                        data: chData.data,
                        fill: chData.fill,
                        backgroundColor: chData.backgroundColor ? chData.backgroundColor : graphColors,
                        borderColor: chData.borderColor ? chData.borderColor : graphOutlines,
                        hoverBackgroundColor: hoverColor,
                        borderWidth: chData.borderWidth
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
        }
    });
}
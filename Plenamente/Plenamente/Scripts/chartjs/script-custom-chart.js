var graphColors = [];
var graphOutlines = [];
var hoverColor = [];
function generarColores(internalDataLength) {
    for (var i = 0; i < internalDataLength; i++) {
        var randomR = Math.floor((Math.random() * 130) + 100);
        var randomG = Math.floor((Math.random() * 130) + 100);
        var randomB = Math.floor((Math.random() * 130) + 100);

        var graphBackground = "rgb("
            + randomR + ", "
            + randomG + ", "
            + randomB + ")";
        graphColors.push(graphBackground);

        var graphOutline = "rgb("
            + (randomR - 80) + ", "
            + (randomG - 80) + ", "
            + (randomB - 80) + ")";
        graphOutlines.push(graphOutline);

        var hoverColors = "rgb("
            + (randomR + 25) + ", "
            + (randomG + 25) + ", "
            + (randomB + 25) + ")";
        hoverColor.push(hoverColors);
    }
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
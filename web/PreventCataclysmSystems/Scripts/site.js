google.charts.load('current', { 'packages': ['corechart', 'scatter', 'gauge'] });
google.charts.setOnLoadCallback(drawVisualization);

function drawVisualization() {
    drawClimateChart();
    drawAxisChart();
    drawChart();
}

function drawClimateChart() {
    var leituras = eval($('#leituras').val());

    var src = [['Leitura', 'Temperatura (°C)', 'Umidade Ar (%)', 'Umidade Solo (%)']];

    leituras.forEach(function (o, i) {
        src.push([convertDate(o.Leitura), o.Temperatura, o.Umidade, o.Solo])
    });

    var data = google.visualization.arrayToDataTable(src);

    var options = {
        title: 'Condições climáticas do ambiente explorado.',
        vAxis: { title: 'Variação' },
        hAxis: { title: 'Data' },
        seriesType: 'bars',
        series: { 0: { type: 'line' } },
        animation: {
            duration: 1500,
            easing: 'inAndOut',
            startup: true
        }
    };

    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}

function drawChart() {

    var json = $.parseJSON($('#aceleracao').val());

    var data = google.visualization.arrayToDataTable([
          ['Label', 'Value'],
          ['X', json.x],
          ['Y', json.y],
          ['Z', json.z]
    ]);

    var options = {
        chart: {
            title: 'Acelerômetro - Deslocamento.',
            subtitle: 'baseado nos eixos x, y e z'
        },
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById('gauge_div'));

    chart.draw(data, options);

    setInterval(function () {
        data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 13000);
    setInterval(function () {
        data.setValue(1, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 5000);
    setInterval(function () {
        data.setValue(2, 1, 60 + Math.round(20 * Math.random()));
        chart.draw(data, options);
    }, 26000);
}


function drawAxisChart() {
    var src = eval($('#axis').val());

    var chartDiv = document.getElementById('animatedshapes_div');

    var data = new google.visualization.DataTable();
    data.addColumn('number', 'Deslocamento em Profundidade');
    data.addColumn('number', 'Deslocamento Vertical');
    data.addColumn('number', 'Deslocamento Horizontal');

    var result = [];

    src.forEach(function (o, i) {
        result.push([o.z, o.y, o.x])
    });

    data.addRows(result);

    var materialOptions = {
        chart: {
            title: 'Deslocamento Axial em 3D',
            subtitle: 'baseado nos eixos x, y e z'
        },
        width: 1000,
        height: 500,
        series: {
            0: { axis: 'vertical' },
            1: { axis: 'horizontal' }
        },
        axes: {
            y: {
                'vertical': { label: 'Deslocamento Vertical (Y, Z)' },
                'horizontal': { label: 'Deslocamento Horizontal (X, Z)' }
            }
        }
    };

    function drawMaterialChart() {
        var materialChart = new google.charts.Scatter(chartDiv);
        materialChart.draw(data, google.charts.Scatter.convertOptions(materialOptions));
    }

    drawMaterialChart();

    /*var classicOptions = {
        width: 800,
        series: {
            0: { targetAxisIndex: 0 },
            1: { targetAxisIndex: 1 },
        },
        title: 'Deslocamento Axial',
        vAxes: {
            0: { title: 'Horizontal' },
            1: { title: 'Vertical' }
        },
        curveType: 'function',
        animation: {
            duration: 2000,
            easing: 'inAndOut',
            startup: true
        }
    };*/

    /*function drawClassicChart() {
        var classicChart = new google.visualization.ScatterChart(chartDiv);
        classicChart.draw(data, classicOptions);
    }*/

    //drawClassicChart();
}

function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
}


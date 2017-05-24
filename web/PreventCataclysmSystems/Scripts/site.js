google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawVisualization);

function drawVisualization() {

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
        series: { 5: { type: 'line' } }
    };

    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}

function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
}
google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawVisualization);

function drawVisualization(group) {    
    var data = google.visualization.arrayToDataTable([
     ['Time', 'Temperatura', 'Umidade Ar', 'Umidade Solo', 'Média'],
     ['2004/05', 165, 938, 522, 614.6],
     ['2005/06', 135, 1120, 599, 682],
     ['2006/07', 157, 1167, 587, 623],
     ['2007/08', 139, 1110, 615, 609.4],
     ['2008/09', 136, 691, 629, 569.6]
    ]);

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
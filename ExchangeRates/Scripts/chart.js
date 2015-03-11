$(function () {
    var dates = $('#dates').val();
    $('#container').highcharts({
        title: {
            text: 'Schedule of rates',
            x: -20
        },
        subtitle: {
            text: 'by date',
            x: -20
        },
        xAxis: {
            categories: JSON.parse('[' + dates + ']')
        },
        yAxis: {
            title: {
                text: 'Cost'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series: [{
            name: $('#money').val(),
            data: JSON.parse('[' + $('#values').val() + ']')
        }]
    });
});
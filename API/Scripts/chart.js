var ViewModel = function () {
    var self = this;
    self.stock = ko.observable();
    self.error = ko.observable();
    self.prediction = ko.observable();

    self.day1 = {
        Close: ko.observable(),
        Volume: ko.observable()
    }
    self.day2 = {
        Close: ko.observable(),
        Volume: ko.observable()
    }
    self.day3 = {
        Close: ko.observable(),
        Volume: ko.observable()
    }
    
    var id;
    var stocksUri = '/api/stocks/';
    var stockHistoriesUri = '/api/stockhistories/for/';
    var predictedStockHistoriesUri = '/api/predictedstockhistories/for/';
    var predictUri = '/api/PredictedStockHistories/MakePrediction/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }
 
    function getStock() {
        id = window.location.href.substring(window.location.href.lastIndexOf('/') + 1);
        ajaxHelper(stocksUri + id, 'GET').done(function (data) {
            self.stock = data;
            prepareHighstockData();
        });
    }

    //retrieve stock
    getStock();

    //draw highstock chart
    var seriesOptions = [],
        seriesCounter = 0,
        names = ['real', 'predicted'];

    function prepareHighstockData() {
        $.each(names, function (i, name) {
            var request;
            if (name == 'real') request = stockHistoriesUri;
            else if (name == 'predicted') request = predictedStockHistoriesUri;

            $.getJSON(request + self.stock.Id, function (data) {
                seriesOptions[i] = {
                    name: name,
                    data: data
                };

                // As we're loading the data asynchronously, we don't know what order it will arrive. So
                // we keep a counter and create the chart when all the data is loaded.
                seriesCounter += 1;

                if (seriesCounter === names.length) {
                    drawHighstock();
                }
            });
        });
    }

    function drawHighstock() {
        Highcharts.stockChart('chart', {

            rangeSelector: {
                selected: 4
            },

            yAxis: {
                labels: {
                    formatter: function () {
                        return this.value + '$';
                    }
                },
                plotLines: [{
                    value: 0,
                    width: 2,
                    color: 'silver'
                }]
            },

            plotOptions: {
                series: {
                    showInNavigator: true
                }
            },

            tooltip: {
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b><br/>',
                valueDecimals: 2,
                split: true
            },

            series: seriesOptions
        });
    }

    self.makePrediction = function (formElement) {
        var data = {
            day1: self.day1,
            day2: self.day2,
            day3: self.day3,
            id: self.stock.Id
        };
        self.error('');
        $.ajax({
            type: "POST",
            url: predictUri,
            data: data,
            success: null,
            dataType: null
        }).done(function (item) {
            var result = 'Tomorrow Close price will be ' + item;
            self.prediction(result);
        }).fail(function (xhr, status, error) {
            var message = xhr + ' ' + status + ' ' + error;
            self.error(message);
        });
    }
};

ko.applyBindings(new ViewModel());

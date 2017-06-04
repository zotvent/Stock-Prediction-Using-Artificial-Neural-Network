var ViewModel = function () {
    var self = this;
    self.stocks = ko.observableArray();
    self.error = ko.observable();

    var stocksUri = '/api/stocks/';
    var stockHistoriesUri = '/api/stockhistories/';
    var chartUri = '/Home/Chart/';

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

    function getAllStocks() {
        ajaxHelper(stocksUri, 'GET').done(function (data) {
            self.stocks(data);
        });
    }

    // Fetch the initial data.
    getAllStocks();

    self.getStockChart = function (item) {
        window.location = chartUri + item.Id;
    }
};

ko.applyBindings(new ViewModel());

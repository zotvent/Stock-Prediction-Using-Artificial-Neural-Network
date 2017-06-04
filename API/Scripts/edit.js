var ViewModel = function () {
    var self = this;
    self.stocks = ko.observableArray();

    var stocksUri = '/api/stocks/';

    function ajaxHelper(uri, method, data) {
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

    self.deleteStock = function (item) {
        ajaxHelper(stocksUri + item.Id, 'DELETE').done(function (data) {
            getAllStocks();
        });
    }
};

ko.applyBindings(new ViewModel());

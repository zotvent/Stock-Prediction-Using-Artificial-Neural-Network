var ViewModel = function () {
    var self = this;
    self.error = ko.observable();

    var stocksUri = '/api/stocks/';
    var stockHistoriesUri = '/api/stockhistories/';
    var uploadUri = '/Home/HandleCSVForm/';
    var editUri = '/Home/Edit';

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

    self.newStock = {
        Name: ko.observable(),
        Ticker: ko.observable()
    }

    self.addStock = function (formElement) {
        var stock = {
            Name: self.newStock.Name(),
            Ticker: self.newStock.Ticker()
        };
        var files = $('#inputHistory')[0].files;

        if (stock.Name != '' && stock.Ticker != '' && files.length > 0) {
            ajaxHelper(stocksUri, 'POST', stock).done(function (item) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < files.length; x++) {
                        data.append("file" + x, files[x]);
                    }
                    $.ajax({
                        type: "POST",
                        url: uploadUri + item.Id,
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            console.log(result);
                            window.location = editUri;
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(err);
                            self.error = err;
                        }
                    });
                } else {
                    alert("This browser doesn't support HTML5 file uploads!");
                }
            });
        }
        else {
            self.error('Please, fill all fields');
            return false;
        }
    }
};

ko.applyBindings(new ViewModel());

# Stock Prediction Using Artificial Neural Network

## Used Technologies

### NuGet Packages

- [encog-dotnet-core](http://www.heatonresearch.com/encog/)
- [Bootstrap CSS](http://getbootstrap.com)
- [jQuery](https://jquery.com)
- [knockoutjs](http://knockoutjs.com)

### JavaScript (not available in NuGet)

- [Highstock](https://www.highcharts.com/products/highstock)

## How Neural Network works

It takes 3 consecutive days in CSV file. In each day volume and close price were choosen. Then it predicts close price for the 4th day.

## Usage

1. Install NuGet packages mentioned above.
2. Create database from NNModel.edmx.sql script. If you apply script to existing database make sure that your database is called **_NeuronNetworkDB_**.
3. In API project in Web.config in connectionStrings sector change path to your database.
4. Log in as administrator and create a new neural network for some company.
5. Choose created company at home page.
6. Now you can see the results of trained neural network on chart.
7. Below the chart you can enter volume and price for 3 consecutive days and make a prediction for the 4th day.

## How to log in as administrator

    login: admin
    password: Qwerty123

## CSV

[Yahoo Finance](https://finance.yahoo.com) was used for getting stock history prices over some period of time. You can find examples of CSV file that were used for testing during the development in examplesCSV folder.

## Credits

[Nadil Karimov](https://github.com/nadilk) neural network architecture and implementation

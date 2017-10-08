var app = (function (window, undefined) {

    var vm;

    var countriesDropDown;
    var citiesDropDown;
    var citiesText;
    var weatherServicesDropDown;
    var goButton;

    var resultContainer;
    var resultCityText;
    var resultCountryText;
    var resultCurrentTempText;
    var resultDescPrimaryText;
    var resultDecSecondaryText;
    var resultErrorContainer;
    var resultErrorText;
    var detailedTable;

    var currentCountry;
    var currentCity;

    function init(viewModel) {

        vm = viewModel;

        console.log(vm);

        countriesDropDown = $("#countriesDropDown");
        citiesDropDown = $("#citiesDropDown");
        citiesText = $("#citiesText");
        weatherServicesDropDown = $("#weatherServicesDropDown");
        goButton = $("#goButton");

        resultContainer = $("#resultContainer");
        resultCityText = $("#resultCityText");
        resultCountryText = $("#resultCountryText");
        resultCurrentTempText = $("#resultCurrentTempText");
        resultDescPrimaryText = $("#resultDescPrimaryText");
        resultDecSecondaryText = $("#resultDecSecondaryText");

        detailedTable = $("#detailedTable");

        resultErrorContainer = $("#resultErrorContainer");
        resultErrorText = $("#resultErrorText");

        setupCountriesDropDown();
        setupCitiesDropDown();
        setupWeatherServicesDropDown();
        setupGoButton();
    }

    function setupCountriesDropDown() {

        //No countries

        if (vm.Countries.length === 0) {
            disableDropDown(countriesDropDown, "No countries available");
            return;
        }

        //Countries available

        countriesDropDown.empty();

        var selectMsg = "Select...";

        countriesDropDown.append($("<option>", { value: selectMsg, html: selectMsg }));

        $(vm.Countries).each(function (i, v) {
            countriesDropDown.append($("<option>", { value: v.Name, html: v.Name }));
        });

        countriesDropDown.removeAttr("disabled");

        countriesDropDown.change(function () {
            loadCities();
            goButtonEnabledLogic();
        });

    }

    function setupCitiesDropDown() {
        citiesDropDown.change(function () {
            goButtonEnabledLogic();
        });
    }

    function setupWeatherServicesDropDown() {

        //No weather services

        if (vm.WeatherServices.length === 0) {
            disableDropDown(weatherServicesDropDown, "No weather services available");
            return;
        }

        //Weather services available

        weatherServicesDropDown.empty();

        $(vm.WeatherServices).each(function (i, v) {
            weatherServicesDropDown.append($("<option>", { value: v.Name, html: v.Name }));
        });

        weatherServicesDropDown.val("Reliable");
        weatherServicesDropDown.removeAttr("disabled");

        weatherServicesDropDown.change(function () {
            goButtonEnabledLogic();
        });

    }

    function setLoading(loading) {

        if (loading) {
            $("#spinner").removeClass("hidden");
        } else {
            $("#spinner").addClass("hidden");
        }
    }

    function setupGoButton() {
        goButton.click(function () {

            goButton.addClass("disabled");

            loadWeather(function () {
                goButton.removeClass("disabled");

            });
        });
    }

    function selectedCountry() {
        return dropDownSelectedItem(countriesDropDown, vm.Countries, -1);
    }

    function selectedCity() {
        return citiesDropDown.val();
    }

    function selectedWeatherService() {
        return dropDownSelectedItem(weatherServicesDropDown, vm.WeatherServices, 0);
    }

    function loadCities() {

        citiesDropDown.empty();
        citiesDropDown.attr("disabled", "disabled");

        //Country not selected
        if (!dropDownHasSelection(countriesDropDown, 1)) {
            citiesDropDown.hide();
            citiesText.text("Please select a country first");
            return;
        }

        //Country selected...

        setLoading(true);

        citiesText.text("Loading...");

        //Minus 1 for the select msg at index 0
        var idx = countriesDropDown.prop("selectedIndex") - 1;
        var url = vm.Countries[idx].CitiesLink;

        get(url,
            function (data) {
                //Success
                if (data && data.length > 0) {

                    var selectMsg = "Select...";

                    citiesDropDown.append($("<option>", { value: selectMsg, html: selectMsg }));

                    $(data).each(function (i, v) {
                        citiesDropDown.append($("<option>", { value: v, html: v }));
                    });

                    citiesText.text("Select a city");
                    citiesDropDown.removeAttr("disabled");
                    citiesDropDown.show();
                } else {
                    citiesDropDown.hide();
                    citiesText.text("No cities were found for the selected country");
                }
                setLoading(false);
            },
            function (data, status) {
                //Failure
                citiesText.text("An error has occurred. Please try again.");
                setLoading(false);
            });
    }

    function loadWeather(callback) {

        var weatherService = selectedWeatherService();

        currentCountry = selectedCountry();
        currentCity = selectedCity();

        var url = weatherService.WeatherLink + "?";
        var args = { country: currentCountry.Name, city: currentCity };

        url = url + $.param(args);

        clearWeatherResult();

        setLoading(true);

        get(url,
            function (data) {
                displayWeatherResult(data);
                setLoading(false);
                callback();
            },
            function (data, status) {
                displayWeatherError(data);
                setLoading(false);
                callback();
            });
    }

    function clearWeatherResult() {

        resultContainer.hide();

        resultCurrentTempText.text("");
        resultDescPrimaryText.text("");
        resultDecSecondaryText.text("");
        resultCountryText.text("");
        resultCityText.text("");

        detailedTable.empty();

        resultErrorContainer.hide();
        resultErrorText.text("");

    }

    function displayWeatherResult(data) {

        resultCountryText.text(currentCountry.Name);
        resultCityText.text(currentCity);

        resultDescPrimaryText.text(data.MainDesc);
        resultDecSecondaryText.text(data.SecondaryDesc);
        resultCurrentTempText.text(data.CurrentTemp);

        $.map(data.Props, function (v, k) {
            detailedTable.append("<tr><td class=\"col-md-1\">" + k + "</td><td class=\"col-md-3\">" + v + "</td></tr>");
        });

        resultContainer.show();

    }

    function displayWeatherError(data) {

        resultErrorText.text(data.responseText);
        resultErrorContainer.show();
    }

    function goButtonEnabledLogic() {

        var enabled =
            dropDownHasSelection(countriesDropDown, 1) &&
            dropDownHasSelection(citiesDropDown, 1) &&
            dropDownHasSelection(weatherServicesDropDown, 0);

        if (enabled) {
            goButton.removeClass("disabled");
        } else {
            goButton.addClass("disabled");
        }

        return true;

    }

    function disableDropDown(dropDown, message) {
        dropDown.empty();
        dropDown.append($("<option>", { value: message, html: message }));
        dropDown.attr("disabled", "disabled");
    }

    function dropDownHasSelection(dropDown, minIndex) {
        var idx = dropDown.prop("selectedIndex");
        return idx >= minIndex;
    }

    function dropDownSelectedItem(dropDown, dataSource, indexOffset) {
        var idx = dropDown.prop("selectedIndex") + indexOffset;
        return dataSource[idx];
    }

    function get(url, successCallback, failCallback) {
        $.ajax(url,
            {
                method: 'GET',
                contentType: 'text/json',
                beforeSend: function (xmlHttpRequest) {
                    xmlHttpRequest.withCredentials = true;
                }
            }).then(
            function success(data) {
                console.log(data);
                successCallback(data);
            },
            function fail(data, status) {
                failCallback(data, status);
            });
    }

    return {
        init: init
    }

})(window);
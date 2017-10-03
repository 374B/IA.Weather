var app = (function (window, undefined) {

    var vm;

    var countriesDropDown;
    var citiesDropDown;
    var weatherServicesDropDown;

    function init(viewModel) {

        vm = viewModel;

        console.log(vm);

        countriesDropDown = $("#countriesDropDown");
        citiesDropDown = $("#citiesDropDown");
        weatherServicesDropDown = $("#weatherServicesDropDown");

        setupCountriesDropDown();
        setupWeatherServicesDropDown();
    }

    function setLoading(loading) {

        if (loading) {
            $("#spinner").removeClass("hidden");
        } else {
            $("#spinner").addClass("hidden");
        }
    }

    function setupCountriesDropDown() {

        //No countries

        if (vm.Countries.length === 0) {
            disableDropDown(countriesDropDown, "No countries available");
            return;
        }

        //Countries available

        countriesDropDown.empty();

        var selectMsg = "Select a country...";

        countriesDropDown.append($("<option>", { value: selectMsg, html: selectMsg }));

        $(vm.Countries).each(function (i, v) {
            countriesDropDown.append($("<option>", { value: v.Name, html: v.Name }));
        });

        countriesDropDown.removeAttr("disabled");

        countriesDropDown.change(function () {
            loadCities();
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

        weatherServicesDropDown.removeAttr("disabled");

    }

    function loadCities() {

        //Minus 1 for the "select..." message
        var idx = countriesDropDown.prop("selectedIndex") - 1;

        //Country not selected
        if (countriesDropDown.prop("selectedIndex") < 0) {
            $("#cityNoneFound").hide();
            $("#citySelectCountry").hide();
            $("#citySelectCity").show();
            return;
        }

        //Country selected...

        setLoading(true);

        $("#cityNoneFound").hide();
        $("#citySelectCountry").hide();
        $("#citySelectCity").hide();

        var url = vm.Countries[idx].CitiesLink;

        $.ajax(url, {
            method: 'GET',
            contentType: 'text/json',
            beforeSend: function (xmlHttpRequest) {
                xmlHttpRequest.withCredentials = true;
            }
        }).then(
            function success(data) {

                console.log(data);

                if (data && data.length > 0) {

                    citiesDropDown.empty();

                    var selectMsg = "Select a city...";

                    countriesDropDown.append($("<option>", { value: selectMsg, html: selectMsg }));

                    $(data).each(function (i, v) {
                        citiesDropDown.append($("<option>", { value: v, html: v }));
                    });

                    $("#citySelectCity").show();
                } else {
                    $("#cityNoneFound").show();
                }
                setLoading(false);
            },
            function fail(data, status) {
                //TODO: Should really be an error message
                $("#citySelectCity").hide();
                setLoading(false);
            });
    }

    function disableDropDown(dropDown, message) {
        dropDown.empty();
        dropDown.append($("<option>", { value: message, html: message }));
        dropDown.attr("disabled", "disabled");
    }

    return {
        init: init
    }

})(window);
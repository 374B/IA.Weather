var app = (function (window, undefined) {

    var vm;

    var countriesDropDown;
    var weatherServicesDropDown;

    function init(viewModel) {

        vm = viewModel;

        console.log(vm);

        countriesDropDown = $("#countriesDropDown");
        weatherServicesDropDown = $("#weatherServicesDropDown");

        setupCountriesDropDown();
        setupWeatherServicesDropDown();

        setUiState();

    }

    function setLoading(loading) {

        if (loading) {
            $("#spinner").removeClass("hidden");
        } else {
            $("#spinner").addClass("hidden");
        }
    }

    function setUiState() {

        //if (countriesDropDown.length > 0) {
        //    if (countriesDropDown.prop("selectedIndex") > 0) {
        //        //Country selected
        //        $("#cityNoneFound").hide();
        //        $("#citySelectCountry").hide();
        //        $("#citySelectCity").show();

        //    } else {
        //        //Index 0 selected, not a country
        //        $("#cityNoneFound").hide();
        //        $("#citySelectCity").hide();
        //        $("#citySelectCountry").show();
        //    }
        //} else {
        //    //No countries
        //    $("#citySelectCity").hide();
        //    $("#citySelectCountry").hide();
        //    $("#cityNoneFound").show();
        //}
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

        $("#citySelectCity").hide();

        var url = vm.Countries[idx].CitiesLink;

        $.getJSON(url, null, function () {
            setLoading(true);
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
var app = (function (window, undefined) {

    var countriesLoaded = false;
    var providersLoaded = false;

    var countries = [];
    var providers = [];

    var countriesDropDown;
    var providersDropDown;

    function init() {

        countriesDropDown = $("#countriesDropDown");
        providersDropDown = $("#providersDropDown");

        setUiState();

        var toLoad = 2;

        var onLoad = function () {
            toLoad--;

            if (toLoad === 0) {

                setLoading(false);

                if (providersLoaded && countriesLoaded) {
                    setUiState();
                    setLoading(false);
                } else {
                    $("#container").hide();
                    $("#error").show();
                }
            }
        }

        setLoading(true);

        loadCountriesList(onLoad);
        loadWeatherProviders(onLoad);

    }

    function setLoading(loading) {

        if (loading) {
            $("#spinner").removeClass("hidden");
        } else {
            $("#spinner").addClass("hidden");
        }
    }

    function setUiState() {

        setupCountriesDropDown();
        setupProvidersDropDown();

        if (countriesLoaded) {

            if (countriesDropDown.length > 0) {
                if (countriesDropDown.prop("selectedIndex") > 0) {
                    //Country selected
                    $("#cityNoneFound").hide();
                    $("#citySelectCountry").hide();
                    $("#citySelectCity").show();

                } else {
                    //Index 0 selected, not a country
                    $("#cityNoneFound").hide();
                    $("#citySelectCity").hide();
                    $("#citySelectCountry").show();
                }
            } else {
                //No countries
                $("#citySelectCity").hide();
                $("#citySelectCountry").hide();
                $("#cityNoneFound").show();
            }
        } else {
            $("#citySelectCity").hide();
            $("#citySelectCountry").hide();
            $("#cityNoneFound").hide();
        }
    }

    function setupCountriesDropDown() {

        //No countries

        if (countries.length === 0) {
            disableDropDown(countriesDropDown, "No countries available");
            return;
        }

        //Countries loaded

        countriesDropDown.empty();

        $(countries).each(function (i, v) {
            countriesDropDown.append($("<option>", { value: v, html: v }));
        });

        countriesDropDown.removeAttr("disabled");

        countriesDropDown.change(function () {
            $("#citiesSpinner").removeClass("hidden");
        });

    }

    function setupProvidersDropDown() {

        //No providers

        if (providers.length === 0) {
            disableDropDown(providersDropDown, "No provides available");
            return;
        }

        //Providers loaded

        providersDropDown.empty();

        $(providers).each(function (i, v) {
            providersDropDown.append($("<option>", { value: v, html: v }));
        });

        providersDropDown.removeAttr("disabled");

    }

    function loadCountriesList(callback) {

        disableDropDown(countriesDropDown, "Loading countries...");

        countries = ["Select a country...", "Australia", "France", "England", "Spain"];

        countriesLoaded = true;

        callback();
    }

    function loadWeatherProviders(callback) {

        disableDropDown(providersDropDown, "Loading providers...");

        setTimeout(function () {
            providers = ["A", "B"];
            providersLoaded = true;
            callback();
        }, 2000);
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
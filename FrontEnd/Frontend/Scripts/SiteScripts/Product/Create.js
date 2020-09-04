$(document).ready(function () {
    $('#create-product-form').validate();

    $(".money-mask").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '', decimal: ',', affixesStay: false });
});   
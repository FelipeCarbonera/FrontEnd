$(document).ready(function () {
    $('#edit-product-form').validate();

    $(".money-mask").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '', decimal: ',', affixesStay: false });
});   
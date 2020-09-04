var qtdproductslist = 0;

$('.checkbox-product-cart').change(function () {
    var div = this.parentElement;
    var productName = $(div).children('.product-name');
    var productPrice = $(div).children('.product-price');
    var productCategory = $(div).children('.product-category');
    var idProduct = $(productName).data('idproduto');
    
    productName = productName.text();
    
    productPrice = $(productPrice).data('productprice');
    productCategory = $(productCategory).data('productcategory');
    var id = idProduct + productName;
    id = id.replace(/\s+/g, '');

    if (this.checked) {
        qtdproductslist++;
        $('#product-list').append(
                                    '<tr class="' + id + '">'+
                                    '<td>' + qtdproductslist + '</td>'+
                                    '<td>' + productName + '</td>'+
                                    '<td>' + productCategory + '</td>'+
                                    '<td class="table-product-list-price">' + productPrice + '</td>'+
                                    '</tr>'
                                  );
    }
    else {
        qtdproductslist--;
        $('.' + id).remove();
    }

    var totalproductsprice = 0;
    $('.table-product-list-price').each(function () {
        totalproductsprice += parseFloat($(this).html().replace(',','.'));
    });

    $('#total-price-element').html('Total: R$ ' + totalproductsprice.toString().replace('.', ','));

    var qtdFilmes = $('tbody#product-list tr').length;
    $('#qtd-products').text(qtdFilmes);
});
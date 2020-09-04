$(document).ready(function () {

    $('input[name="ids-product-delete"').on('click', function () {

        if ($('input[name="ids-product-delete"]:checked').length <= 0) {
            $('#delete-products-btn').prop('disabled', true);
        }
        else {
            $('#delete-products-btn').prop('disabled', false);
        }
    });
    
    $('#edit-product-modal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)
        var categoryId = button.data('productid')

        $.ajax({
            type: 'POST',
            url: updateUrlModal + '/' + categoryId,
            success: function (data) {
                $('#update-modal-body-div').html(data);
            }
        });
    })
    
})
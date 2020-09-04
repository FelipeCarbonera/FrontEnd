$(document).ready(function () {

    $('input[name="ids-category-delete"').on('click', function () {

        if ($('input[name="ids-category-delete"]:checked').length <= 0) {
            $('#delete-categories-btn').prop('disabled', true);
        }
        else {
            $('#delete-categories-btn').prop('disabled', false);
        }
    });


    $('#edit-category-modal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)
        var categoryId = button.data('categoryid')

        $.ajax({
            type: 'POST',
            url: updateUrlModal + '/' + categoryId,
            success: function (data) {
                $('#update-modal-body-div').html(data);
            }
        });
    })

})




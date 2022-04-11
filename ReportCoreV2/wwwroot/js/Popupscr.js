var My2jquery = jQuery.noConflict()
showInPopup = (url, title) => {
    My2jquery.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            My2jquery('#form-modal .modal-body').html(res);
            My2jquery('#form-modal .modal-title').html(title);
            My2jquery('#form-modal').modal('show');
            // to make popup draggable
            // My2jquery('.modal-dialog').draggable({
            //     handle: ".modal-header"
            // });
        }
    })
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    
$(document).ready(function () {
        $.noConflict();
    $('#myTable').DataTable({
        language: {
            search: '<span class="icon-search2"></span>',
            searchPlaceholder: "Search..."
        },
            "scrollY": "600px",
            "scrollCollapse": true,
            "paging": true,
            "lengthMenu": [[15, 30, 50, -1],[15, 30, 50, "All"]],
            "pageLength": 15
            
             
        });
});

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            //$('.modal-dialog').draggable({
            //    handle: ".modal-header"
            //});
        }
    })
}

SaveDataPost = form => {
    try {
        
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#Index').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}



 


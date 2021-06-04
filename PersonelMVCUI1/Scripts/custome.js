
function CheckDateTypeIsValid(dataElement) {

    var value = $(dataElement).val();
    if (value == '') {
        $(dataElement).valid("false");
    }

    else {
        $(dataElement).valid();
    }
}


$(function () {

    $("#tblDepartmanlar").dataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });

    //$("#tblPersoneller").dataTable({
    //    "language": {
    //        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
    //    }
    //});

    $("#tblDepartmanlar").on("click", ".btnDepartmanSil", function () {

        var btn = $(this);    
        bootbox.confirm("Departmanı silmek istediğinizden emin misiniz ?", function (result) {

            if (result) {
                var id = btn.data("id");
                            
                $.ajax({
                    type: "GET",
                    url: "/Departman/Sil/" + id,
                    success: function () {
                        btn.parent().parent().remove();
                    }
                });
            }

        })
    });
});

//$(function () {



//    $("#tblPersoneller").on("click", ".btnPersonelSil", function () {

//        var btn = $(this);
//        bootbox.confirm("Personeli silmek istediğinizden emin misiniz ?", function (result) {

//            if (result) {
//                var id = btn.data("id");

//                $.ajax({
//                    type: "GET",
//                    url: "/Personel/Sil/" + id,
//                    success: function () {
//                        btn.parent().parent().remove();
//                    }
//                });
//            }

//        })
//    });
//});






$(document).ready(function () {


    /*
     اعتبارسنجی فرم ایجاد محتوای آزاد
     */
    $("form[name='reg_free_content']").validate({
        debug: true,
        onkeyup: true,
        errorClass: "has-error has-feedback",
        validClass: "has-success has-feedback",

        rules: {

            Title: "required",
            TitleEn: "required",
            Type: "required"

        },
        messages: {
            Title: 'لطفا  عنوان فارسی محتوای آزاد را وارد فرمایید.',
            TitleEn: "لطفا  عنوان انگلیسی محتوای آزاد را وارد فرمایید.",
            Type: "لطفا  نوع محتوای آزاد را وارد فرمایید."
        }


    });


    /*
     اعتبارسنجی فرم ویرایش محتوای آزاد
     */

    $("form[name='edit_free_content']").validate({
        debug: true,
        onkeyup: true,
        errorClass: "has-error has-feedback",
        validClass: "has-success has-feedback",

        rules: {

            Title: "required",
            TitleEn: "required",
            Type: "required"

        },
        messages: {
            Title: 'لطفا  عنوان فارسی محتوای آزاد را وارد فرمایید.',
            TitleEn: "لطفا  عنوان انگلیسی محتوای آزاد را وارد فرمایید.",
            Type: "لطفا  نوع محتوای آزاد را وارد فرمایید."
        },


    });
    


    $('#clear-reg-free-content').click(function () {

        ClearForm_input("form[name='reg_free_content']");
        $('.close').click();
    });
});

require(['JsonAction'], function (JsonAction) {
    var Get_Module;

    Get_Module = JsonAction;
    var free_content = new Get_Module("#free-content-grid", "#rest-free-content", "محتوای آزاد", free_content_data, free_content_Edit, free_content_delete, free_content_multidelete, free_content_create);

    JsonAction.prototype.TableConfig = function () {
        var parentThis = this;

        $(parentThis.TableName).bootgrid({
            ajax: true,
            url: parentThis.SetDataUrl,
            ajaxSettings: {
                method: "GET",
                rowCount: 5000,
                cache: false,
                headers: {
                    'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content'),
                }
            },
            formatters: {

                "btn_free_content": function (column, row) {
                    parentThis.GetRows.push(row);
                    //console.log(row["Id"])
                    return "<a  href=\"#\"   data-toggle=\"modal\"   data-target=\"#myEditModal\"  class=\"edit-free-content \" data-id=\"" + row["Id"] + "\"><span class=\"fa fa-pencil\"></span></button> <a class=\"delete-free-content\" data-id=\"" + row["Id"] + "\"><span class=\"fa fa-trash-o\"></span></button> ";
                    //return "<a data-toggle=\"modal\"   data-target=\"#myEditModal\" class=\"command-edit\" data-row-id=\"" + row["Id"] + "\"><span class=\"fa fa-pencil\"></span></button> ";
                },


            },
            requestHandler: function (request) {
                // Scan the original sort object from Bootgrid...
                // and populate an array of "SortData"...
                request.sortItems = {};

                if (request.sort == null)
                    return request;
                for (var property in request.sort) {
                    if (request.sort.hasOwnProperty(property)) {
                        //request.sortItems.push({ "Field": property, "Type": request.sort[property] });

                        request.sortItems["Field"] = property;
                        request.sortItems["Type"] = request.sort[property];

                    }
                }
                //console.log(request.sortItems);
                return request;
            },

            selection: true,
            multiSelect: true,
            keepSelection: true,
            sorting: true,
            sortable: true
        }).on("selected.rs.jquery.bootgrid", function (e, rows) {
            for (var i = 0; i < rows.length; i++) {
                parentThis.rowIds.push(rows[i].Id);
            }
        }).on("deselected.rs.jquery.bootgrid", function (e, rows) {
            for (var i = 0; i < rows.length; i++) {
                $.each(parentThis.rowIds, function (ix) {
                    if (parentThis.rowIds[ix] == rows[i].Id) {
                        parentThis.rowIds.splice(ix, 1);
                        return false;
                    }
                });
            }

        }).on("loaded.rs.jquery.bootgrid", function (e, rows) {

            $('[data-toggle="tooltip"]').tooltip();

            $(parentThis.TableName).find(".edit-free-content").on("click", function (e, row) {
                e.preventDefault();
                $('.Edit-Modal').click();

                parentThis.getID = $(this).attr('data-id');


                //$("form[name='edit_free_content']").ClearForm_input();
                //if ($('#edit-Condition').is(':checked')) {
                //    $("#edit-Condition").click();
                //    $('#edit-Condition').val('0');
                //} 
                for (var c = 0; c < parentThis.GetRows.length; c++) {

                    if (parentThis.GetRows[c]['Id'] == parentThis.getID) {
                        var Title = parentThis.GetRows[c]['Title'];
                        var TitleEn = parentThis.GetRows[c]['TitleEn'];
                        var Type = parentThis.GetRows[c]['Type'];
                        var Priority = parentThis.GetRows[c]['Priority'];
                        var Condition = parentThis.GetRows[c]['Condition'];

                        
                        $("#edit-Title").val(Title);
                        $("#edit-TitleEn").val(TitleEn);
                        $("#edit-Type").val(Type);
                        $("#edit-Priority").val(Priority);

                        //if (Condition == 1) {
                        //    $("#edit-Condition").click();
                        //}



                    }

                }
               

            });
            $(parentThis.TableName).find(".delete-free-content").on("click", function (e) {

                e.preventDefault();
                var id = $(this).attr('data-id');
                free_content.Delete(id)

            });
            
        });


    };

    free_content.TableConfig();

    $("#free-content-update").click(function () {
        //if ($("form[name='edit_free_content']").valid()) {

            free_content.SetDataUpdate("#edit-Title", "Title", false);
            free_content.SetDataUpdate("#edit-TitleEn", "TitleEn", false);
            free_content.SetDataUpdate("#edit-Type", "Type", false);

            if ($('#edit-Priority').val() != "") {
                free_content.SetDataUpdate("#edit-Priority", "Priority", false);
            }

            //free_content.SetDataUpdate("#edit-Condition", "Condition", false);

            free_content.PostUpdate();

        //}

    });

    $("#free-content-create").click(function () {

        if ($("form[name='reg_free_content']").valid()) {
            free_content.SetDataCreate("#Title", "Title", false);
            free_content.SetDataCreate("#TitleEn", "TitleEn", false);
            free_content.SetDataCreate("#Type", "Type", false);

            if ($('#Priority').val() != "") {
                free_content.SetDataCreate("#Priority", "Priority", false);
            }

            free_content.SetDataCreate("#Condition", "Condition", false);

            free_content.Create();
        }

    });

    $('#Condition').click(function () {
        if ($('#Condition').is(':checked')) {
            $('#Condition').val('1');
        } else {
            $('#Condition').val('0');
        }

    });

    $('#edit-Condition').click(function () {
        if ($('#edit-Condition').is(':checked')) {
            $('#edit-Condition').val('1');
        } else {
            $('#edit-Condition').val('0');
        }

    });

    $("#free-content-MultiDelete").click(function (e) {
        e.preventDefault();
        free_content.MultiDelete();
    });


});

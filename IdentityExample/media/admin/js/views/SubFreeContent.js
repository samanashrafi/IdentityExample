
$(document).ready(function () {

    /*
     --------------------
     -------------------- create Free Content
     --------------------
     */


    /*
     اعتبارسنجی فرم ایجاد آیتم محتوای 
     */
    //$("form[name='reg_SubFreeContent']").validate({
    //    debug: true,
    //    onkeyup: true,
    //    errorClass: "has-error has-feedback",
    //    validClass: "has-success has-feedback",

    //    rules: {

    //        Title: "required",
    //        EnTitle: "required",

    //    },
    //    messages: {
    //        Title: 'لطفا  عنوان فارسی محتوای آزاد را وارد فرمایید.',
    //        EnTitle: "لطفا  عنوان انگلیسی محتوای آزاد را وارد فرمایید.",
    //    }


    //});

    $("form[name='edit_SubFreeContent']").validate({
        debug: true,
        onkeyup: true,
        errorClass: "has-error has-feedback",
        validClass: "has-success has-feedback",

        rules: {

            Title: "required",
            EnTitle: "required",

        },
        messages: {
            Title: 'لطفا  عنوان فارسی محتوای آزاد را وارد فرمایید.',
            EnTitle: "لطفا  عنوان انگلیسی محتوای آزاد را وارد فرمایید.",
        },


    });


    /*
    --------------------
    -------------------- Free Content
    --------------------
     */


    /*
     اعتبارسنجی فرم ویرایش آیتم محتوا
     */




    $('#clear-reg-SubFreeContent').click(function () {

        ClearForm_input("form[name='reg_SubFreeContent']");
        $('.close').click();
    });




});


require(['JsonAction'], function (JsonAction) {
    var Get_Module;
    var update_Images = [];
    Get_Module = JsonAction;
    var SubFreeContent = new Get_Module("#SubFreeContent-grid", "#rest-SubFreeContent", "محتوای آزاد", SubFreeContent_data, SubFreeContent_Edit, SubFreeContent_delete, SubFreeContent_multidelete, SubFreeContent_create);

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

                "btn_SubFreeContent": function (column, row) {
                    parentThis.GetRows.push(row);
                    //console.log(row["Id"])
                    return "<a  href=\"#\"   data-toggle=\"modal\"   data-target=\"#myEditModal\"  class=\"edit-SubFreeContent \" data-id=\"" + row["Id"] + "\"><span class=\"fa fa-pencil\"></span></button> <a class=\"delete-SubFreeContent\" data-id=\"" + row["Id"] + "\"><span class=\"fa fa-trash-o\"></span></button> ";
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


            img_img = 0;


            $(parentThis.TableName).find(".edit-SubFreeContent").on("click", function (e, row) {
                e.preventDefault();
                $('.Edit-Modal').click();
                parentThis.getID = $(this).attr('data-id');

                //                alert('edit is click.');
                //$("form[name='edit_SubFreeContent']").ClearForm_input();
                //if ($('#edit-Condition').is(':checked')) {
                //    $("#edit-Condition").click();
                //    $('#edit-Condition').val('0');
                //}
                for (var c = 0; c < parentThis.GetRows.length; c++) {

                    if (parentThis.GetRows[c]['Id'] == parentThis.getID) {
                        var Title = parentThis.GetRows[c]['Title'];
                        var EnTitle = parentThis.GetRows[c]['EnTitle'];
                        var Type = parentThis.GetRows[c]['Type'];
                        var Priority = parentThis.GetRows[c]['Priority'];
                        var Condition = parentThis.GetRows[c]['Condition'];
                        var PageTitle = parentThis.GetRows[c]['PageTitle'];
                        var MetaKeyword = parentThis.GetRows[c]['MetaKeyword'];
                        var MetaDescription = parentThis.GetRows[c]['MetaDescription'];
                        var LongDescription = parentThis.GetRows[c]['LongDescription'];
                        var Image_Url = parentThis.GetRows[c]['Image'].Url;
                        var Image_Name = parentThis.GetRows[c]['Image'].Name;

                        //$("#FreeContentId").val(parseInt(parentThis.getID));
                        $("#edit-Title").val(Title);
                        $("#edit-EnTitle").val(EnTitle);
                        $("#edit-Type").val(Type);
                        $("#edit-Priority").val(Priority);
                        $("#edit-PageTitle").val(PageTitle);
                        $("#edit-MetaKeyword").val(MetaKeyword);
                        $("#edit-MetaDescription").val(MetaDescription);
                        //$("#edit-LongDescription").val(LongDescription);


                        dataEditorUpdate.setData(LongDescription);;


                        //console.log('Image_Url', Image_Url);

                        //var indexOf = Image_Url.lastIndexOf('upload')
                        //console.log('indexOf', indexOf);

                        //Image_Url = Image_Url.slice(indexOf + 7)
                        //console.log('Image_Url', Image_Url);

                        //                        $("#edit-Images").attr('src', parentThis.GetRows[c]['Image']['Url']);

                        //                        $("#edit-Images").attr('src', '/upload/pd.jpg');
                        //if (img_img == 0) {
                        //    $('.img-inner').append(

                        //   ' <div class="col-xs-4">' +
                        //   '<div class="remove-img">' +
                        //        '<i class="fa fa-trash"></i>' +
                        //    '</div>' +
                        //       '<img id="edit-images-update" src=' + '/upload/'+Image_Url + ' class="img-responsive " />' +
                        //    '</div>'
                        //    );
                        //    img_img = 1;
                        //} else {
                        //    $("#edit-images-update").attr('src', '/upload/'+Image_Url );
                        //    console.log(img_img);

                        //}

                        //$('.remove-img').click(function () {
                        //    $('.img-inner').html('');
                        //})
                        //if (Condition == 1) {
                        //    $("#edit-Condition").click();
                        //}


                    }

                }




            });
            $(parentThis.TableName).find(".delete-SubFreeContent").on("click", function (e) {


                e.preventDefault();
                var id = $(this).attr('data-id');
                SubFreeContent.Delete(id);

            });
            //
        });


    };

    SubFreeContent.TableConfig();
    var dataEditor = "", dataEditorUpdate = "";

    ClassicEditor.create(document.querySelector('#SubFC-E-Create'), {
        language: 'fa'
    }).then(editor => {
        dataEditor = editor;

    }).catch(err => {
        console.error(err.stack);
    });

    ClassicEditor.create(document.querySelector('#SubFC'), {
        language: 'fa'
    }).then(editor => {
        dataEditorUpdate = editor;

    }).catch(err => {
        console.error(err.stack);
    });


    $('#edit-Image').change(function (event) {
        var files = this.files[0];
        var tmppath = URL.createObjectURL(event.target.files[0]);
        console.log(tmppath);
        $("#edit-images-update").attr('src', tmppath);
    });



    $("#SubFreeContent-update").click(function () {
        //if ($("form[name='edit_SubFreeContent']").valid()) {

        SubFreeContent.SetDataUpdate("#edit-Title", "Title", false);
        SubFreeContent.SetDataUpdate("#edit-EnTitle", "EnTitle", false);
        //
        //        if ($('#edit-Priority').val() != "") {
        //            SubFreeContent.SetDataUpdate("#edit-Priority", "Priority", false);
        //        }

        if ($('#edit-MetaKeyword').val() != "") {
            SubFreeContent.SetDataUpdate("#edit-MetaKeyword", "MetaKeyword", false);
        }

        if ($('#edit-PageTitle').val() != "") {
            SubFreeContent.SetDataUpdate("#edit-PageTitle", "PageTitle", false);
        }

        if ($('#edit-MetaDescription').val() != "") {
            SubFreeContent.SetDataUpdate("#edit-MetaDescription", "MetaDescription", false);
        }

        if ($('#edit-LongDescription').val() != "") {
            SubFreeContent.SetDataUpdate("LongDescription", dataEditorUpdate.getData(), true);

        }

        if ($('#edit-Condition').is(':checked')) {
            SubFreeContent.SetDataUpdate("Condition", true, true);

        } else {
            SubFreeContent.SetDataUpdate("Condition", false, true);
        }
        SubFreeContent.SetDataUpdate("#FreeContentId", "FreeContentId", false);
        //        SubFreeContent.SetDataUpdate("#edit-images-update", "", false);


        //SubFreeContent.PostUpdateMultiImage('edit-Image', 'SubFreeContent-update');
        SubFreeContent.PostUpdate();

        //}

    });


    $("#SubFreeContent-create").click(function () {

        if ($("form[name='reg_SubFreeContent']").valid()) {
            SubFreeContent.SetDataCreate("#Title", "Title", false);
            SubFreeContent.SetDataCreate("#EnTitle", "EnTitle", false);

            if ($('#Priority').val() != "") {
                SubFreeContent.SetDataCreate("#Priority", "Priority", false);
            }

            if ($('#MetaKeyword').val() != "") {
                SubFreeContent.SetDataCreate("#MetaKeyword", "MetaKeyword", false);
            }

            if ($('#PageTitle').val() != "") {
                SubFreeContent.SetDataCreate("#PageTitle", "PageTitle", false);
            }

            if ($('#MetaDescription').val() != "") {
                SubFreeContent.SetDataCreate("#MetaDescription", "MetaDescription", false);
            }

            if ($('#LongDescription').val() != "") {
                SubFreeContent.SetDataCreate("LongDescription", dataEditor.getData(), true);
            }

            console.log(dataEditor.getData())
            if ($('#ShortDescription').val() != "") {
                SubFreeContent.SetDataCreate("#ShortDescription", "ShortDescription", false);

            }

            if ($('#file').val() != "") {
                SubFreeContent.SetDataCreate("#file", "file", false);

            }

            //SubFreeContent.SetDataCreate("#file", "file", false);

            SubFreeContent.SetDataCreate("#Condition", "Condition", false);
            SubFreeContent.SetDataCreate("#FreeContentId", "FreeContentId", false);
            //            SubFreeContent.Create();

            SubFreeContent.CreateMultiImage('file');

        }
    });




    $('#Condition').click(function () {
        if ($('#Condition').is(':checked')) {
            $('#Condition').val('true');
        } else {
            $('#Condition').val('false');
        }

    });


    $('#edit-Condition').click(function () {
        if ($('#edit-Condition').is(':checked')) {
            $('#edit-Condition').val('true');
        } else {
            $('#edit-Condition').val('false');
        }

    });


    $("#SubFreeContent-MultiDelete").click(function (e) {
        e.preventDefault();
        SubFreeContent.MultiDelete();
    });


});

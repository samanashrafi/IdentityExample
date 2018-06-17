// require(['Action'], function(Action) {
//     var Get_Module;
//
//     Get_Module = Action;
//     var myAction = new Get_Module();
//
// });
define(function () {
    var highlight = function (element, errorClass, validClass, massage) {
        $(element).parents('.fg-line').addClass(errorClass).removeClass(validClass);
        $(element).parents('.form-group .fg-line').children('.check_valid').removeClass('Display-None');

        // $(element).parents('.form-group .fg-line').append('label');
        $(element).parents('.form-group .fg-line').children('label').css({ "display": "block" });
        $(element).parents('.form-group .fg-line').children('label').html(massage);
    };
    var unhighlight = function (element, errorClass, validClass) {
        $(element).parents('.fg-line').addClass(validClass).removeClass(errorClass);
        $(element).parents('.form-group .fg-line').children('.check_valid').removeClass('Display-None');
        $(element).parents('.form-group .fg-line').children('label').css({ "display": "none" });
    };
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "progressBar": true,
        "preventDuplicates": false,
        "positionClass": "toast-bottom-left",
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "10000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    function JsonAction(TableName, ResetBtn, PageName, SetDataUrl, SetUpdateUrl, SetDeleteUrl, SetMultiDeleteUrl, SetSaveUrl) {

        this.TableName = TableName;
        this.ResetBtn = ResetBtn;
        this.PageName = PageName;
        this.SetDataUrl = SetDataUrl;
        this.SetUpdateUrl = SetUpdateUrl;
        this.SetDeleteUrl = SetDeleteUrl;
        this.SetMultiDeleteUrl = SetMultiDeleteUrl;
        this.SetSaveUrl = SetSaveUrl;
        this.dataUpdate = {};
        this.dataCreate = {};
        this.rowIds = [];
        this.GetRowUpdate = [];
        this.GetRowCreate = [];
        this.getID = 0;
        this.flag = false;
        this.GetRows = [];
        this.SetSaveUrl = ;
    }


    JsonAction.prototype.SetDataUpdate = function (name, value) {
        var parentThis = this;
        if (parentThis.GetRowUpdate.indexOf(value) == -1) {
            parentThis.GetRowUpdate.push([name, value]);
        }
    };

    JsonAction.prototype.SetDataUpdatePost = function (name, value) {
        var parentThis = this;

        for (var i = 0; i < parentThis.GetRowUpdate.length; ++i) {
            if (!parentThis.GetRowUpdate[i].indexOf(-1)) {

                parentThis.GetRowUpdate[i].push([name, value]);

            }
            else {
                parentThis.GetRowUpdate[i].slice(1);

                parentThis.GetRowUpdate[i].push([name, value]);

                break;
            }
        }

    };

    JsonAction.prototype.SetDataCreate = function (name, value, flag) {
        var parentThis = this;
        if (parentThis.GetRowCreate.indexOf(value) == -1) {
            parentThis.GetRowCreate.push([name, value, flag]);
        }
    };

    JsonAction.prototype.PostUpdate = function () {
        //
        var parentThis = this;

        for (var i = 0; i < parentThis.GetRowUpdate.length; ++i) {
            parentThis.dataUpdate[parentThis.GetRowUpdate[i][1]] = $(parentThis.GetRowUpdate[i][0]).val();
        }
        console.log(parentThis.dataUpdate);
        $.ajax({
            headers: {
                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
            },
            url: parentThis.SetUpdateUrl +'/'+ parentThis.getID,
            method: "post",
            data: parentThis.dataUpdate,
            success: function (response) {

             

                if (response.success) {

                    
                    console.log('success');

                    setTimeout(function () {
                        ReloadTable(parentThis.TableName);
                        toastr.success(response.responseText);
                    }, 500);
                    $('#Edit-Modal').find('.close').click();

                } else {
                    console.log('error')
                    toastr.error(response.responseText);

                }

            },
            error: function (response) {
   
                var json = JSON.parse(response.success);
                if (json != "true") {

                    for (var i = 0; i < parentThis.GetRowUpdate.length; i++)
                        //console.log(parentThis.GetRowUpdate[i]["key"] + '==' + json[parentThis.GetRowUpdate[i]["errors"]])

                        if (json[parentThis.GetRowUpdate[i][0]]) {
                            console.log(parentThis.GetRowUpdate[i][0] + ' == ' + json[parentthis.GetRowUpdate[i][1]]);
                            highlight(parentThis.GetRowUpdate[i][0] + "has-error has-feedback", "has-success has-feedback", json[parentThis.GetRowUpdate[i][1]]);
                        }
                        else {
                            unhighlight(parentThis.GetRowUpdate[i][0], "has-error has-feedback", "has-success has-feedback");
                        }

                }

                if (response.success) {

                    toastr.error(response.responseText);

                    return false;
                }

                if (json) {
                    toastr.error(" لطفا دوباره ورودی های خود را بررسی فرمایید...");

                    return false;
                }

            }


        });


    };
    JsonAction.prototype.PostUpdateMultiImage = function (fileInput) {
        var parentThis = this;
        var files = $("#" + fileInput).get(0).files;
        console.log(files);
        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            //fileData.push(files[i]);
            fileData[i] = files[i];
        }
        console.log(fileData);
        this.dataCreate["Images"] = fileData;
        for (var i = 0; i < parentThis.GetRowUpdate.length; ++i) {
            // this.dataCreate[parentThis.GetRowUpdate[i][1]] = $(parentThis.GetRowUpdate[i][0]).val();
            if (parentThis.GetRowUpdate[i][2] == true) {
                this.dataCreate[parentThis.GetRowUpdate[i][0]] = parentThis.GetRowUpdate[i][1];

            } else {
                this.dataCreate[parentThis.GetRowUpdate[i][1]] = $(parentThis.GetRowUpdate[i][0]).val();

            }
        }


        $.ajax({
            url: parentThis.SetUpdateUrl,
            dataType: 'json',
            data: parentThis.dataUpdate,
            method: "post",
            //contentType: false,
            processData: false,
            contentType: 'application/x-www-form-urlencoded',
            headers: {
                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
            },
            success: function (response) {


                $('.btn-clear').click();

                setTimeout(function () {
                    toastr.success(response.responseText)

                    ReloadTable(parentThis.TableName);
                }, 500);

            },
            error: function (response) {



                if (json != "true") {

                    for (var i = 0; i < parentThis.GetRowUpdate.length; i++)

                        if (json[parentThis.GetRowUpdate[i][0]]) {
                            highlight(parentThis.GetRowUpdate[i][0] + "has-error has-feedback", "has-success has-feedback", json[parentThis.GetRowUpdate[i][1]]);
                        }
                        else {
                            unhighlight(parentThis.GetRowUpdate[i][0], "has-error has-feedback", "has-success has-feedback");
                        }

                }

                if (response.responseJSON["message"]) {
                    toastr.error(response.responseText);
                    return false;
                }

                if (json) {
                    toastr.error(" لطفا دوباره ورودی های خود را بررسی فرمایید...");
                    return false;
                }


            }

        });
    };

    JsonAction.prototype.Create = function () {
        var parentThis = this;
        for (var i = 0; i < parentThis.GetRowCreate.length; ++i) {
            // this.dataCreate[parentThis.GetRowCreate[i][1]] = $(parentThis.GetRowCreate[i][0]).val();
            if (parentThis.GetRowCreate[i][2] == true) {
                this.dataCreate[parentThis.GetRowCreate[i][0]] = parentThis.GetRowCreate[i][1];

            } else {
                this.dataCreate[parentThis.GetRowCreate[i][1]] = $(parentThis.GetRowCreate[i][0]).val();

            }
        }
        console.log(parentThis.dataCreate);
        $.ajax({
            url: parentThis.SetSaveUrl,
            dataType: 'json',
            data: parentThis.dataCreate,
            method: "post",
            // contentType: 'multipart/form-data',
            headers: {
                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
            },
            success: function (response) {

                $('.btn-clear').click();

                setTimeout(function () {
                    toastr.success(response.responseText)

                    ReloadTable(parentThis.TableName);
                }, 500);

            },
            error: function (response) {

                console.log('bad');
                console.log(response);
                //var json = response.responseJSON;
                //if (json != "true") {

                //    for (var i = 0; i < parentThis.GetRowCreate.length; i++)

                //        if (json[parentThis.GetRowCreate[i][0]]) {
                //            highlight(parentThis.GetRowCreate[i][0] + "has-error has-feedback", "has-success has-feedback", json[parentThis.GetRowCreate[i][1]]);
                //        }
                //        else {
                //            unhighlight(parentThis.GetRowCreate[i][0], "has-error has-feedback", "has-success has-feedback");
                //        }

                //}

                //if (response.responseJSON["message"]) {
                //    toastr.error(response.responseText);
                //    return false;
                //}

                //if (json) {
                //    toastr.error(" لطفا دوباره ورودی های خود را بررسی فرمایید...");
                //    return false;
                //}


            }

        });
    };

    JsonAction.prototype.CreateMultiImage = function (fileInput) {
        var parentThis = this;
        var files = $("#" + fileInput).get(0).files;
        console.log(files);
        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            //fileData.push(files[i]);
            fileData[i] = files[i];
        }
        console.log(fileData);
        this.dataCreate["Image"] = fileData;
        for (var i = 0; i < parentThis.GetRowCreate.length; ++i) {
            // this.dataCreate[parentThis.GetRowCreate[i][1]] = $(parentThis.GetRowCreate[i][0]).val();
            if (parentThis.GetRowCreate[i][2] == true) {
                this.dataCreate[parentThis.GetRowCreate[i][0]] = parentThis.GetRowCreate[i][1];

            } else {
                this.dataCreate[parentThis.GetRowCreate[i][1]] = $(parentThis.GetRowCreate[i][0]).val();

            }
        }
        console.log(this.dataCreate);


        $.ajax({
            url: parentThis.SetSaveUrl,
            dataType: 'json',
            data: parentThis.dataCreate,
            method: "post",
            //contentType: false,
            processData: false,
            contentType: 'application/x-www-form-urlencoded',
            headers: {
                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
            },
            success: function (response) {


                $('.btn-clear').click();

                setTimeout(function () {
                    toastr.success(response.responseText)

                    ReloadTable(parentThis.TableName);
                }, 500);

            },
            error: function (response) {



                if (json != "true") {

                    for (var i = 0; i < parentThis.GetRowCreate.length; i++)

                        if (json[parentThis.GetRowCreate[i][0]]) {
                            highlight(parentThis.GetRowCreate[i][0] + "has-error has-feedback", "has-success has-feedback", json[parentThis.GetRowCreate[i][1]]);
                        }
                        else {
                            unhighlight(parentThis.GetRowCreate[i][0], "has-error has-feedback", "has-success has-feedback");
                        }

                }

                if (response.responseJSON["message"]) {
                    toastr.error(response.responseText);
                    return false;
                }

                if (json) {
                    toastr.error(" لطفا دوباره ورودی های خود را بررسی فرمایید...");
                    return false;
                }


            }

        });
    };

    JsonAction.prototype.Delete = function (id) {
        var parentThis = this;


        swal({
            title: "حذف گروهی",
            text: "  آیا مطمئن هستید که این " + parentThis.PageName + " و تمام تعاریف مرتبط با آن حذف شود؟",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'حذف شوند',
            cancelButtonText: 'انصراف'
        }, function () {

            $.ajax({
                // url:"{{  route('delete-educational-branch', ['branch' => null]) }}" + "/" + id_Delete ,

                url: parentThis.SetDeleteUrl + "/" + id,
                method: "post",
                headers: {
                    'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                },
                success: function (response) {

                    $('.btn-clear').click();
                    setTimeout(function () {
                        toastr.success(response.responseText)
                        ReloadTable(parentThis.TableName);
                    }, 500);
                 
                },
                error: function (response) {
                    toastr.error(response.responseTex);
                }
            });
        });






    };

    JsonAction.prototype.MultiDelete = function () {
        var parentThis = this;

        if (parentThis.rowIds.length != 0) {
            swal({
                title: 'حذف ' + parentThis.PageName,
                text: "آیا مطمئن هستید که " + parentThis.PageName + "<b class='c-red'>" + "(" + parentThis.rowIds.length + " آیتم) " + "</b>" + " و تمام تعاریف مرتبط با آنها حذف شوند؟",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'حذف شوند',
                cancelButtonText: 'انصراف'
            }, function () {

                var data = JSON.stringify({ rowIds: parentThis.rowIds })
                console.log({ data: parentThis.rowIds });

                $.ajax({
                    url: parentThis.SetMultiDeleteUrl,
                    dataType: 'json',
                    data: { Id: parentThis.rowIds },
                    method: "post",
                    traditional: true,
                    headers: {
                        'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                    },
                    success: function (response) {

                        $(parentThis.ResetBtn).click();

                        $('.btn-clear').click();

                        setTimeout(function () {
                            toastr.success(response.responseText)
                            ReloadTable(parentThis.TableName);
                        }, 500);
                    },
                    error: function (response) {
                        toastr.error(response.responseTex);


                    }
                });
            });

        } else {

            toastr.error('هیچ آیمتی انتخاب نشده است...')

        }




    };

    var ReloadTable = function (tableName) {

        $(tableName).bootgrid('reload');

    };

    JsonAction.prototype.UpdateTable = function () {
        var parentThis = this;
        $(parentThis.TableName).bootgrid('reload');
    };

    return JsonAction

});

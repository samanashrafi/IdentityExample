
/*

 در این فایل تمامی توابعی که در کل پروژه قابل استفاده است نوشته میشود........
 */





(function ($) {


    /*
     پاک کردن اعتبارسنجی فرم
     */




    /*
     پاک کردن فرم بدون input
     */
    $.fn.ClearForm = function (form) {
        $.fn.ClearValidation(form);
        $('.form-group .fg-line .validcheck').addClass('display-none');
        $('.form-group .fg-line ').removeClass('has-success');
    };

    /*
     پاک کردن فرم بدون با input
     */
    $.fn.ClearForm_input = function (form) {
        $('input,textarea').val("");
        $.fn.ClearValidation(form);
        $('.form-group .fg-line .validcheck').addClass('display-none');
        $('.form-group .fg-line ').removeClass('has-success');
    };

    /*
     تنظیک کردم drop down به مقدار اولیه
     */
    $.fn.RestDropDow = function (Id) {
        $(Id).val('default');
        $(Id).selectpicker("refresh");
    };

    /*
     اعتبار سنجی drop down
     */

    $.fn.RestDropDow_Default = function (id) {
        var get_value = $(id + " option:selected").text();
        if (get_value != '') {
            $(id).parents('.fg-line').addClass("has-success has-feedback").removeClass("has-error has-feedback");
            $(id).parents('.form-group .fg-line').children('.validcheck').removeClass('display-none');
            $(id).parents('.form-group .fg-line').children('.validcheck').addClass('zmdi-check').removeClass('zmdi-close');
            $(id).parents('.form-group .fg-line').children('label').css({ "display": "none" });
            $(id + '-error').css({ "display": "none" });
        }
    };



}(jQuery));




/*
جلوگیری کردن از وارد کردن اعداد انگلیسی text box
 */
$('*').on('keydown', '.just-number', function (e) { -1 !== $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) || /65|67|86|88/.test(e.keyCode) && (!0 === e.ctrlKey || !0 === e.metaKey) || 35 <= e.keyCode && 40 >= e.keyCode || (e.shiftKey || 48 > e.keyCode || 57 < e.keyCode) && (96 > e.keyCode || 105 < e.keyCode) && e.preventDefault() });

function ClearValidation(form) {
    var v = $(form).validate();
    $(form).each(function () {
        v.successList.push(this);
        v.showErrors();
    });
    v.resetForm();
    v.reset();

};

/*
 پاک کردن فرم بدون با input
 */
var ClearForm_input = function (form) {
    $('input,textarea').val("");
    ClearValidation(form);
    $('.form-group .fg-line .validcheck').addClass('display-none');
    $('.form-group .fg-line ').removeClass('has-success');
};

/*
 تنظیک کردم drop down به مقدار اولیه
 */
var RestDropDow = function (Id) {
    $(Id).val('default');
    $(Id).selectpicker("refresh");
};

/*
اعتبار سنجی drop down
 */

var Validation_Dropdown = function (id) {
    var get_value = $(id + " option:selected").text();
    if (get_value != '') {
        $(id).parents('.fg-line').addClass("has-success has-feedback").removeClass("has-error has-feedback");
        $(id).parents('.form-group .fg-line').children('.validcheck').removeClass('display-none');
        $(id).parents('.form-group .fg-line').children('.validcheck').addClass('zmdi-check').removeClass('zmdi-close');
        $(id).parents('.form-group .fg-line').children('label').css({ "display": "none" });
        $(id + '-error').css({ "display": "none" });
    }
};

/*
 نمایش دادن و پنهان کردن دو المان
 */
var ShowHide_Element = function (HideId, ShowId) {
    $(HideId).hide();
    $(ShowId).show();
};

/*
 نمایش پیغام
 */
function notify(from, align, icon, type, animIn, animOut, title, message) {
    $.growl({
        icon: icon,
        title: title,
        message: message,
        url: ''
    }, {
        element: 'body',
        type: type,
        allow_dismiss: true,
        placement: {
            from: from,
            align: align
        },
        offset: {
            x: 20,
            y: 85
        },
        spacing: 10,
        z_index: 1031,
        delay: 2500,
        timer: 5000,
        url_target: '_blank',
        mouse_over: false,
        animate: {
            enter: animIn,
            exit: animOut
        },
        icon_type: 'class',
        template: '<div data-growl="container" class="alert" role="alert">' +
        '<button type="button" class="close m-0"  data-growl="dismiss">' +
        '<span aria-hidden="true" style="vertical-align: sub">&times;</span>' +
        '<span class="sr-only">Close</span>' +
        '</button>' +
        '<span data-growl="icon"></span>' +
        '<span data-growl="title"></span>' +
        '<span data-growl="message" style="vertical-align: sub"></span>' +
        '<a href="#" data-growl="url"></a>' +
        '</div>'
    });
}


/*
 قرمز کردن کادر ها textbox در موقعی که اعتبار سنجی خطا داشته باشد
 */
var highlight = function (element, errorClass, validClass, massage) {
    $(element).parents('.fg-line').addClass(errorClass).removeClass(validClass);
    $(element).parents('.form-group .fg-line').children('.validcheck').removeClass('display-none');
    $(element).parents('.form-group .fg-line').children('.validcheck').addClass('zmdi-close').removeClass('zmdi-check');
    $(element).parents('.form-group .fg-line').children('label').css({ "display": "block" });
    $(element).parents('.form-group .fg-line').children('label').html(massage);
};

/*
 سبز کردن کادر ها textbox در موقعی که اعتبار سنجی خطا نداشته باشد
 */
var unhighlight = function (element, errorClass, validClass) {
    $(element).parents('.fg-line').addClass(validClass).removeClass(errorClass);
    $(element).parents('.form-group .fg-line').children('.validcheck').removeClass('display-none');
    $(element).parents('.form-group .fg-line').children('.validcheck').addClass('zmdi-check').removeClass('zmdi-close');
    $(element).parents('.form-group .fg-line').children('label').css({ "display": "none" });
};



/*
جدا سازی ارقام به صورت 3 تا
 */

function Comma_separator(id, value) {
    var separator = ",";
    var int = value.replace(new RegExp(separator, "g"), "");
    var regexp = new RegExp("\\B(\\d{3})(" + separator + "|$)");
    do {
        int = int.replace(regexp, separator + "$1");
    }
    while (int.search(regexp) >= 0)
    value.value = int;
    document.getElementById(id).innerHTML = int;
}


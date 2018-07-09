$(function () {

    /*
   ----------------> global
   */

    $('*').persiaNumber();

    /*

    ----------------> dropdown menu
    */


    $('ul.nav li.dropdown').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(500);
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(500);
    });



    /*
  ----------------> hamberger menu
  */

    var navbar_toggle = 0;
    $('.navbar-toggle').click(function () {
        if (navbar_toggle == 0) {
            $('#bs-example-navbar-collapse-1').addClass('display-menu-m');
            $('.navbar-nav').addClass('display-menu-m');
            $('.navbar-nav:after').addClass('display-menu-m');
            navbar_toggle = 1;
        } else {
            $('#bs-example-navbar-collapse-1').removeClass('display-menu-m');
            $('.navbar-nav').removeClass('display-menu-m');
            $('.navbar-nav:after').removeClass('display-menu-m');
            navbar_toggle = 0;
        }

        console.log('navbar-toggle');

    });



    $('.close-menu,.navbar-collapse::after').click(function () {
        console.log('close-menu');
        $('.navbar-toggle').click();
    });



    /*
   ----------------> slide Show
   */

    //=========================================================
    var c = $("#div-layerSlider-can").find("a").length;
    var w = $("#div-layerSlider-place").width();
    $("#div-layerSlider-can").find("a").width(w);
    $("#div-layerSlider-can").width(c * w);
    //=========================================================
    var cc = 0;
    function verticalCenter() {
        var h = $(".img_slider_mob").first().height();
        var h2 = $("#slider-forward").height();
        //$("#slider-forward").css({ top: h / 2 - h2 / 2 });
        h2 = $("#slider-backward").height();
        //$("#slider-backward").css({ top: h / 2 - h2 / 2 });
    }
    $("#slider-forward").click(function () {
        verticalCenter();

        if (cc >= c - 1) {
            $("#div-layerSlider-can").animate({
                marginRight: '0px'
            }, 500, function () {

                cc = 0;
            });
        }
        else {
            $("#div-layerSlider-can").animate({
                marginRight: '-=' + w + 'px'
            }, 500, function () {

                cc = cc + 1;
            });
        }

        $(".slider-pin").removeClass("slider-pin-active");
        $("#slider-pin-" + cc).addClass("slider-pin-active");


    });
    $("#slider-backward").click(function () {
        verticalCenter();

        //==================
        if (cc <= 0) {
            $("#div-layerSlider-can").animate({
                marginRight: '-=' + ((c - 1) * w) + 'px'
            }, 500, function () {

                cc = c - 1;

            });
        }
        else {
            $("#div-layerSlider-can").animate({
                marginRight: '+=' + w + 'px'
            }, 500, function () {

                cc = cc - 1;

            });
        }

        $(".slider-pin").removeClass("slider-pin-active");
        $("#slider-pin-" + cc).addClass("slider-pin-active");


    });

    //verticalCenter();

    var slideTimer = null;
    function startSlider() {
        slideTimer = setInterval(function () {


            $("#slider-forward").click();

        }, 6000);
    }

    startSlider();

    $("#div-layerSlider-place").mouseenter(function () {
        clearInterval(slideTimer);
    });
    $("#div-layerSlider-place").mouseleave(function () {
        startSlider();
    });

    $(".slider-pin").click(function (e) {
        e.preventDefault();
        var img = $(this);
        var i = parseInt(img.attr("data-index"));
        cc = i;
        $(".slider-pin").removeClass("slider-pin-active");
        img.addClass("slider-pin-active");




        $("#div-layerSlider-can").animate({
            marginRight: (-i * w) + 'px'
        }, 500, function () {

        });

    });


});
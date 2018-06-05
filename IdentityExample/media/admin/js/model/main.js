
require.config({
    paths: {
        // educationalyears: 'educational-years',
        JsonAction: 'JsonAction',
        
    }

});



$(document).ready(function () {



    /*
    ---------------- جستجو بوت گرید
    */

    var SearchShow = 0;
    $("#SearchShow").click(function () {

        
        if (SearchShow == 0) {
            $('.search').addClass("block-opacity");
          
            SearchShow = 1;
        }

        $(".input-group-addon").click(function () {
          
            setTimeout(function () {
                $('.search').removeClass("block-opacity");
                SearchShow = 0;
                console.log(SearchShow);
            }, 300);
          
        });

    });



  
   

});






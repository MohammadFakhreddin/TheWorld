(function() {//For removing global using 

    

    //var ele = $("#myname");//JQuery gruantese that every thig will be same in all browsers
    //ele.text("Mohammad Fakhreddin");

    var main = $("#header");
    main.on("mouseenter", function () {
        main.style.backgroundColor = "#888";//For simple using
        //main.style = "background: #888;";//For jQuery
    });

    main.on("mouseleave",function () {
        //main.style.backgroundColor = "";//Making it to it's default color//For simple using
        main.style = "";//For jQuery
    });

    var menuItems = $("ul.menu li a");
    menuItems.on("click", function () {
        var me = $(this);
     //   alert(me.text()); 
    });

    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");

    var sidebarToggle = $("#sidebarToggle");
    sidebarToggle.on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");    
        } else {
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left");
        }
    });
})();
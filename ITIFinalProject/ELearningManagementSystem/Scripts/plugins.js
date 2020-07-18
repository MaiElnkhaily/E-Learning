$(document).ready(function()
{
    "use strict";
    /*$("li a").onclick= function(){

    $(this).addClass("active");
    };*/
  
    //nice scroll
    $("html").niceScroll();
    
    $('.carousel').carousel(
    {
        interval:6000
    });
    
    //show color option div when click on the gear
    $(".gear-cheack").click(function()
    {
        $(".color-option").fadeToggle();
    });
    
    //Change theme color
    var colorLi=$(".color-option ul li");
    colorLi
        .eq(0).css("backgroundColor","#E41B17").end()
        .eq(1).css("backgroundColor","#E426D5").end()
        .eq(2).css("backgroundColor","#009AFF").end()
        .eq(3).css("backgroundColor","#FFD500");
    colorLi.click(function()
    {
        $("link[href*='theme']").attr("href",$(this).attr("data-value"));
    });
    
    //caching the scroll top element
    var scrollButton =$("#scroll-top");
    $(window).scroll(function()
    {
        $(this).scrollTop()>=700 ? scrollButton.show() : scrollButton.hide();
    });
        
        //click on button to scroll up
        scrollButton.click(function()
        {
            $("html,body").animate({scrollTop:0},600);
        });
    
    //loading screen
    $(window).load(function()
    {
    //loading elements
       $(".loading-overlay .spinner").fadeOut(1000,
        function()
        {
           $(this).parent().fadeOut(2000,
            function()
            {
               //show the scroll
               $("body").css("overflow","auto");
               $(this).remove();
            });
        }); 
    });
    
    //loading screen
    $(window).load(function()
    {
        $(".loading-overlay,.loading-overlay .spinner").fadeOut(2000);
    });
    //dashbord
    $(function(){
    $('[data-toggle="tooltip"]').tooltip();
    $(".side-nav .collapse").on("hide.bs.collapse", function() {                   
        $(this).prev().find(".fa").eq(1).removeClass("fa-angle-right").addClass("fa-angle-down");
    });
    $('.side-nav .collapse').on("show.bs.collapse", function() {                        
        $(this).prev().find(".fa").eq(1).removeClass("fa-angle-down").addClass("fa-angle-right");        
    });
}) 
  //login form
$(function() {
  
  // contact form animations
  $('#contact').click(function() {
    $('#contactForm').fadeToggle();
  })
  $(document).mouseup(function (e) {
    var container = $("#contactForm");

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        container.fadeOut();
    }
  });
});
 //chat
    $(document).ready(function(){
    $('#action_menu_btn').click(function(){
        $('.action_menu').toggle();
    });
        });
    
});


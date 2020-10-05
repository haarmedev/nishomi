// Menu Init
$('.burger, .overlay').click(function(){
  $('.burger').toggleClass('clicked');
  $('.overlay').toggleClass('show');
  $('nav').toggleClass('show');
  $('body').toggleClass('overflow');
});

//smooth scroll
$(function(){
  var $window = $(window);
  var scrollTime = 1.2;
  var scrollDistance = 170;

});

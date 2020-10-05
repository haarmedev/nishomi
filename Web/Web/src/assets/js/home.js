// Carousel

var swiper = new Swiper('.one', {
	autoplay: 6000,
	pagination: '.prodpage',
	paginationClickable: true,
	breakpoints: {
		640: {
			slidesPerView: 1,
			spaceBetween: 15,
			slidesPerGroup: 1,
		},
		768: {
			slidesPerView: 2,
			spaceBetween: 10,
			slidesPerGroup: 2,
		},
		1024: {
			slidesPerView: 3,
			spaceBetween: 10,
			slidesPerGroup: 3,
		},
		1920: {
			slidesPerView: 5,
			spaceBetween: 15,
			slidesPerGroup: 4,
			centeredSlides: true,
			loop: true,
			loopFillGroupWithBlank: true,
		},
	}
});


var swiper = new Swiper('.topprod', {
	slidesPerView: 1,
	spaceBetween: 0,
	effect: 'fade',
	speed: 1500,
	spaceBetween: 100,
	slidesPerGroup: 1,
	centeredSlides: true,
	autoplay: 5000,
	loop: true,
	loopFillGroupWithBlank: true,
	keyboardControl: true,
	pagination: '.bannpage',
	paginationClickable: true,
});
if($(window).width() >= 1200){
	var swiper = new Swiper('.two', {
		pagination: '.swiper-pagination',
		paginationClickable: true,
		effect: 'coverflow',
		loop: true,
		centeredSlides: true,
		slidesPerView: 'auto',
		coverflow: {
			rotate: 0,
			stretch: 130,
			depth: 140,
			modifier: 1.5,
			slideShadows : false,
		},
		keyboardControl: true,
		nextButton: '.swiper-button-next',
		prevButton: '.swiper-button-prev',
	});
}
 if ( $(window).width() > 767 && $(window).width() < 1200) {
	var swiper = new Swiper('.two', {
		pagination: '.swiper-pagination',
		paginationClickable: true,
		effect: 'coverflow',
		loop: true,
		centeredSlides: true,
		slidesPerView: 'auto',
		coverflow: {
			rotate: 0,
			stretch: 100,
			depth: 100,
			modifier: 2.5,
			slideShadows : false,
		},
		keyboardControl: true,
		nextButton: '.swiper-button-next',
		prevButton: '.swiper-button-prev',
	});
}
if($(window).width() < 766){
	var swiper = new Swiper('.two', {
		pagination: '.swiper-pagination',
		paginationClickable: true,
		effect: 'flip',
		loop: true,
		centeredSlides: true,
		slidesPerView: 'auto',
		keyboardControl: true,
		nextButton: '.swiper-button-next',
		prevButton: '.swiper-button-prev',
	});
}

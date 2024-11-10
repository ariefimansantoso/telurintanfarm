/*
Author       : Dreamguys
Template Name: POS - Bootstrap Admin Template
*/


$(document).ready(function () {

	// Variables declarations
	var $wrapper = $('.main-wrapper');
	var $slimScrolls = $('.slimscroll');
	var $pageWrapper = $('.page-wrapper');
	feather.replace();

	// Page Content Height Resize
	$(window).resize(function () {
		if ($('.page-wrapper').length > 0) {
			var height = $(window).height();
			$(".page-wrapper").css("min-height", height);
		}
	});

	// Mobile menu sidebar overlay
	$('body').append('<div class="sidebar-overlay"></div>');
	$(document).on('click', '#mobile_btn', function () {
		$wrapper.toggleClass('slide-nav');
		$('.sidebar-overlay').toggleClass('opened');
		$('html').addClass('menu-opened');
		$('#task_window').removeClass('opened');
		return false;
	});

	$(".sidebar-overlay").on("click", function () {
		$('html').removeClass('menu-opened');
		$(this).removeClass('opened');
		$wrapper.removeClass('slide-nav');
		$('.sidebar-overlay').removeClass('opened');
		$('#task_window').removeClass('opened');
	});

	// on mobile view, when any menu item is click, it should automatically hide the menu.
	$('#sidebar-menu').on('click', 'a', function (event) {
		if ($(this).attr('href') !== 'javascript:void(0);') {
			if ($('#mobile_btn').css('display') !== 'none') {
				$wrapper.toggleClass('slide-nav');
				$('.sidebar-overlay').toggleClass('opened');
				$('html').addClass('menu-opened');
				$('#task_window').removeClass('opened');

				window.location.href = $(this).attr('href');

				return false;
			}
		}
	});

	// Logo Hide Btn

	$(document).on("click", ".hideset", function () {
		$(this).parent().parent().parent().hide();
	});

	$(document).on("click", ".delete-set", function () {
		$(this).parent().parent().hide();
	});
	// Loader
	setTimeout(function () {
		$('#global-loader');
		setTimeout(function () {
			$("#global-loader").fadeOut("slow");
		}, 100);
	}, 500);

	// Datetimepicker
	if ($('.datetimepicker').length > 0) {
		$('.datetimepicker').datetimepicker({
			format: 'DD-MM-YYYY',
			icons: {
				up: "fas fa-angle-up",
				down: "fas fa-angle-down",
				next: 'fas fa-angle-right',
				previous: 'fas fa-angle-left'
			}
		});
	}

	// toggle-password
	if ($('.toggle-password').length > 0) {
		$(document).on('click', '.toggle-password', function () {
			$(this).toggleClass("fa-eye fa-eye-slash");
			var input = $(".pass-input");
			if (input.attr("type") == "password") {
				input.attr("type", "text");
			} else {
				input.attr("type", "password");
			}
		});
	}
	if ($('.toggle-passwords').length > 0) {
		$(document).on('click', '.toggle-passwords', function () {
			$(this).toggleClass("fa-eye fa-eye-slash");
			var input = $(".pass-inputs");
			if (input.attr("type") == "password") {
				input.attr("type", "text");
			} else {
				input.attr("type", "password");
			}
		});
	}
	if ($('.toggle-passworda').length > 0) {
		$(document).on('click', '.toggle-passworda', function () {
			$(this).toggleClass("fa-eye fa-eye-slash");
			var input = $(".pass-inputs");
			if (input.attr("type") == "password") {
				input.attr("type", "text");
			} else {
				input.attr("type", "password");
			}
		});
	}

	// Select 2
	if ($('.select').length > 0) {
		$('.select').select2({
			minimumResultsForSearch: -1,
			width: '100%'
		});
	}

	// Counter 
	if ($('.counter').length > 0) {
		$('.counter').counterUp({
			delay: 20,
			time: 2000
		});
	}
	if ($('#timer-countdown').length > 0) {
		$('#timer-countdown').countdown({
			from: 180, // 3 minutes (3*60)
			to: 0, // stop at zero
			movingUnit: 1000, // 1000 for 1 second increment/decrements
			timerEnd: undefined,
			outputPattern: '$day Day $hour : $minute : $second',
			autostart: true
		});
	}

	if ($('#timer-countup').length > 0) {
		$('#timer-countup').countdown({
			from: 0,
			to: 180
		});
	}

	if ($('#timer-countinbetween').length > 0) {
		$('#timer-countinbetween').countdown({
			from: 30,
			to: 20
		});
	}

	if ($('#timer-countercallback').length > 0) {
		$('#timer-countercallback').countdown({
			from: 10,
			to: 0,
			timerEnd: function () {
				this.css({ 'text-decoration': 'line-through' }).animate({ 'opacity': .5 }, 500);
			}
		});
	}

	if ($('#timer-outputpattern').length > 0) {
		$('#timer-outputpattern').countdown({
			outputPattern: '$day Days $hour Hour $minute Min $second Sec..',
			from: 60 * 60 * 24 * 3
		});
	}

	// Summernote

	if ($('#summernote').length > 0) {
		$('#summernote').summernote({
			height: 300,                 // set editor height
			minHeight: null,             // set minimum height of editor
			maxHeight: null,             // set maximum height of editor
			focus: true                  // set focus to editable area after initializing summernote
		});
	}



	// Sidebar Slimscroll
	if ($slimScrolls.length > 0) {
		$slimScrolls.slimScroll({
			height: 'auto',
			width: '100%',
			position: 'right',
			size: '7px',
			color: '#ccc',
			wheelStep: 10,
			touchScrollStep: 100
		});
		var wHeight = $(window).height() - 60;
		$slimScrolls.height(wHeight);
		$('.sidebar .slimScrollDiv').height(wHeight);
		$(window).resize(function () {
			var rHeight = $(window).height() - 60;
			$slimScrolls.height(rHeight);
			$('.sidebar .slimScrollDiv').height(rHeight);
		});
	}

	// Sidebar
	var Sidemenu = function () {
		this.$menuItem = $('#sidebar-menu a');
	};

	function init() {
		var $this = Sidemenu;
		$('#sidebar-menu a').on('click', function (e) {
			if ($(this).parent().hasClass('submenu')) {
				e.preventDefault();
			}
			if (!$(this).hasClass('subdrop')) {
				// $('ul', $(this).parents('ul:first')).slideUp(250);
				$('a', $(this).parents('ul:first')).removeClass('subdrop');
				$(this).next('ul').slideDown(350);
				$(this).addClass('subdrop');
			} else if ($(this).hasClass('subdrop')) {
				$(this).removeClass('subdrop');
				$(this).next('ul').slideUp(350);
			}
		});
		$('#sidebar-menu ul li.submenu a.active').parents('li:last').children('a:first').addClass('active').trigger('click');
	}

	// Sidebar Initiate
	init();
	$(document).on('mouseover', function (e) {
		e.stopPropagation();
		if ($('body').hasClass('mini-sidebar') && $('#toggle_btn').is(':visible')) {
			var targ = $(e.target).closest('.sidebar, .header-left').length;
			if (targ) {
				$('body').addClass('expand-menu');
				$('.subdrop + ul').slideDown();
			} else {
				$('body').removeClass('expand-menu');
				$('.subdrop + ul').slideUp();
			}
			return false;
		}
	});

	$('.submenus').on('click', function () {
		$('body').addClass('sidebarrightmenu');
	});

	// toggle-password
	if ($('.toggle-password').length > 0) {
		$(document).on('click', '.toggle-password', function () {
			$(this).toggleClass("fa-eye fa-eye");
			var input = $(".pass-input");
			if (input.attr("type") == "text") {
				input.attr("type", "text");
			} else {
				input.attr("type", "password");
			}
		});
	}


	if ($('.win-maximize').length > 0) {
		$('.win-maximize').on('click', function (e) {
			if (!document.fullscreenElement) {
				document.documentElement.requestFullscreen();
			} else {
				if (document.exitFullscreen) {
					document.exitFullscreen();
				}
			}
		})
	}
});
window.getComponentHtmlById = (elementId) => {
	const element = document.getElementById(elementId);
	return element ? element.outerHTML : null;
};

window.HTMLToPdf = async (fileName, contentStreamReference) => {
	const arrayBuffer = await contentStreamReference.arrayBuffer();
	const blob = new Blob([arrayBuffer]);
	const url = URL.createObjectURL(blob);
	const anchorElement = document.createElement('a');
	anchorElement.href = url;
	anchorElement.download = fileName ?? '';
	anchorElement.click();
	anchorElement.remove();
	URL.revokeObjectURL(url);
}

window.getElementHeight = function (elementId) {
	var element = document.getElementById(elementId);
	if (element) {
		return element.clientHeight;
	}
	return 0;
};

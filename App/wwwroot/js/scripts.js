
jQuery(document).ready(function() {
	
//------------------------SIDEBAR--------------------------------------------
	$('.dismiss, .overlay').on('click', function() {
        $('.sidebar').removeClass('active');
        $('.overlay').removeClass('active');
    });

    $('.open-menu').on('click', function(e) {
    	e.preventDefault();
        $('.sidebar').addClass('active');
		$('.overlay').addClass('active');

        //Fechar submenus abertos
        $('.collapse.show').toggleClass('show');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
   
	//Substitui a barra de rolagem padrão do navegador na sidebar, caso o menu da sidebar tenha uma altura maior que o viewport
	$('.sidebar').mCustomScrollbar({
		theme: "minimal-dark"
	});
	
});
//------------------------END-SIDEBAR------------------------------------------


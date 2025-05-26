jQuery(document).ready(function() {
	//Substitui a barra de rolagem padrão do navegador na sidebar, caso o menu da sidebar tenha uma altura maior que o viewport
	$('.sidebar').mCustomScrollbar({
		theme: "minimal-dark",
		advanced: {
			updateOnContentResize: true,
			updateOnImageLoad: true
		},
		callbacks: {
			onInit: function() {
				// Configurar event listeners como passive para melhor performance
				this.mcs.content.get(0).addEventListener('touchstart', function(e) {
					// Event handler passivo
				}, { passive: true });
				
				this.mcs.content.get(0).addEventListener('touchmove', function(e) {
					// Event handler passivo
				}, { passive: true });
			}
		}
	});
	
	// Configurar event listeners globais como passive para melhor performance
	if (typeof window !== 'undefined' && window.addEventListener) {
		// Override jQuery's event handling for touch events to be passive by default
		var originalOn = $.fn.on;
		$.fn.on = function(events, selector, data, handler) {
			if (typeof events === 'string' && (events.includes('touchstart') || events.includes('touchmove') || events.includes('wheel'))) {
				// Para eventos de toque e scroll, usar passive por padrão
				if (this[0] && this[0].addEventListener) {
					var element = this[0];
					events.split(' ').forEach(function(event) {
						if (event.includes('touchstart') || event.includes('touchmove') || event.includes('wheel')) {
							element.addEventListener(event, handler || data, { passive: true });
						}
					});
					return this;
				}
			}
			return originalOn.call(this, events, selector, data, handler);
		};
	}
});


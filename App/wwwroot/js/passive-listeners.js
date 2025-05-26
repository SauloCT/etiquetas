// Configuração de passive event listeners para melhorar a performance
(function() {
    'use strict';
    
    // Lista de eventos que devem ser passive por padrão
    var passiveEvents = ['touchstart', 'touchmove', 'wheel', 'mousewheel'];
    
    // Override do addEventListener para tornar eventos específicos passive por padrão
    var originalAddEventListener = EventTarget.prototype.addEventListener;
    
    EventTarget.prototype.addEventListener = function(type, listener, options) {
        // Se o evento está na lista de eventos passive e não foi especificado explicitamente
        if (passiveEvents.includes(type) && typeof options !== 'object') {
            options = { passive: true };
        } else if (passiveEvents.includes(type) && typeof options === 'object' && options.passive === undefined) {
            options.passive = true;
        }
        
        return originalAddEventListener.call(this, type, listener, options);
    };
    
    // Configuração específica para jQuery
    if (typeof jQuery !== 'undefined') {
        // Interceptar eventos jQuery para torná-los passive quando apropriado
        var originalJQueryOn = jQuery.fn.on;
        
        jQuery.fn.on = function(events, selector, data, handler) {
            // Se é um evento de toque/scroll e não há seletor específico
            if (typeof events === 'string' && passiveEvents.some(event => events.includes(event))) {
                // Para elementos que já existem, adicionar listeners passive diretamente
                this.each(function() {
                    var element = this;
                    events.split(' ').forEach(function(eventType) {
                        if (passiveEvents.includes(eventType.trim())) {
                            var actualHandler = handler || data;
                            if (typeof actualHandler === 'function') {
                                element.addEventListener(eventType.trim(), actualHandler, { passive: true });
                            }
                        }
                    });
                });
                
                // Ainda chamar o método original para manter compatibilidade
                return originalJQueryOn.call(this, events, selector, data, handler);
            }
            
            return originalJQueryOn.call(this, events, selector, data, handler);
        };
    }
    
    // Configuração específica para mCustomScrollbar
    document.addEventListener('DOMContentLoaded', function() {
        // Aguardar um pouco para que o mCustomScrollbar seja inicializado
        setTimeout(function() {
            // Encontrar elementos com mCustomScrollbar e reconfigurar eventos
            var scrollbarElements = document.querySelectorAll('.mCustomScrollbar, .sidebar');
            
            scrollbarElements.forEach(function(element) {
                // Remover listeners antigos e adicionar novos como passive
                var events = ['touchstart', 'touchmove', 'wheel'];
                
                events.forEach(function(eventType) {
                    // Clonar o elemento para remover todos os event listeners
                    var newElement = element.cloneNode(true);
                    
                    // Adicionar event listeners passive
                    newElement.addEventListener(eventType, function(e) {
                        // Handler vazio passive
                    }, { passive: true });
                    
                    // Não substituir o elemento para não quebrar o mCustomScrollbar
                    // Apenas adicionar os listeners passive
                    element.addEventListener(eventType, function(e) {
                        // Handler passive adicional
                    }, { passive: true });
                });
            });
        }, 1000);
    });
    
    // Suprimir avisos específicos do console (opcional)
    var originalConsoleWarn = console.warn;
    console.warn = function(message) {
        // Filtrar avisos específicos de passive listeners
        if (typeof message === 'string' && 
            (message.includes('Added non-passive event listener') || 
             message.includes('passive event listener'))) {
            return; // Não mostrar estes avisos
        }
        return originalConsoleWarn.apply(console, arguments);
    };
    
})(); 
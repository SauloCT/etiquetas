/*! Backstretch - v2.0.4 - 2013-06-19 (Improved Version with Error Handling)
* http://srobbin.com/jquery-plugins/backstretch/
* Copyright (c) 2013 Scott Robbin; Licensed MIT 
* Improved version with input validation and error handling
*/
(function($, window, undefined) {
    'use strict';

    // Função de validação de entrada
    function validateInput(images) {
        if (!images || (Array.isArray(images) && images.length === 0)) {
            console.error('Backstretch: Nenhuma imagem foi fornecida');
            return false;
        }
        
        if (typeof images === 'string') {
            images = [images];
        }
        
        if (!Array.isArray(images)) {
            console.error('Backstretch: Formato de imagem inválido');
            return false;
        }
        
        // Validar cada URL de imagem
        for (var i = 0; i < images.length; i++) {
            if (typeof images[i] !== 'string' || images[i].trim() === '') {
                console.error('Backstretch: URL de imagem inválida no índice ' + i);
                return false;
            }
        }
        
        return true;
    }

    // Função para verificar se uma URL é válida
    function isValidUrl(url) {
        try {
            new URL(url);
            return true;
        } catch (e) {
            // Se não for uma URL absoluta, verificar se é um caminho relativo válido
            return /^[a-zA-Z0-9._\-\/]+\.(jpg|jpeg|png|gif|webp|svg)$/i.test(url);
        }
    }

    $.fn.backstretch = function(images, options) {
        try {
            // Validar entrada
            if (!validateInput(images)) {
                return this;
            }

            // Normalizar imagens para array
            if (typeof images === 'string') {
                images = [images];
            }

            // Validar URLs das imagens
            var validImages = [];
            for (var i = 0; i < images.length; i++) {
                if (isValidUrl(images[i])) {
                    validImages.push(images[i]);
                } else {
                    console.warn('Backstretch: URL de imagem inválida ignorada: ' + images[i]);
                }
            }

            if (validImages.length === 0) {
                console.error('Backstretch: Nenhuma imagem válida encontrada');
                return this;
            }

            // Verificar se o scroll está no topo
            if ($(window).scrollTop() === 0 && window.scrollTo) {
                window.scrollTo(0, 0);
            }

            return this.each(function() {
                try {
                    var $this = $(this);
                    var existing = $this.data('backstretch');

                    if (existing) {
                        if (typeof images === 'string' && typeof existing[images] === 'function') {
                            existing[images](options);
                            return;
                        }
                        options = $.extend(existing.options, options);
                        existing.destroy(true);
                    }

                    var instance = new Backstretch(this, validImages, options);
                    $this.data('backstretch', instance);
                } catch (e) {
                    console.error('Backstretch: Erro ao inicializar instância', e);
                }
            });
        } catch (e) {
            console.error('Backstretch: Erro geral', e);
            return this;
        }
    };

    $.backstretch = function(images, options) {
        try {
            return $('body').backstretch(images, options).data('backstretch');
        } catch (e) {
            console.error('Backstretch: Erro ao aplicar no body', e);
            return null;
        }
    };

    $.expr[':'].backstretch = function(element) {
        return $(element).data('backstretch') !== undefined;
    };

    $.fn.backstretch.defaults = {
        centeredX: true,
        centeredY: true,
        duration: 5000,
        fade: 0
    };

    var wrapperStyles = {
        left: 0,
        top: 0,
        overflow: 'hidden',
        margin: 0,
        padding: 0,
        height: '100%',
        width: '100%',
        zIndex: -999999
    };

    var imageStyles = {
        position: 'absolute',
        display: 'none',
        margin: 0,
        padding: 0,
        border: 'none',
        width: 'auto',
        height: 'auto',
        maxHeight: 'none',
        maxWidth: 'none',
        zIndex: -999999
    };

    function Backstretch(container, images, options) {
        try {
            this.options = $.extend({}, $.fn.backstretch.defaults, options || {});
            this.images = images;
            
            // Pré-carregar imagens com tratamento de erro
            var self = this;
            $.each(this.images, function(index, src) {
                try {
                    var img = new Image();
                    img.onerror = function() {
                        console.warn('Backstretch: Falha ao carregar imagem: ' + src);
                    };
                    img.src = src;
                } catch (e) {
                    console.warn('Backstretch: Erro ao pré-carregar imagem: ' + src, e);
                }
            });

            this.isBody = container === document.body;
            this.$container = $(container);
            this.$root = this.isBody ? (isOlderMobile ? $(window) : $(document)) : this.$container;

            var existingWrapper = this.$container.children('.backstretch').first();
            this.$wrap = existingWrapper.length ? 
                existingWrapper : 
                $('<div class="backstretch"></div>').css(wrapperStyles).appendTo(this.$container);

            if (!this.isBody) {
                var containerPosition = this.$container.css('position');
                var containerZIndex = this.$container.css('zIndex');
                
                this.$container.css({
                    position: containerPosition === 'static' ? 'relative' : containerPosition,
                    zIndex: containerZIndex === 'auto' ? 0 : containerZIndex,
                    background: 'none'
                });
                
                this.$wrap.css({
                    zIndex: -999998
                });
            }

            this.$wrap.css({
                position: this.isBody && isOlderMobile ? 'fixed' : 'absolute'
            });

            this.index = 0;
            this.show(this.index);

            $(window)
                .on('resize.backstretch', $.proxy(this.resize, this))
                .on('orientationchange.backstretch', $.proxy(function() {
                    if (this.isBody && window.pageYOffset === 0) {
                        window.scrollTo(0, 1);
                        this.resize();
                    }
                }, this));
        } catch (e) {
            console.error('Backstretch: Erro no construtor', e);
        }
    }

    Backstretch.prototype = {
        resize: function() {
            try {
                var imageOffset = {left: 0, top: 0};
                var rootWidth = this.isBody ? this.$root.width() : this.$root.innerWidth();
                var imageWidth = rootWidth;
                var rootHeight = this.isBody ? 
                    (window.innerHeight ? window.innerHeight : this.$root.height()) : 
                    this.$root.innerHeight();
                var imageHeight = imageWidth / this.$img.data('ratio');
                var heightDiff;

                if (imageHeight >= rootHeight) {
                    heightDiff = (imageHeight - rootHeight) / 2;
                    if (this.options.centeredY) {
                        imageOffset.top = '-' + heightDiff + 'px';
                    }
                } else {
                    imageHeight = rootHeight;
                    imageWidth = imageHeight * this.$img.data('ratio');
                    heightDiff = (imageWidth - rootWidth) / 2;
                    if (this.options.centeredX) {
                        imageOffset.left = '-' + heightDiff + 'px';
                    }
                }

                this.$wrap.css({width: rootWidth, height: rootHeight})
                    .find('img:not(.deleteable)')
                    .css({width: imageWidth, height: imageHeight})
                    .css(imageOffset);
            } catch (e) {
                console.error('Backstretch: Erro no redimensionamento', e);
            }
            return this;
        },

        show: function(index) {
            try {
                if (Math.abs(index) > this.images.length - 1) {
                    console.warn('Backstretch: Índice de imagem fora do intervalo: ' + index);
                    return this;
                }

                var self = this;
                var oldImages = self.$wrap.find('img').addClass('deleteable');
                var eventData = {relatedTarget: self.$container[0]};

                self.$container.trigger($.Event('backstretch.before', eventData), [self, index]);

                this.index = index;
                clearInterval(self.interval);

                self.$img = $('<img />')
                    .css(imageStyles)
                    .bind('load', function(e) {
                        try {
                            var naturalWidth = this.naturalWidth || $(e.target).width();
                            var naturalHeight = this.naturalHeight || $(e.target).height();

                            if (naturalWidth === 0 || naturalHeight === 0) {
                                console.error('Backstretch: Dimensões de imagem inválidas');
                                return;
                            }

                            $(this).data('ratio', naturalWidth / naturalHeight);

                            $(this).fadeIn(self.options.speed || self.options.fade, function() {
                                oldImages.remove();

                                if (!self.paused) {
                                    self.cycle();
                                }

                                $(['after', 'show']).each(function() {
                                    self.$container.trigger($.Event('backstretch.' + this, eventData), [self, index]);
                                });
                            });

                            self.resize();
                        } catch (e) {
                            console.error('Backstretch: Erro no carregamento da imagem', e);
                        }
                    })
                    .bind('error', function() {
                        console.error('Backstretch: Falha ao carregar imagem: ' + self.images[index]);
                        oldImages.remove();
                    })
                    .appendTo(self.$wrap);

                self.$img.attr('src', self.images[index]);
                return self;
            } catch (e) {
                console.error('Backstretch: Erro ao mostrar imagem', e);
                return this;
            }
        },

        next: function() {
            try {
                return this.show(this.index < this.images.length - 1 ? this.index + 1 : 0);
            } catch (e) {
                console.error('Backstretch: Erro ao avançar para próxima imagem', e);
                return this;
            }
        },

        prev: function() {
            try {
                return this.show(this.index === 0 ? this.images.length - 1 : this.index - 1);
            } catch (e) {
                console.error('Backstretch: Erro ao voltar para imagem anterior', e);
                return this;
            }
        },

        pause: function() {
            this.paused = true;
            return this;
        },

        resume: function() {
            this.paused = false;
            this.next();
            return this;
        },

        cycle: function() {
            try {
                if (this.images.length > 1) {
                    clearInterval(this.interval);
                    this.interval = setInterval($.proxy(function() {
                        if (!this.paused) {
                            this.next();
                        }
                    }, this), this.options.duration);
                }
            } catch (e) {
                console.error('Backstretch: Erro no ciclo de imagens', e);
            }
            return this;
        },

        destroy: function(preserveBackground) {
            try {
                $(window).off('resize.backstretch orientationchange.backstretch');
                clearInterval(this.interval);
                if (!preserveBackground) {
                    this.$wrap.remove();
                }
                this.$container.removeData('backstretch');
            } catch (e) {
                console.error('Backstretch: Erro ao destruir instância', e);
            }
        }
    };

    // Detecção de dispositivos móveis antigos
    var userAgent = navigator.userAgent;
    var platform = navigator.platform;
    var webkit = userAgent.match(/AppleWebKit\/([0-9]+)/);
    var webkitVersion = webkit && webkit[1];
    var fennec = userAgent.match(/Fennec\/([0-9]+)/);
    var fennecVersion = fennec && fennec[1];
    var operaMobile = userAgent.match(/Opera Mobi\/([0-9]+)/);
    var operaMobileVersion = operaMobile && operaMobile[1];
    var ie = userAgent.match(/MSIE ([0-9]+)/);
    var ieVersion = ie && ie[1];

    var isOlderMobile = !(
        (platform.indexOf('iPhone') > -1 || platform.indexOf('iPad') > -1 || platform.indexOf('iPod') > -1) && webkitVersion && webkitVersion < 534 ||
        window.operamini && ({}).toString.call(window.operamini) === '[object OperaMini]' ||
        operaMobile && operaMobileVersion < 7458 ||
        userAgent.indexOf('Android') > -1 && webkitVersion && webkitVersion < 533 ||
        fennec && fennecVersion < 6 ||
        'palmGetResource' in window && webkitVersion && webkitVersion < 534 ||
        userAgent.indexOf('MeeGo') > -1 && userAgent.indexOf('NokiaBrowser/8.5.0') > -1 ||
        ie && ieVersion <= 6
    );

})(jQuery, window); 
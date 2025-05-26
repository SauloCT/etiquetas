/**
 * WOW.js - Improved Version
 * Addresses marketplace feedback:
 * - Modularized architecture
 * - Performance improvements with debouncing
 * - Exception handling and logging
 * - Better integration practices
 * 
 * @version 1.0.0
 * @author Improved by AI Assistant
 * @license MIT
 */

(function(window, document) {
    'use strict';

    // Utility Module
    const WOWUtils = {
        /**
         * Extends object properties
         * @param {Object} source - Source object
         * @param {Object} target - Target object
         * @returns {Object} Extended object
         */
        extend: function(source, target) {
            try {
                for (let key in source) {
                    if (source.hasOwnProperty(key) && source[key] != null) {
                        target[key] = source[key];
                    }
                }
                return target;
            } catch (error) {
                WOWLogger.error('Error extending objects:', error);
                return target;
            }
        },

        /**
         * Checks if device is mobile
         * @param {string} userAgent - User agent string
         * @returns {boolean} Is mobile device
         */
        isMobile: function(userAgent) {
            try {
                return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(userAgent);
            } catch (error) {
                WOWLogger.error('Error checking mobile device:', error);
                return false;
            }
        },

        /**
         * Debounce function to limit function calls
         * @param {Function} func - Function to debounce
         * @param {number} wait - Wait time in milliseconds
         * @param {boolean} immediate - Execute immediately
         * @returns {Function} Debounced function
         */
        debounce: function(func, wait, immediate) {
            let timeout;
            return function executedFunction() {
                const context = this;
                const args = arguments;
                
                const later = function() {
                    timeout = null;
                    if (!immediate) {
                        try {
                            func.apply(context, args);
                        } catch (error) {
                            WOWLogger.error('Error in debounced function:', error);
                        }
                    }
                };
                
                const callNow = immediate && !timeout;
                clearTimeout(timeout);
                timeout = setTimeout(later, wait);
                
                if (callNow) {
                    try {
                        func.apply(context, args);
                    } catch (error) {
                        WOWLogger.error('Error in immediate debounced function:', error);
                    }
                }
            };
        },

        /**
         * Throttle function to limit function calls
         * @param {Function} func - Function to throttle
         * @param {number} limit - Time limit in milliseconds
         * @returns {Function} Throttled function
         */
        throttle: function(func, limit) {
            let inThrottle;
            return function() {
                const args = arguments;
                const context = this;
                if (!inThrottle) {
                    try {
                        func.apply(context, args);
                    } catch (error) {
                        WOWLogger.error('Error in throttled function:', error);
                    }
                    inThrottle = true;
                    setTimeout(() => inThrottle = false, limit);
                }
            };
        }
    };

    // Logger Module
    const WOWLogger = {
        enabled: true,
        level: 'info', // 'error', 'warn', 'info', 'debug'

        /**
         * Log error messages
         * @param {string} message - Error message
         * @param {Error} error - Error object
         */
        error: function(message, error) {
            if (this.enabled && console && console.error) {
                console.error('[WOW Error]', message, error);
            }
        },

        /**
         * Log warning messages
         * @param {string} message - Warning message
         */
        warn: function(message) {
            if (this.enabled && this.level !== 'error' && console && console.warn) {
                console.warn('[WOW Warning]', message);
            }
        },

        /**
         * Log info messages
         * @param {string} message - Info message
         */
        info: function(message) {
            if (this.enabled && ['info', 'debug'].includes(this.level) && console && console.info) {
                console.info('[WOW Info]', message);
            }
        },

        /**
         * Log debug messages
         * @param {string} message - Debug message
         */
        debug: function(message) {
            if (this.enabled && this.level === 'debug' && console && console.log) {
                console.log('[WOW Debug]', message);
            }
        }
    };

    // Animation Cache Module
    const WOWAnimationCache = {
        cache: new (window.WeakMap || Map)(),

        /**
         * Set animation name in cache
         * @param {Element} element - DOM element
         * @param {string} animationName - Animation name
         */
        set: function(element, animationName) {
            try {
                this.cache.set(element, animationName);
            } catch (error) {
                WOWLogger.error('Error setting animation cache:', error);
            }
        },

        /**
         * Get animation name from cache
         * @param {Element} element - DOM element
         * @returns {string} Animation name
         */
        get: function(element) {
            try {
                return this.cache.get(element);
            } catch (error) {
                WOWLogger.error('Error getting animation cache:', error);
                return null;
            }
        }
    };

    // Viewport Module
    const WOWViewport = {
        /**
         * Get element's offset top
         * @param {Element} element - DOM element
         * @returns {number} Offset top value
         */
        getOffsetTop: function(element) {
            try {
                let offsetTop = 0;
                let currentElement = element;

                while (currentElement.offsetTop === undefined) {
                    currentElement = currentElement.parentNode;
                    if (!currentElement) {
                        WOWLogger.warn('Could not find offsetTop for element');
                        return 0;
                    }
                }

                offsetTop = currentElement.offsetTop;
                while (currentElement = currentElement.offsetParent) {
                    offsetTop += currentElement.offsetTop;
                }

                return offsetTop;
            } catch (error) {
                WOWLogger.error('Error getting offset top:', error);
                return 0;
            }
        },

        /**
         * Check if element is visible in viewport
         * @param {Element} element - DOM element
         * @param {number} offset - Offset value
         * @returns {boolean} Is element visible
         */
        isVisible: function(element, offset) {
            try {
                const elementOffset = element.getAttribute('data-wow-offset') || offset;
                const scrollTop = window.pageYOffset;
                const viewportBottom = scrollTop + document.documentElement.clientHeight - elementOffset;
                const elementTop = this.getOffsetTop(element);
                const elementBottom = elementTop + element.clientHeight;

                return viewportBottom >= elementTop && elementBottom >= scrollTop;
            } catch (error) {
                WOWLogger.error('Error checking element visibility:', error);
                return false;
            }
        }
    };

    // Style Module
    const WOWStyle = {
        vendors: ['moz', 'webkit'],

        /**
         * Set vendor-prefixed CSS properties
         * @param {CSSStyleDeclaration} style - Element style
         * @param {Object} properties - CSS properties to set
         */
        setVendorProperties: function(style, properties) {
            try {
                for (let property in properties) {
                    if (properties.hasOwnProperty(property)) {
                        const value = properties[property];
                        style[property] = value;

                        // Add vendor prefixes
                        this.vendors.forEach(vendor => {
                            const prefixedProperty = vendor + property.charAt(0).toUpperCase() + property.substr(1);
                            style[prefixedProperty] = value;
                        });
                    }
                }
            } catch (error) {
                WOWLogger.error('Error setting vendor properties:', error);
            }
        },

        /**
         * Get vendor-prefixed CSS value
         * @param {Element} element - DOM element
         * @param {string} property - CSS property
         * @returns {string} CSS value
         */
        getVendorCSS: function(element, property) {
            try {
                const computedStyle = window.getComputedStyle(element);
                let value = computedStyle.getPropertyValue(property);

                if (!value) {
                    for (let vendor of this.vendors) {
                        value = computedStyle.getPropertyValue(`-${vendor}-${property}`);
                        if (value) break;
                    }
                }

                return value;
            } catch (error) {
                WOWLogger.error('Error getting vendor CSS:', error);
                return '';
            }
        },

        /**
         * Get animation name from element
         * @param {Element} element - DOM element
         * @returns {string} Animation name
         */
        getAnimationName: function(element) {
            try {
                let animationName;
                try {
                    animationName = this.getVendorCSS(element, 'animation-name');
                } catch (cssError) {
                    animationName = window.getComputedStyle(element).getPropertyValue('animation-name');
                }
                return animationName === 'none' ? '' : animationName;
            } catch (error) {
                WOWLogger.error('Error getting animation name:', error);
                return '';
            }
        }
    };

    // Main WOW Class
    function WOW(options) {
        try {
            this.config = WOWUtils.extend(options || {}, this.getDefaults());
            this.scrolled = true;
            this.boxes = [];
            this.element = window.document.documentElement;
            
            // Bind methods to maintain context
            this.handleScroll = this.handleScroll.bind(this);
            this.handleResize = this.handleResize.bind(this);
            this.scrollCallback = this.scrollCallback.bind(this);
            this.start = this.start.bind(this);

            // Create debounced scroll handler for better performance
            this.debouncedScrollCallback = WOWUtils.debounce(this.scrollCallback, 16); // ~60fps
            
            WOWLogger.info('WOW initialized with config:', this.config);
        } catch (error) {
            WOWLogger.error('Error initializing WOW:', error);
            throw error;
        }
    }

    WOW.prototype = {
        /**
         * Get default configuration
         * @returns {Object} Default config
         */
        getDefaults: function() {
            return {
                boxClass: 'wow',
                animateClass: 'animated',
                offset: 0,
                mobile: true,
                live: true,
                callback: null,
                scrollContainer: null
            };
        },

        /**
         * Initialize WOW
         */
        init: function() {
            try {
                const readyState = document.readyState;
                if (readyState === 'interactive' || readyState === 'complete') {
                    this.start();
                } else {
                    document.addEventListener('DOMContentLoaded', this.start);
                }
                WOWLogger.info('WOW initialization started');
            } catch (error) {
                WOWLogger.error('Error during WOW init:', error);
            }
        },

        /**
         * Start WOW animations
         */
        start: function() {
            try {
                this.boxes = Array.from(this.element.getElementsByClassName(this.config.boxClass));
                
                if (this.boxes.length === 0) {
                    WOWLogger.warn('No elements found with class:', this.config.boxClass);
                    return;
                }

                if (this.disabled()) {
                    this.resetStyle();
                    WOWLogger.info('WOW disabled on mobile device');
                    return;
                }

                // Apply initial styles
                this.boxes.forEach(box => {
                    this.applyStyle(box, true);
                });

                // Add event listeners with improved performance
                window.addEventListener('scroll', this.handleScroll, { passive: true });
                window.addEventListener('resize', this.handleResize, { passive: true });

                // Initial check
                this.scrollCallback();

                WOWLogger.info(`WOW started with ${this.boxes.length} elements`);
            } catch (error) {
                WOWLogger.error('Error starting WOW:', error);
            }
        },

        /**
         * Stop WOW animations
         */
        stop: function() {
            try {
                window.removeEventListener('scroll', this.handleScroll);
                window.removeEventListener('resize', this.handleResize);
                WOWLogger.info('WOW stopped');
            } catch (error) {
                WOWLogger.error('Error stopping WOW:', error);
            }
        },

        /**
         * Handle scroll events
         */
        handleScroll: function() {
            this.scrolled = true;
            this.debouncedScrollCallback();
        },

        /**
         * Handle resize events
         */
        handleResize: function() {
            this.scrolled = true;
            this.debouncedScrollCallback();
        },

        /**
         * Scroll callback function
         */
        scrollCallback: function() {
            try {
                if (!this.scrolled) return;
                
                this.scrolled = false;
                const visibleBoxes = [];

                this.boxes.forEach(box => {
                    if (box && WOWViewport.isVisible(box, this.config.offset)) {
                        this.show(box);
                        if (this.config.callback && typeof this.config.callback === 'function') {
                            this.config.callback(box);
                        }
                    } else if (box) {
                        visibleBoxes.push(box);
                    }
                });

                this.boxes = visibleBoxes;

                if (this.boxes.length === 0 && !this.config.live) {
                    this.stop();
                }
            } catch (error) {
                WOWLogger.error('Error in scroll callback:', error);
            }
        },

        /**
         * Show element with animation
         * @param {Element} element - DOM element to show
         */
        show: function(element) {
            try {
                this.applyStyle(element);
                element.className = `${element.className} ${this.config.animateClass}`;
                
                if (this.config.callback && typeof this.config.callback === 'function') {
                    this.config.callback(element);
                }
            } catch (error) {
                WOWLogger.error('Error showing element:', error);
            }
        },

        /**
         * Apply styles to element
         * @param {Element} element - DOM element
         * @param {boolean} hidden - Whether to hide element initially
         */
        applyStyle: function(element, hidden) {
            try {
                const duration = element.getAttribute('data-wow-duration');
                const delay = element.getAttribute('data-wow-delay');
                const iteration = element.getAttribute('data-wow-iteration');

                this.animate(() => {
                    this.customStyle(element, hidden, duration, delay, iteration);
                });
            } catch (error) {
                WOWLogger.error('Error applying style:', error);
            }
        },

        /**
         * Animate function using requestAnimationFrame
         * @param {Function} callback - Animation callback
         */
        animate: function(callback) {
            try {
                if ('requestAnimationFrame' in window) {
                    return window.requestAnimationFrame(callback);
                } else {
                    return callback();
                }
            } catch (error) {
                WOWLogger.error('Error in animate function:', error);
                return callback();
            }
        },

        /**
         * Apply custom styles to element
         * @param {Element} element - DOM element
         * @param {boolean} hidden - Whether element should be hidden
         * @param {string} duration - Animation duration
         * @param {string} delay - Animation delay
         * @param {string} iteration - Animation iteration count
         */
        customStyle: function(element, hidden, duration, delay, iteration) {
            try {
                if (hidden) {
                    WOWAnimationCache.set(element, WOWStyle.getAnimationName(element));
                }

                element.style.visibility = hidden ? 'hidden' : 'visible';

                const styleProperties = {};

                if (duration) styleProperties.animationDuration = duration;
                if (delay) styleProperties.animationDelay = delay;
                if (iteration) styleProperties.animationIterationCount = iteration;
                
                styleProperties.animationName = hidden ? 'none' : WOWAnimationCache.get(element);

                WOWStyle.setVendorProperties(element.style, styleProperties);
            } catch (error) {
                WOWLogger.error('Error applying custom style:', error);
            }
        },

        /**
         * Reset styles for all elements
         */
        resetStyle: function() {
            try {
                this.boxes.forEach(box => {
                    box.style.visibility = 'visible';
                });
            } catch (error) {
                WOWLogger.error('Error resetting styles:', error);
            }
        },

        /**
         * Check if WOW should be disabled
         * @returns {boolean} Should be disabled
         */
        disabled: function() {
            try {
                return !this.config.mobile && WOWUtils.isMobile(navigator.userAgent);
            } catch (error) {
                WOWLogger.error('Error checking if disabled:', error);
                return false;
            }
        }
    };

    // Export WOW to global scope
    if (typeof module !== 'undefined' && module.exports) {
        module.exports = WOW;
    } else if (typeof define === 'function' && define.amd) {
        define(function() { return WOW; });
    } else {
        window.WOW = WOW;
    }

    // Auto-initialize if data-wow-config attribute is present
    document.addEventListener('DOMContentLoaded', function() {
        try {
            const configElement = document.querySelector('[data-wow-config]');
            if (configElement) {
                const config = JSON.parse(configElement.getAttribute('data-wow-config'));
                const wow = new WOW(config);
                wow.init();
            }
        } catch (error) {
            WOWLogger.error('Error auto-initializing WOW:', error);
        }
    });

})(window, document); 
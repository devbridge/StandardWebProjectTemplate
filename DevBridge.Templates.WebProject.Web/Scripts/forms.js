/*jslint vars: true */
/*global define, alert*/

define(['jquery'], function ($) {
    'use strict';

    var console = window.console || { log: $.noop },
        options = {},
        defaults = {
            selector: '.f-ajax',
            errorSelector: '.f-ajax-errors',
            classWhileSubmitting: 'f-submitting',
            defaultMessage: 'Failed to process request. Please try again later.'
        };

    var processForm = function (e) {
        e.preventDefault();
        console.log('Submiting form');

        var submitting = 'submitting',
            status = 'status',
            form = $(this),
            errors = form.find('.f-ajax-errors');

        if (form.data(status) === submitting) {
            console.log('Cancel submission, form is already being processed.');
            return false;
        }

        form.data(status, submitting);
        form.addClass(options.classWhileSubmitting);
        errors.hide().empty();

        // Exit if form is not valid:
        if (typeof form.valid === "function" && !form.valid()) {
            return false;
        }

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            dataType: 'json'
        }).done(function (data) {
            if (data.success) {
                if (data.redirectUrl) {
                    window.location = data.redirectUrl;
                    return;
                }
            } else {
                errors.html(data.message || options.defaultMessage).fadeIn();
                console.log(data.message || options.defaultMessage);
            }

            form.data(status, null);
            form.removeClass(options.classWhileSubmitting);
        }).fail(function (xhr) {
            form.data(status, null);
            form.removeClass(options.classWhileSubmitting);
            errors.html('Failed to get response from the server.').fadeIn();
            console.log('Failed: ', xhr);
        });

        return true;
    };

    return {
        init: function (opts) {
            console.log('Initializing forms module');
            options = $.extend(defaults, opts);
            $(document).on('submit', options.selector, processForm);
        }
    };
});
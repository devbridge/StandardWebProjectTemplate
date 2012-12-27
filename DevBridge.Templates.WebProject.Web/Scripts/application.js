/*jslint regexp: true, nomen: true, sloppy: true */
/*global require, alert */

require.config({
    paths: {
        jquery: 'http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min'
    }
});

// This is application entry point.
require(['jquery', 'jquery.modal'], function ($) {
    'use strict';

    var console = window.console || { log: function () { } };

    console.log('Application loaded.');
    console.log('jQuery version: ' + $.fn.jquery);
    console.log('Modal version: ' + $.Modal.version);

    // $('#debug').html('Scripts loaded');

    // Every element marked with "modal" class will open modal dialog:
    $(document).on('click', '.modal', function (e) {
        e.preventDefault();
        $(this).openModal();
    });
});
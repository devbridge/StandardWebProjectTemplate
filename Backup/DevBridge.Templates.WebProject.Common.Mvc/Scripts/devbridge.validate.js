(function ($) {

	function getModelPrefix(fieldName) {
		return fieldName.substr(0, fieldName.lastIndexOf('.') + 1);
	}

	function appendModelPrefix(value, prefix) {
		if (value.indexOf('*.') === 0) {
			value = value.replace('*.', prefix);
		}
		return value;
	}

	function getModelElement(options, paramName, prefix) {
		var elementName = appendModelPrefix(options.params[paramName], prefix);
		var elements = $(options.form).find(':input[name="' + elementName + '"]');
		var type = elements.attr('type');
		if (elements.length > 0 && (/^radio$/i.test(type) || /^checkbox$/i.test(type))) {
			return function () {
				return elements.filter(':checked')[0];
			};
		}

		return function () {
			return elements[0];
		};
	}
	
	function setValidationValues(options, ruleName, value) {
		options.rules[ruleName] = value;
		if (options.message) {
			options.messages[ruleName] = options.message;
		}
	}
	
	// CompareWithEndDate
	$.validator.addMethod('comparewithenddate',
		function (value, element, param) {
			if (this.optional(element) || this.optional(param) || (value == '')) {
				return true;
			}
			var startDate = $.datepicker.parseDate($.datepicker._defaults.dateFormat, value);
			var endDate = $.datepicker.parseDate($.datepicker._defaults.dateFormat, $(param).val());
			return startDate <= endDate;
		});

	$.validator.unobtrusive.adapters.add('comparewithenddate', ['propertyenddate'], function (options) {
		var prefix = getModelPrefix(options.element.name),
			compareWithElement = getModelElement(options, 'propertyenddate', prefix);

		setValidationValues(options, 'comparewithenddate', compareWithElement);
	});
})(jQuery);
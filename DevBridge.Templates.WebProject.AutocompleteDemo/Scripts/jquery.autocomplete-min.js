/**
*  Ajax Autocomplete for jQuery, version 1.1.5
*  (c) 2010 Tomas Kirda, Vytautas Pranskunas
*
*  Ajax Autocomplete for jQuery is freely distributable under the terms of an MIT-style license.
*  For details, see the web site: http://www.devbridge.com/projects/autocomplete/jquery/
*
*  Last Review: 07/24/2012
*/

(function (d) {
	function j(b, a, c) { a = "(" + c.replace(k, "\\$1") + ")"; return b.replace(RegExp(a, "gi"), "<strong>$1</strong>") } function g(b, a) {
		this.el = d(b); this.el.attr("autocomplete", "off"); this.suggestions = []; this.data = []; this.badQueries = []; this.selectedIndex = -1; this.currentValue = this.el.val(); this.intervalId = 0; this.cachedResponse = []; this.onChange = this.onChangeInterval = null; this.ignoreValueChange = !1; this.serviceUrl = a.serviceUrl; this.isLocal = !1; this.options = { autoSubmit: !1, minChars: 1, maxHeight: 300, deferRequestBy: 0,
			width: 0, highlight: !0, params: {}, fnFormatResult: j, delimiter: null, zIndex: 9999
		}; this.initialize(); this.setOptions(a); this.el.data("autocomplete", this)
	} var k = RegExp("(\\/|\\.|\\*|\\+|\\?|\\||\\(|\\)|\\[|\\]|\\{|\\}|\\\\)", "g"); d.fn.autocomplete = function (b, a) { var c; if ("string" == typeof b) { if (c = this.data("autocomplete"), "function" == typeof c[b]) c[b](a) } else c = new g(this.get(0) || d("<input />"), b); return c }; g.prototype = { killerFn: null, initialize: function () {
		var b, a, c; b = this; a = Math.floor(1048576 * Math.random()).toString(16);
		c = "Autocomplete_" + a; this.killerFn = function (a) { 0 === d(a.target).parents(".autocomplete").size() && (b.killSuggestions(), b.disableKillerFn()) }; this.options.width || (this.options.width = this.el.width()); this.mainContainerId = "AutocompleteContainter_" + a; d('<div id="' + this.mainContainerId + '" style="position:absolute;z-index:9999;"><div class="autocomplete-w1"><div class="autocomplete" id="' + c + '" style="display:none; width:300px;"></div></div></div>').appendTo("body"); this.container = d("#" + c); this.fixPosition();
		window.opera ? this.el.keypress(function (a) { b.onKeyPress(a) }) : this.el.keydown(function (a) { b.onKeyPress(a) }); this.el.keyup(function (a) { b.onKeyUp(a) }); this.el.blur(function () { b.enableKillerFn() }); this.el.focus(function () { b.fixPosition() }); this.el.change(function () { b.onValueChanged() })
	}, extendOptions: function (b) { d.extend(this.options, b) }, setOptions: function (b) {
		var a = this.options; this.extendOptions(b); if (a.lookup || a.isLocal) this.isLocal = !0, d.isArray(a.lookup) && (a.lookup = { suggestions: a.lookup, data: [] });
		d("#" + this.mainContainerId).css({ zIndex: a.zIndex }); this.container.css({ maxHeight: a.maxHeight + "px", width: a.width })
	}, clearCache: function () { this.cachedResponse = []; this.badQueries = [] }, disable: function () { this.disabled = !0 }, enable: function () { this.disabled = !1 }, fixPosition: function () { var b = this.el.offset(); d("#" + this.mainContainerId).css({ top: b.top + this.el.innerHeight() + "px", left: b.left + "px" }) }, enableKillerFn: function () { d(document).bind("click", this.killerFn) }, disableKillerFn: function () {
		d(document).unbind("click",
this.killerFn)
	}, killSuggestions: function () { var b = this; this.stopKillSuggestions(); this.intervalId = window.setInterval(function () { b.hide(); b.stopKillSuggestions() }, 300) }, stopKillSuggestions: function () { window.clearInterval(this.intervalId) }, onValueChanged: function () { this.change(this.selectedIndex) }, onKeyPress: function (b) {
		if (!this.disabled && this.enabled) {
			switch (b.keyCode) {
				case 27: this.el.val(this.currentValue); this.hide(); break; case 9: case 13: if (-1 === this.selectedIndex) { this.hide(); return } this.select(this.selectedIndex);
					if (9 === b.keyCode) return; break; case 38: this.moveUp(); break; case 40: this.moveDown(); break; default: return
			} b.stopImmediatePropagation(); b.preventDefault()
		}
	}, onKeyUp: function (b) { if (!this.disabled) { switch (b.keyCode) { case 38: case 40: return } clearInterval(this.onChangeInterval); if (this.currentValue !== this.el.val()) if (0 < this.options.deferRequestBy) { var a = this; this.onChangeInterval = setInterval(function () { a.onValueChange() }, this.options.deferRequestBy) } else this.onValueChange() } }, onValueChange: function () {
		clearInterval(this.onChangeInterval);
		this.currentValue = this.el.val(); var b = this.getQuery(this.currentValue); this.selectedIndex = -1; this.ignoreValueChange ? this.ignoreValueChange = !1 : "" === b || b.length < this.options.minChars ? this.hide() : this.getSuggestions(b)
	}, getQuery: function (b) { var a; a = this.options.delimiter; if (!a) return d.trim(b); b = b.split(a); return d.trim(b[b.length - 1]) }, getSuggestionsLocal: function (b) {
		var a, c, d, f, e; c = this.options.lookup; d = c.suggestions.length; a = { suggestions: [], data: [] }; b = b.toLowerCase(); for (e = 0; e < d; e++) f = c.suggestions[e],
0 === f.toLowerCase().indexOf(b) && (a.suggestions.push(f), a.data.push(c.data[e])); return a
	}, getSuggestions: function (b) { var a, c; (a = this.isLocal ? this.getSuggestionsLocal(b) : this.cachedResponse[b]) && d.isArray(a.suggestions) ? (this.suggestions = a.suggestions, this.data = a.data, this.suggest()) : this.isBadQuery(b) || (c = this, c.options.params.query = b, d.get(this.serviceUrl, c.options.params, function (a) { c.processResponse(a) }, "text")) }, isBadQuery: function (b) {
		for (var a = this.badQueries.length; a--; ) if (0 === b.indexOf(this.badQueries[a])) return !0;
		return !1
	}, hide: function () { this.enabled = !1; this.selectedIndex = -1; this.container.hide() }, suggest: function () {
		if (0 === this.suggestions.length) this.hide(); else {
			var b, a, c, h, f, e, g, i; b = this; a = this.suggestions.length; h = this.options.fnFormatResult; f = this.getQuery(this.currentValue); g = function (a) { return function () { b.activate(a) } }; i = function (a) { return function () { b.select(a) } }; this.container.hide().empty(); for (e = 0; e < a; e++) c = this.suggestions[e], c = d((b.selectedIndex === e ? '<div class="selected"' : "<div") + ' title="' +
c + '">' + h(c, this.data[e], f) + "</div>"), c.mouseover(g(e)), c.click(i(e)), this.container.append(c); this.enabled = !0; this.container.show()
		}
	}, processResponse: function (b) { var a; try { a = eval("(" + b + ")") } catch (c) { return } d.isArray(a.data) || (a.data = []); this.options.noCache || (this.cachedResponse[a.query] = a, 0 === a.suggestions.length && this.badQueries.push(a.query)); a.query === this.getQuery(this.currentValue) && (this.suggestions = a.suggestions, this.data = a.data, this.suggest()) }, activate: function (b) {
		var a, c; a = this.container.children();
-1 !== this.selectedIndex && a.length > this.selectedIndex && d(a.get(this.selectedIndex)).removeClass(); this.selectedIndex = b; -1 !== this.selectedIndex && a.length > this.selectedIndex && (c = a.get(this.selectedIndex), d(c).addClass("selected")); return c
	}, deactivate: function (b, a) { b.className = ""; this.selectedIndex === a && (this.selectedIndex = -1) }, select: function (b) {
		var a; if (a = this.suggestions[b]) this.el.val(a), this.options.autoSubmit && (a = this.el.parents("form"), 0 < a.length && a.get(0).submit()), this.ignoreValueChange = !0,
this.hide(), this.onSelect(b)
	}, change: function (b) { if (this.suggestions[b]) { var a, c; a = this.suggestions[b]; c = this.data[b]; this.el.val(this.getValue(a)) } else a = "", c = -1; b = this.options.onChange; d.isFunction(b) && b(a, c, this.el) }, moveUp: function () { -1 !== this.selectedIndex && (0 === this.selectedIndex ? (this.container.children().get(0).className = "", this.selectedIndex = -1, this.el.val(this.currentValue)) : this.adjustScroll(this.selectedIndex - 1)) }, moveDown: function () {
		this.selectedIndex !== this.suggestions.length - 1 && this.adjustScroll(this.selectedIndex +
1)
	}, adjustScroll: function (b) { var a, c, d; a = this.activate(b).offsetTop; c = this.container.scrollTop(); d = c + this.options.maxHeight - 25; a < c ? this.container.scrollTop(a) : a > d && this.container.scrollTop(a - this.options.maxHeight + 25); this.el.val(this.getValue(this.suggestions[b])) }, onSelect: function (b) { var a, c; a = this.options.onSelect; c = this.suggestions[b]; b = this.data[b]; this.el.val(this.getValue(c)); d.isFunction(a) && a(c, b, this.el) }, getValue: function (b) {
		var a, c; a = this.options.delimiter; if (!a) return b; c = this.currentValue;
		a = c.split(a); return 1 === a.length ? b : c.substr(0, c.length - a[a.length - 1].length) + b
	}
	}
})(jQuery);



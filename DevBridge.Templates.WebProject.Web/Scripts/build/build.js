// Build configuration guide can be found at: https://github.com/jrburke/r.js/blob/master/build/example.build.js

({
    name: 'application',
    baseUrl: '../',
    optimize: 'uglify2',
    exclude: ['jquery'],
    paths: {
        requireLib: 'require'
    },
    mainConfigFile: '../application.js',
    out: '../application.min.js',
    // A function that if defined will be called for every file read in the
    // build that is done to trace JS dependencies.
    // Remove references to console.log(...)
    onBuildRead: function (moduleName, path, contents){
        return contents.replace(/console.log(.*);/g, '');
    }
})

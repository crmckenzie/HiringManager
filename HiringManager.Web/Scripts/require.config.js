require.config({
    baseUrl: window.baseSiteUrl + "scripts",
    "paths": {
        "jquery": "https://code.jquery.com/jquery-2.1.1.min",
        "jquery-ui": "jquery-ui-1.10.3",
        "bootstrap": 'http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min',
        "kendo": "http://cdn.kendostatic.com/2014.2.716/js/kendo.all.min",
        "knockout": 'http://cdnjs.cloudflare.com/ajax/libs/knockout/3.1.0/knockout-min',
        "PubSub": '//cdn.jsdelivr.net/pubsubjs/1.4.2/pubsub.min',
        'underscore': '//cdnjs.cloudflare.com/ajax/libs/underscore.js/1.6.0/underscore-min',
        "zenbox": "http://asset0.zendesk.com/external/zenbox/v2.6/zenbox",
    },
    shim: {
        'bootstrap': {
            deps: ['jquery']
        },
        "jquery-ui": { deps: ['jquery'], exports: "$" },
        "kendo": {
            deps: ['jquery']
        },
        knockout: { exports: 'ko' },
        PubSub: { exports: 'PubSub' },
        underscore: { deps: ['jquery'], exports: '_' },
    },
    waitSeconds: 30
    //deps: ["jquery", "knockout", "kendo", 'bootstrap'],
});
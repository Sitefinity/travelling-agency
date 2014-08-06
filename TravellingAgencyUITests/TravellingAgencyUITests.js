var fs = require('fs');

function setPageViewportAndBackground() {
    casper.viewport(1600, 1200);

    casper.evaluate(function () {
        document.querySelector('body').style.backgroundColor = 'white';
    });
}

casper.test.begin('Tests homepage', 1, function suite(test) {

    casper.start();

    casper.then(function () {
        test.assertEquals(1, 1);
    });

    casper.run(function () {
        test.done();
    });
});
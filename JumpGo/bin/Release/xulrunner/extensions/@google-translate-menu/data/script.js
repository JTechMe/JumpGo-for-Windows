self.on(
    'context',
    function () {
        var text = window.getSelection().toString();
            if (text.length > 20) {
                text = text.substr(0, 20) + '...';
            }
        return 'Translate "' + text + '"';
    }
);

self.on("click", self.postMessage);

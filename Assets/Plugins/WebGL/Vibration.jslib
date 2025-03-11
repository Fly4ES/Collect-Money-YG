mergeInto(LibraryManager.library, {
    Vibrate: function(duration) {
        if ("vibrate" in navigator) {
            navigator.vibrate(duration);
        }
    }
});
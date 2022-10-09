$(document).ready(function () {
    var controls = {
        video: $("#myvideo"),
        playpause: $("#play-pause"),
        progress: $("#fader"),
        duration: $("#duration"),
        currentTime: $("#currenttime"),
        hasHours: false
    };

    var video = controls.video[0];
    var imgbutton = $("#play-pause-img");

    controls.playpause.click(function () {
        if (video.paused) {
            video.play();
            console.log("button to play clicked");
            imgbutton.attr("src", "/img/pause.png")
        } else {
            video.pause();
            console.log("button to pause clicked");
            imgbutton.attr("src", "/img/play-button.png");
        }

    });
    video.addEventListener("ended", function () {
        video.pause();
        console.log("button to pause clicked");
        imgbutton.attr("src", "/img/play-button.png");
    });
    video.addEventListener("canplay", function () {
        controls.hasHours = (video.duration / 3600) >= 1.0;
        controls.duration.text(formatTime(video.duration, controls.hasHours));
        controls.currentTime.text(formatTime(0), controls.hasHours)
    }, false);
    function formatTime(time, hours) {
        if (hours) {
            var h = Math.floor(time / 3600);
            time = time - h * 3600;

            var m = Math.floor(time / 60);
            var s = Math.floor(time % 60);

            return h.lead0(2) + ":" + m.lead0(2) + ":" + s.lead0(2);
        } else {
            var m = Math.floor(time / 60);
            var s = Math.floor(time % 60);

            return m.lead0(2) + ":" + s.lead0(2);
        }
    }
    Number.prototype.lead0 = function (n) {
        var nz = "" + this;
        while (nz.length < n) {
            nz = "0" + nz;
        }
        return nz;
    };

    video.addEventListener("timeupdate", function () {
        controls.currentTime.text(formatTime(video.currentTime, controls.hasHours));
        var progress = (Math.floor(video.currentTime) / Math.floor(video.duration)) * 10000;
        controls.progress.attr("value", progress);

    });
});



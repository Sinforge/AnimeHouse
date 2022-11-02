$(document).ready(function () {
    var controls = {
        video: $("#myvideo"),
        playpause: $("#play-pause"),
        progress: $("#fader"),
        duration: $("#duration"),
        currentTime: $("#currenttime"),
        zoom: $("#fullmode"),
        volume: $("#volume"),
        hasHours: false
    };

    var video = controls.video[0];
    video.addEventListener("canplaythrough", () => controls.progress.attr("max", Math.floor(video.duration) ));
    controls.playpause.click(function () {
        if (video.paused) {
            video.play();
            console.log("button to play clicked");
            controls.playpause.attr("src", "/img/pause.png");

        } else {
            video.pause();
            console.log("button to pause clicked");
            controls.playpause.attr("src", "/img/play-buttton.png");
        }

    });
    controls.zoom.click(function() {
        if (video.requestFullscreen) {
            video.requestFullscreen();
        }
    });
    video.addEventListener("ended", function () {
        video.pause();
        console.log("button to pause clicked");
        controls.playpause.attr("src", "/img/play-buttton.png");
    });
    controls.volume.click(function() {
        if (video.volume !== 0) {
            video.volume = 0;
        } else {
            video.volume = 1.0;
        }
    });
    video.addEventListener("canplay", function () {
        controls.hasHours = (video.duration / 3600) >= 1.0;
        controls.duration.text(formatTime(video.duration, controls.hasHours));
        controls.currentTime.text(formatTime(0), controls.hasHours);
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
        var progress = video.currentTime;
        document.getElementById("fader").value = progress;
    });

    controls.progress.change(function () {
        video.currentTime = document.getElementById("fader").value;
        controls.currentTime.text(formatTime(video.currentTime, controls.hasHours));
    });
    
});




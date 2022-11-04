$(document).ready(function () {
    var controls = {
        video: $("#myvideo"),
        playpause: $("#play-pause"),
        progress: $("#fader"),
        duration: $("#duration"),
        currentTime: $("#currenttime"),
        zoom: $("#fullmode"),
        volume: $("#volume"),
        volume_mixer: $("#volume-mixer"),
        volume_block: $("#volume-block"),
        hasHours: false
    };
    var controlsBlock = $("#controlls");
    var videoPlayer = $(".player")

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
    var fullscreen = 0;
    controls.zoom.click(function() {
        if (fullscreen == 0) {
            fullscreen = 1;
            controls.video.appendTo('body');
            controls.video.css('position', 'absolute').css('width', "100%").css('height', '100%').css('z-index', 600);
        } else {
            fullscreen = 0;
            videoPlayer.css('position', 'relative').css('width', '700px').css('height', '400px');


        }
    });
    /*
    .player {
        overflow: visible;
    }

    .video - player - block.player {
        max - width: 850px;
        text - align: center;
        width: 100 %;
    }

    .video - player - block.player.video {
        max - width: 100 %;
        width: 700px;
        height: 400px;
    }


    .video - player - block.controlls #play - pause {
        color: black;
    }
    #controlls {
        display: flex;
        flex - direction: column;
        align - items: center;
        position: relative;
        top: -80px;
        margin - left: 25px;
        margin - right: 20px;
        background - color: transparent;
        padding: 10px 10px 10px 10px;
    }*/
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
    controls.volume_mixer.change(function () {
        console.log("Im change volume");
        video.volume = this.value / 100;


    });
    controls.volume_block.mouseover(function() {
        console.log("Show range volume");
        controls.volume_mixer.css("display", "block");
    });
    controls.volume_block.mouseout(function() {
        console.log("Hide range volume");
        controls.volume_mixer.css("display", "none");
    });

});




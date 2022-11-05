﻿


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
    var videoPlayer = $(".player");

    document.querySelector('#fullmode').addEventListener('click', toggleScreen);
    let fullscreenVideo = 0;
    let player1 = document.querySelector('.player');
    let video1 = document.querySelector('.video');
    function toggleScreen() {


        if (fullscreenVideo == 0) {
            console.log("Im in fullscreen");
            fullscreenVideo = 1;

            video1.style.width = '100%';
            video1.style.height = '100%';
            player1.style.position = 'absolute';
            player1.style.width = '100%';
            player1.style.height = '100%';
            if (player1.requestFullscreen) {
                player1.requestFullscreen();
            }
            else if (player1.mozRequestFullScreen) {
                player1.mozRequestFullScreen();
            }
            else if (player1.webkitRequestFullScreen) {
                player1.webkitRequestFullScreen();
            }
            else if (player1.msRequestFullscreen) {
                player1.msRequestFullscreen();
            }
        } else {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            }
            else if (document.webkitCancelFullScreen) {
                document.webkitCancelFullScreen();
            }
            else if (document.msExitFullScreen) {
                document.msExitFullscreen();
            }
            console.log("Im not in fullscreen");
            fullscreenVideo = 0;
            video1.style.width = '';
            video1.style.height = '';
            player1.style.position = '';
            player1.style.width = '';
            player1.style.height = '';

        }



    }
    function ChangeVideo(id) {
        let video = document.getElementById("myvideo");
        let newId = id + '.';
        video.src = video.src.replace(/\d+\./, newId);
        console.log(video.src);
    }


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
    /*controls.zoom.click(function() {
        /*if (fullscreen == 0) {
            fullscreen = 1;
            controls.video.appendTo('body');
            $('body').css('overflow', 'hidden');
            controls.video.css('position', 'fixed').css('right', 0).css('bottom', 0).css('min-width', '100%')
                .css('min-heigth', '100%').css('width', 'auto')
                .css('height', 'auto').css('z-index', '1000').css('background-size', 'cover')
                .css('top', '');
        } else {
            fullscreen = 0;
            videoPlayer.css('position', 'relative').css('width', '700px').css('height', '400px');


        }
        let element = document.getElementsByClassName("video-player-block");
        fullscreen(element);
}); */

    /*
     video {
    position: fixed; right: 0; bottom: 0;
    min-width: 100%; min-height: 100%;
    width: auto; height: auto; z-index: -100;
    background: url(polina.jpg) no-repeat;
    background-size: cover;
}
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




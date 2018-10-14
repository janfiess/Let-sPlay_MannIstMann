/** 
 * Audience interaction interface for live performances
 * Developed by Jan Fiess in March 2018
 * Filmakademie Baden-Württemberg
 * mail@janfiess.com
*/


var express = require('express');
var app = express();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var port = process.env.PORT || 3000;
var vote_domenico = 0, vote_alex = 0;
var countdown = 16;
var isStopwatchRunnning = false;


// include external data, eg. css, images (public folder)
app.use(express.static(__dirname + '/public'));

// routing
app.get('/', function (req, res) {
  res.redirect('/index.html'); // alternative: res.sendfile('./public/index.html');
});

app.get('/show', function (req, res) {
  res.redirect('/showcontroller.html'); // alternative: res.sendfile('./public/showcontroller.html');
});

app.get('/d', function (req, res) {
  res.redirect('/ddd/fiddle.html'); 
});




var currentMode = 'startMode';
var currentQuestion;

/**
 * This app has several modes:
 * 1. start mode (shown before the show starts)
 * 2. vote mode (shown when the audience is asked to vote)
 * 3. restTime mode (shown when the user has already voted, but is still in time)
 * 4. standby mode (shown between the questions)
 * 5. end mode (shown when the show has ended)
 */


// Socket.io
io.on('connection', function (socket) {

  // user (dis)connected
  console.log('a user connected');
  socket.emit(currentMode, currentQuestion);
  socket.on('disconnect', function () {
    console.log('user disconnected');
  });

  // 5 app modes
  // startMode (1)
  socket.on('startMode', function () {
    startMode();
  });

  function startMode() {
    console.log("Start Mode");
    io.emit('startMode');
    currentMode = "startMode";
  }

  // voteMode (2)
  socket.on('voteMode', function (question) {
    voteMode(question);
  });

  function voteMode(question) {
    console.log("Vote Mode: " + question);
    vote_domenico = vote_alex = 0;
    currentQuestion = question;
    io.emit('voteMode', question);
    currentMode = "voteMode";
    countdown = 8;
    if(isStopwatchRunnning == false){
      isStopwatchRunnning = true;
      setInterval(runStopwatch, 1000);
    }
  }



  function runStopwatch() {
    if (countdown >= 0) countdown--;

    if (countdown >= 0) {
      console.log(countdown);
      io.emit('stopwatch', countdown);
    }
    if (countdown == 0) {
      setTimeout(() => {
        
        clearInterval(this);
        evaluateVotes();
        isStopwatchRunnning = false;
        standbyMode();
      }, 1000);
    }
  }

  function evaluateVotes(){
    console.log("Domenico: " + vote_domenico + " | Alex: " + vote_alex);
  }

  // incoming vote message
  socket.on('vote', function (msg) {
    console.log(msg);
    if (msg == "d") vote_domenico++;
    else if (msg == "a") vote_alex++;
    console.log("incoming vote: " + msg);
  });


  // standbyMode (4)
  socket.on('standbyMode', function () {
    standbyMode();
  });

  function standbyMode() {
    console.log("Standby Mode");
    io.emit('standbyMode');
    currentMode = "standbyMode";
  }

  // endMode (5)
  socket.on('endMode', function () {
    endMode();
  });

  function endMode() {
    console.log("End Mode");
    io.emit('endMode');
    currentMode = "endMode";
  }





});

// put application on server
http.listen(port, function () {
  console.log('listening on port ' + port);
});

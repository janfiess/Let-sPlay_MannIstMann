var express = require('express');
var app = express();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var port = process.env.PORT || 3000;
var vote_yes = 0, vote_no = 0;

// include external data, eg. css, images (public folder)
app.use(express.static(__dirname + '/public'));

app.get('/', function(req, res){
  res.redirect('/index.html'); // alternative: res.sendfile('./public/html/index.html');
  
});

io.on('connection', function(socket){

  // user (dis)connected
  console.log('a user connected');
  socket.on('disconnect', function(){
      console.log('user disconnected');
  });

  // incoming chat message
  socket.on('vote', function(msg){
    console.log(msg);
    if(msg == "yes") vote_yes++;
    else if(msg == "no") vote_no++;
    console.log("yes: " + vote_yes + " | no: " + vote_no);
    io.emit('chat message', msg);
  });
});

http.listen(port, function(){
  console.log('listening on port ' + port);
});

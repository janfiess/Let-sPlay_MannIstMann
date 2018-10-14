var mqtt = require('mqtt');

var client  = mqtt.connect('mqtt://test.mosquitto.org');
 
client.on('connect', function () {
  client.subscribe('janfiess/#');
  client.publish('janfiess', 'hello from Node.js');
})
 
client.on('message', function (topic, message) {
  // message is Buffer
  console.log(message.toString());
  // client.end();
})
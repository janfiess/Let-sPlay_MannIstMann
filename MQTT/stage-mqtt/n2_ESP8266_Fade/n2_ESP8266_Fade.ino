/*
 * Fading PWM on ESP8266
 * Modified by Jan Fiess on 20180410
 * the esp8266 has 2 onboard leds: pin 2 and led_builtin
 */
 
int freq = 80;
int lightPin1 = 2;
int lightPin2 = 16;
 
void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(lightPin1, OUTPUT);
  pinMode(lightPin2, OUTPUT);
  analogWriteFreq(freq);
  digitalWrite(LED_BUILTIN, LOW);
}
 
void loop() {
  for (int fadeVal = 0; fadeVal <= 1023; fadeVal++){
    analogWrite(lightPin1, fadeVal);
    analogWrite(lightPin2, fadeVal);
    delay(2);
  }
 
  for (int fadeVal = 1023; fadeVal >= 0; fadeVal--){
    analogWrite(lightPin1, fadeVal);
    analogWrite(lightPin2, fadeVal);
    delay(2);
  }
}


/*
 * Fading PWM on ESP32
 * Instructable on https://techtutorialsx.com/2017/06/15/esp32-arduino-led-pwm-fading/
 * analogWrite does not work for ESP32
 * Modified by Jan Fiess on 20180330
 */
 
int freq = 5000;
int ledChannel1 = 0;
int ledChannel2 = 1;
int resolution = 10;
 
void setup() {
  ledcSetup(ledChannel1, freq, resolution);
  ledcAttachPin(2, ledChannel1);

  ledcSetup(ledChannel2, freq, resolution);
  ledcAttachPin(4, ledChannel2);
}
 
void loop() {
  for (int fadeVal = 0; fadeVal <= 1023; fadeVal++){
    ledcWrite(ledChannel1, fadeVal);
    delay(2);
  }
 
  for (int fadeVal = 1023; fadeVal >= 0; fadeVal--){
    ledcWrite(ledChannel1, fadeVal);
    delay(2);
  }

  for (int fadeVal = 0; fadeVal <= 1023; fadeVal++){
    ledcWrite(ledChannel2, fadeVal);
    delay(2);
  }
 
  for (int fadeVal = 1023; fadeVal >= 0; fadeVal--){
    ledcWrite(ledChannel2, fadeVal);
    delay(2);
  }
}


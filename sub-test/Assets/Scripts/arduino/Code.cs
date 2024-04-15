

/* pico
specs:
  PicoGamepad

  Turn a Raspberry Pico 2040 into an HID gamepad

  Supports:
  128 Buttons
  8 Analog axes
  4 Hat switches
  
  created 28 June 2021
  by Jake Wilkinson (RealRobots)

  This example code is in the public domain.

  https://www.gitlab.com/realrobots/PicoGamepad

code :
#include <PicoGamepad.h>

#define PIN_BTN_0 0
#define PIN_BTN_1 3
#define PIN_BTN_2 2

#define PIN_X 27
#define PIN_Y 28


PicoGamepad gamepad;

// 16 bit integer for holding input values
int val;

void setup() {  
  Serial.begin(115200);
  
  pinMode(LED_BUILTIN, OUTPUT);

  // X Potentiometer on pin 26
  pinMode(PIN_X, INPUT);
  // Y Potentiometer on pin 27
  pinMode(PIN_Y, INPUT);

  // Button on pin 
  pinMode(PIN_BTN_0, INPUT_PULLUP);
  pinMode(PIN_BTN_1, INPUT_PULLUP);
  pinMode(PIN_BTN_2, INPUT_PULLUP);
}

void loop() {


  // Repeat with X pin
  val = analogRead(PIN_X);
  val = map(val, 0, 1023, -32767, 32767);
  gamepad.SetX(val);
  
  // Repeat with Y pin
  val = analogRead(PIN_Y);
  val = map(val, 0, 1023, -32767, 32767);
  gamepad.SetY(val);
  
//  gamepad.SetZ(val);
//  gamepad.SetRx(val);
//  gamepad.SetRy(val);
//  gamepad.SetRz(val);
//  gamepad.SetS0(val);
//  gamepad.SetS1(val);

  // Set button 0 of 128 by reading button on digital pin 28
  gamepad.SetButton(0, !digitalRead(PIN_BTN_0));
  gamepad.SetButton(1, !digitalRead(PIN_BTN_1));
  gamepad.SetButton(2, !digitalRead(PIN_BTN_2));

  // Set hat direction, 4 hats available. direction is clockwise 0=N 1=NE 2=E 3=SE 4=S 5=SW 6=W 7=NW 8=CENTER 
  // gamepad.SetHat(0, 8);


  // Send all inputs via HID 
  // Nothing is send to your computer until this is called.
  gamepad.send_update();

  // Flash the LED just for fun
  digitalWrite(LED_BUILTIN, !digitalRead(LED_BUILTIN)); 
  delay(100);
}

*/
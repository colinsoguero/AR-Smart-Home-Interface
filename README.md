# AR-Smart-Home-Interface
IoT inspired smart home application using HTC Vive with SRWorks SDK in Unity that allows users to control connected devices in unique ways.
## Description
**Note: Current committ setup for VR**  
The application uses a TCP client-server connection between Unity and multiple ESP8266 wifi microcontrollers. The ESP8266 modules control various home devices such as a light, fan, and speakers. Music is controlled with a laptop running a Processing script which connects to the Unity server.
## Process
The specific brand of ESP8266 I decided to use is the IZOKEE D1 Mini which is almost identical to the Wemos D1 Mini. These boards are a smaller verion of the regular Wemos board so it has less pins but it was still more than enough for what I needed.
![ESP8266](https://user-images.githubusercontent.com/49736223/81871421-fdf8cb00-9534-11ea-89e5-934cbad8721a.jpg)
After downloading the ESP8266 library, I was able to use the Arduino IDE to write and upload code to the boards. I referred to the pinout diagram below to figure out how to initialize the proper pins in Arduino.
![esp8266-wemos-d1-mini-pinout](https://user-images.githubusercontent.com/49736223/81872215-99d70680-9536-11ea-88b0-b57f3d3ddd9c.png)
I was able to control an LED strip through the ESP8266 usisng the FastLED library. After connecting everything to the ESP this is what it looked like:


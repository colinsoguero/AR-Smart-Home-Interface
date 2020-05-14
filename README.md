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
![ESP8266 connected light](https://user-images.githubusercontent.com/49736223/81871415-f89b8080-9534-11ea-8f06-b4b046929f02.jpg)
I then used a relay attached to an extension cable to control the fan, but really any on/off device plugged into the cable could be controlled.
![ESP8266 connected outlet](https://user-images.githubusercontent.com/49736223/81871365-e7527400-9534-11ea-88c3-374f05223592.jpg)
To allow for music playback in the scene, I used a processing script on my laptop which connected to Unity as a client and was able to playback selected files.
### Unity
I wanted to create fun ways of interacting with the devices so I opted to use a bow and arrow mechanic to turn on the devices. Additionally, I added UIs to the controllers that allow users to choose the color of the light, turn off devices, and play music. I used basic cubes in the scene as virtual switches which are activated by the arrows. A basic interaction diagram can be seen below.
<img width="1039" alt="Interaction Diagram" src="https://user-images.githubusercontent.com/49736223/81879202-55079b80-9547-11ea-9843-2a53404ba460.PNG">


Video Demo:
https://vimeo.com/user103247757/review/418239148/111a8efa3f


/*
    This sketch sends a string to a TCP server, and prints a one-line response.
    You must run a TCP server in your local network.
    For example, on Linux you can use this command: nc -v -l 3000
*/
#include <FastLED.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>


#ifndef STASSID
#define STASSID "Magnum Gig"
#define STAPSK  "jaggedoctopus515"
#endif

#define NUM_LEDS 60

#define DATA_PIN MOSI
#define CLOCK_PIN SCK

CRGB leds[NUM_LEDS];

const char* ssid     = STASSID;
const char* password = STAPSK;

const char* host = "10.0.0.133";
const uint16_t port = 50000;

int ind1, ind2, ind3, ind4;
String state;
String red, green, blue;
ESP8266WiFiMulti WiFiMulti;

void setup() {
  Serial.begin(115200);

  FastLED.addLeds<APA102, DATA_PIN, CLOCK_PIN, BGR>(leds, NUM_LEDS);
  // We start by connecting to a WiFi network
  WiFi.mode(WIFI_STA);
  WiFiMulti.addAP(ssid, password);

  Serial.println();
  Serial.println();
  Serial.print("Wait for WiFi... ");

  while (WiFiMulti.run() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  delay(500);
}


void loop() {
  Serial.print("connecting to ");
  Serial.print(host);
  Serial.print(':');
  Serial.println(port);

  // Use WiFiClient class to create TCP connections
  WiFiClient client;

  if (!client.connect(host, port)) {
    Serial.println("connection failed");
    Serial.println("wait 5 sec...");
    delay(5000);
    return;
  }

  // This will send the request to the server
  client.println("hello from ESP8266");

  //read back one line from server
  Serial.println("receiving from remote server");
  while(true){
      String line = client.readStringUntil('\r');
      Serial.println(line);
      ind1 = line.indexOf(',');  //finds location of first ,
      state = line.substring(0, ind1);   //captures first data String
      ind2 = line.indexOf(',', ind1+1 );   //finds location of second ,
      red = line.substring(ind1+1, ind2+1);   //captures second data String
      ind3 = line.indexOf(',', ind2+1 );
      green = line.substring(ind2+1, ind3+1);
      ind4 = line.indexOf(',', ind3+1 );
      blue = line.substring(ind3+1);
      lightControl(state, red.toFloat(), green.toFloat(), blue.toFloat());

  }


//  Serial.println("closing connection");
//  client.stop();

  Serial.println("wait 5 sec...");
  delay(5000);
}

void lightControl(String state, float red, float green, float blue){
    float r = mapFloat(red, 0.0, 1.0, 0.0, 255.0);
    float g = mapFloat(green, 0.0, 1.0, 0.0, 255.0);
    float b = mapFloat(blue, 0.0, 1.0, 0.0, 255.0);
    Serial.println(blue);
    
    if(state == "A"){
        for(int i = 0; i<NUM_LEDS; i++){
            leds[i].setRGB(r, g, b);
            FastLED.show();
            FastLED.setBrightness(32);
        }
    }
    if(state == "B"){
        for(int i = 0; i<NUM_LEDS; i++){
            leds[i].setRGB(r, g, b);
            FastLED.show();
            FastLED.setBrightness(32);
        }
    }

}
float mapFloat(float x, float in_min, float in_max, float out_min, float out_max)
{
 return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}


#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>

#ifndef STASSID
#define STASSID "Magnum Gig"
#define STAPSK  "jaggedoctopus515"
#endif


const char* ssid     = STASSID;
const char* password = STAPSK;

const char* host = "10.0.0.133";
const uint16_t port = 50000;

int fan = 16;

ESP8266WiFiMulti WiFiMulti;

void setup() {
  Serial.begin(115200);

  pinMode(fan, OUTPUT);
  digitalWrite(fan, LOW);
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
          if(line == "F"){
              digitalWrite(fan, HIGH);
            }
            if(line == "O"){
              digitalWrite(fan, LOW);
            }
  }


//  Serial.println("closing connection");
//  client.stop();

  Serial.println("wait 5 sec...");
  delay(5000);
}

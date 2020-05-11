import processing.sound.*;
import processing.net.*; 

Client myClient; 
int val; 
 
SoundFile file;
void setup() { 
  size(200, 200); 
  // Connect to the local machine at port 5204.
  // This example will not run if you haven't
  // previously started a server on this port.
  myClient = new Client(this, "192.168.1.139", 50000); 
  
  file = new SoundFile(this, "still feel.mp3");
} 
 
void draw() { 
  if (myClient.available() > 0) { 
    val = myClient.read(); 
  } 
  
  println(val);
    if(!file.isPlaying())
  {
      if(val == 80)
      {
        file.play();
      }
  }
  
  if(val == 83)
  {
    file.stop();
  }


} 

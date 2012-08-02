var normalEyes : Texture2D;  //normal (open) eye texture
var blinkyEyes : Texture2D;  //blinking eye texture
var eyeMaterial : Material;  //eye material on player
var eyeTimer : float;        //time to keep eyes open
var blinkTimer : float;      //time to keep eyes closed
var blinking : boolean ;         //is the character currently blinking?
private var currTime : float;//current timer
var randomFactor : float;    //a random factor to make it a little more interesting

function Start()
{
   currTime = eyeTimer + Random.value * randomFactor; //establish current timer as open eyes
   eyeMaterial.mainTexture = normalEyes;   //set eye texture to open
   blinking = false;                       //we are not blinking
}

function Update()
{
  if(currTime > 0.0f) //if the timer is greater than 0
  {
    currTime -= Time.deltaTime; //subtract time
  }
  else //if the timer is up
  {
    if(blinking) //if the character is blinking
    {
      currTime = eyeTimer + (Random.value * randomFactor);  //reset the timer to the open time + a random value
      eyeMaterial.mainTexture = normalEyes; // set the texture to open eyes
      blinking = false;  //we are no longer blinking
    }
    else  //if the character is not blinking
    {
      currTime = blinkTimer + (Random.value * randomFactor);  //reset the timer to blinking time + a random value
      eyeMaterial.mainTexture = blinkyEyes; //set the texture to blinky eyes
      blinking = true;  //we are now blinking
    }
  }
}
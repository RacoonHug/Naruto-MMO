var speed = 6.0;
var PrefabBullet:Transform;
var ShootForce:float;
var nextChangeTime:float;
var hasLanded:boolean;

//audio setting

var Smokepuffaudio : AudioClip;
var JumpaudioA : AudioClip;
var JumpaudioB : AudioClip;
var LandaudioA : AudioClip;
var LandaudioB : AudioClip;
var StepaudioA : AudioClip;
var Fallingwater: AudioClip;


private var moveDirection = Vector3.zero;
private var grounded : boolean = false;


function Start() {
animation.wrapMode = WrapMode.Loop;
animation["shoot"].wrapMode = WrapMode.Once;
animation["shoot"].layer = 1;
animation["Katon"].wrapMode = WrapMode.Once;
animation["Katon"].layer = 2;
extChangeTime = 0.0f;

animation.Stop();
}






function Update()
{
	if(Input.GetKeyDown(KeyCode.Escape))
	{
		Application.LoadLevel (0);
	}
	
	var controller : CharacterController = GetComponent(CharacterController);
	
	if (controller.isGrounded) 
	{
		if(!hasLanded)
		{
			//land audio
			if(Random.Range(0.0f, 2.0f) > 1)
			{
				audio.clip = LandaudioA ;
			}
			else
			{
				audio.clip = LandaudioB ;
			}
			audio.Play();
			hasLanded = true;
			nextChangeTime = Time.time + 0.7f;
		}

        // We are grounded, so recalculate movedirection directly from axes
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		
		if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
		{
			animation.wrapMode = WrapMode.Loop;
			animation.CrossFade("run");
			animation["run"].speed = 2.4;
			
			if( Time.time > nextChangeTime)
			{
				audio.pitch = audio.pitch + Random.Range(-0.2f, 0.2f);
				audio.pan = audio.pan + Random.Range(0.5f, 1.0f);
				audio.clip = StepaudioA;
				audio.Play();
				audio.pitch = 1.0f;
				audio.pan = 1.0f;
				nextChangeTime = Time.time + 0.3f;
			}
		}
		else
		{
	  		animation.wrapMode = WrapMode.Loop;
      		animation.CrossFade ("idle");
	  	}
	  
	  	if (Input.GetButton("Jump")) 
	  	{
			moveDirection.y = 30;
			
			animation.wrapMode = WrapMode.Loop;
			animation.CrossFade("jump");
			animation["jump"].speed = 1.6;
			
			//audiojump
			audio.pitch = audio.pitch + Random.Range(-0.2f, 0.2f);
			if(Random.Range(0.0f, 2.0f) > 1)
			{
				audio.clip = JumpaudioA ;
			}
			else
			{
				audio.clip = JumpaudioB ;
			}
			audio.Play();
			audio.pitch = 1.0f;
			hasLanded = false;
		}
	}
					
	moveDirection.y -= 30 * Time.deltaTime;
	
	controller.Move(moveDirection * Time.deltaTime);

}///end  function update

function shoot () {
animation.Play("shoot");
yield WaitForSeconds(1.1); animation.Stop("shoot");
}

function Katon () {
animation.Play("Katon");
yield WaitForSeconds(2); animation.Stop("Katon");
}

function Fireball () {

if(Input.GetKeyDown("r"))
      {
      var instanceBullet = Instantiate(PrefabBullet, transform.position, Quaternion.identity);
      instanceBullet.rigidbody.AddForce(transform.forward * ShootForce);
      }
	  Katon();
}


@script RequireComponent(CharacterController)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 500f;
	public AudioClip fireSound;

	float xmin;
	float xmax;


	// Use this for initialization
	void Start () {
		/// use camera to bind the screen boundaries.(setting min and max of x)

		///MUST INPUT VECTOR COORDINATES BETWEEN 0 AND 1. IN THIS METHOD THEY REPRESENT THE DISTANCE RELative to screen size
			//top right would be 1,1 while bottom left would be 0,0...z is distance from screen
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0.0f,0.0f,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1.0f,0.0f,distance));

		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;

	}
	
	// Update is called once per frame
	void Update () {
		laserLogic ();

		shipMovement ();

	}

	void OnTriggerEnter2D(Collider2D collider) {


		Projectile missile = collider.gameObject.GetComponent<Projectile>(); //Any object of type projectile that hits the collider is "missile"
		if (missile) {
			

			health -= missile.getDamage ();
			missile.Hit ();
			if (health <= 0) {
				death ();
			}
			Debug.Log ("Player Struck by a projectile");
		}

	}

	void laserLogic ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			//create a new laser called "beam" with 0 rotation at the given location.
			InvokeRepeating ("Fire", 0.000001f, firingRate);
			//We don't want to set 0 for time because it may do multiple at the same time
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}

	void Fire() {
		Vector3 offset = new Vector3 (0, 1, 0); //create offset of 1 for the laser spawn to avoid causing damage to self
		GameObject beam = Instantiate (projectile, transform.position+offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3(0f,projectileSpeed,0);

		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}
	void shipMovement() {
		//Time.deltaTime makes movement independent of frame rate
		//Vector3.Left & Right are predefined vectors that move by a single unit. 

	/// ARROWS
		if (Input.GetKey(KeyCode.LeftArrow)) {
			//			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime; //Vector3.left = 1, making it identical to the commented-out logic above

		} else 	if (Input.GetKey(KeyCode.RightArrow)) {
			//			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		} 

	/// CLAMPING
		//So the new X position will be the old x position, restricted to the xmin and xmax
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax); 
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}



	/*	void shipGridMovement {
											//If I wanted movement to be more grid based

		//Move The Ship in increments with Arrow Keys

							// float motionIncrement = 0.5f;  //If I wanted movement to be more grid based
												 else 		if (Input.GetKeyDown(KeyCode.UpArrow)) {
																	shipPosition.y = shipPosition.y + motionIncrement;
															} else 		if (Input.GetKeyDown(KeyCode.DownArrow)) {
															shipPosition.y = shipPosition.y - motionIncrement;
																} 
		} */

	void death ()
	{
		Destroy (gameObject);
		LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		manager.LoadLevel ("Win Screen");
	}
}

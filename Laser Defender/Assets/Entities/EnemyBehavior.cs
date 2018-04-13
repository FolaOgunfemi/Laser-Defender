using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
	////SUMMARY
	 //Enemies will move based on overall enemeyFormation object.

	//We will have enemies fire according to a probability that is calculated once per frame. If chance-based conditions are met, we will fire.
	   //prob(fire this frame) = time elapsed x frequency

	//When Enemies spawn, we would like there to be a slight delay, and they shoudl spawn in one of the open positions. 
	public float health = 150f;

	public GameObject projectile;
	public float projectileSpeed = 10f;
	//public float firingRate = 0.2f;
	public float shotsPerSecond = 0.5f; //0.5 would be firing one shot every two seconds
	public int scoreValue = 150;
	public AudioClip enemyFireSound;
	public AudioClip enemyDeathSound;

	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D collider) {

			
		Projectile missile = collider.gameObject.GetComponent<Projectile>(); //Any object of type projectile that hits the collider is "missile"
		if (missile) {
			health -= missile.getDamage ();
			missile.Hit ();
			if (health <= 0) {
				enemyDeath ();
			}
			Debug.Log ("Enemy Struck by a projectile");
		}

	}

	void Update () {
		enemyLaserLogic ();


	}

	void enemyLaserLogic ()
	{
		//GOAL: Tweak the probability to rarely fire more than once per frame.

		float probability = Time.deltaTime * shotsPerSecond; //probability(fire this frame) = time elapsed x frequency

		if (Random.value < probability) {       //A simple way to turn probability into action
			enemyFire ();
		}
	}

	void enemyFire() {
		Vector3 firePosition = transform.position + new Vector3 (0f,-0.2f,0f); //creating projectile a small distance away from actual ship
		GameObject enemyBeam = Instantiate (projectile, firePosition, Quaternion.identity) as GameObject;
		enemyBeam.GetComponent<Rigidbody2D> ().velocity = new Vector3(0f,-projectileSpeed,0f); //projectile speed is negative when set as a velocity bc we want it to point downward
		AudioSource.PlayClipAtPoint (enemyFireSound, transform.position);
	}

	void enemyDeath ()
	{
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint (enemyDeathSound, transform.position);
		scoreKeeper.scoreTracker (scoreValue);
		//add the scoreValue to the score
	}
}

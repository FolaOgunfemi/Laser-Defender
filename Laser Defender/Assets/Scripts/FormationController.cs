using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <RESOLVED BUGS>
/// 1) Enemy Gets Stuck on Edge of Playspace at certain movement speeds
/// 	//
/// </RESOLVED BUGS>
public class FormationController : MonoBehaviour { //attatched to EnemyFormaiton GameObject
	public GameObject enemyPrefab;

	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float xmax;
	private float xmin;

	private float rightEdgeOfFormation; 
	private float leftEdgeOfFormation; 



	// Use this for initialization
	void Start () {
		setBoundaries ();

		deployEnemiesUntilFull();
}


	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0f));
	}

	// Update is called once per frame
	void Update () {

		enemyMovement();

		if(AllMembersDead()) {
			Debug.Log("Respawning Formation");
			deployEnemiesUntilFull();
		}
}
		
	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform) { ///Note: Transforms are what tracks an object in the hierarchy so we use that to sort through
			if (childPositionGameObject.childCount > 0) { //counting the children of childPositionGameObject ("Position" in Editor)
				return false;
			}
		}
			return true;
	}

	Transform nextFreePosition() {
		Transform freePosition;

		foreach (Transform childPositionGameObject in transform) { ///Note: Transforms are what tracks an object in the hierarchy.
			if (childPositionGameObject.childCount == 0) { //counting the children of childPositionGameObject ("Position" in Editor)
				freePosition = childPositionGameObject;
				return freePosition;
			
			} 
		}
				return null;
				
	}
		
	void setBoundaries ()
	{
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
	}

	void deployEnemies(){ //deploy 1 enemy to each "Posiiton" GameObject on the screen all at once
		foreach (Transform child in transform) {
			//Note: "child" refers to the child of the gameobject that this script is attatched to....in this case, EnemyFormation
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			///Instantiate returns an object. We do not need the "GameObject enemy =" to instantiate. We do this to set the object equal to a specific gameObject that we can make references to in code.
			enemy.transform.parent = child; //this reads as "the parent of enemy is equal to the child of "this" "...where this, is the Object that EnemySpawner is attatched to.
	}
}
	void deployEnemiesUntilFull(){ // deploy 1 enemy to THE NEXT EMPTY "Position" GameObject, one at a time until they are all full
		Transform freePosition = nextFreePosition ();
		if (freePosition) {
				GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
				///Instantiate returns an object. We do not need the "GameObject enemy =" to instantiate. We do this to set the object equal to a specific gameObject that we can make references to in code.
			enemy.transform.parent = freePosition; //this reads as "the parent of enemy is equal to the child of "this" "...where this, is the Object that EnemySpawner is attatched to.
			}


		if(nextFreePosition()){
		Invoke ("deployEnemiesUntilFull", spawnDelay);  ///THIS CREATES A DELAY FOR SPAWNING BY CALLING THIS METHOD ONLY AFTER A SET TIME PASSES
		}
	}

	void enemyMovement (){
		if (movingRight) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0);
		} else {
			transform.position += new Vector3 (-speed * Time.deltaTime, 0);
		}
		rightEdgeOfFormation = transform.position.x + (0.5f * width);
		leftEdgeOfFormation = transform.position.x - (0.5f * width);

		if (leftEdgeOfFormation < xmin) {  ///If we hit a boundary, reverse direction
			movingRight = true; //toggles true/false
		} else if (rightEdgeOfFormation > xmax) {
			movingRight = false;
		}

	}
}

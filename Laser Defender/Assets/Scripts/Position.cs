using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

	void OnDrawGizmos() {
		//Displays position of object even when not selected in the editor

		Gizmos.DrawWireSphere(transform.position, 1);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

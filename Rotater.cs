using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script rotates the interactable objects in the game to help disnguish them 
/// </summary>
public class Rotater : MonoBehaviour {

	// void -> void
	// Update is called once per frame
	// rotates objects 
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}
}

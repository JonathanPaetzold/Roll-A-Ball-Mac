using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allows for the manipulation of the camera's position.
/// </summary>
public class CameraController : MonoBehaviour {

	// varibale setup
	public GameObject player;
	private Vector3 offeset;

	// void -> void
	// Use this for initialization
	// finds the offest of the player and camera
	void Start () {
		offeset = transform.position - player.transform.position;
	}

	// void -> void 
	// Update is called once per frame
	// manipulates the camera's position in order to manintain the offset
	void LateUpdate () {
		transform.position = player.transform.position + offeset;

	}
}

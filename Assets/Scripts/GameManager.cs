﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public serialHandler handler;


	bool c1Fired = false;
	bool c2Fired = false;
	// Use this for initialization
	void Start () {
		if (GameObject.Find ("SerialController")) {
			handler = GameObject.Find ("SerialController").GetComponent<serialHandler> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (handler && handler.isConnected ()) {

			string data = handler.getRecievedMessage ();

			string[] axes = data.Split (',');

			if (axes.Length == 8) {
				player1.GetComponent<SpaceShip>().joyStick = new Vector2 ((float)double.Parse (axes [0]), (float)double.Parse (axes [1]));
				if(int.Parse(axes[2]) == 1){
					player1.GetComponent<SpaceShip> ().mustShoot = true;
				}

				player2.GetComponent<SpaceShip>().joyStick = new Vector2 ((float)double.Parse (axes [3]), (float)double.Parse (axes [4]));
				if(int.Parse(axes[5]) == 1){
					player2.GetComponent<SpaceShip> ().mustShoot = true;
				}

				if (int.Parse (axes [6]) == 1) {
					if (!c1Fired) {
						player1.GetComponent<SpaceShip> ().fire ();
						c1Fired = true;
					}
				} else {
					c1Fired = false;
				}

				if (int.Parse (axes [7]) == 1) {
					if (!c2Fired) {
						player2.GetComponent<SpaceShip> ().fire ();
						c2Fired = true;
					}
				} else {
					c2Fired = false;
				}

			}

		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour {

	public bool up = false;
	public Animator animator;

	private void Start() {
		animator.SetBool("Up", up);
	}
	public void OnSwitch() {
		if(up) {
			up = false;
		}
		else{
			up = true;
		}

		animator.SetBool("Up", up);
		
		BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
		foreach(BoxCollider2D c in colliders) {
			c.enabled = !c.enabled;
		}
	}
}
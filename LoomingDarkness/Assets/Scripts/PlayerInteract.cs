﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
	public GameObject currInterObj = null;
	public InteractableObject currInterObjScript = null;
	public Inventory inventory;
	public Animator animator;
	public HealthHandler healthHandler;
	public LightHandler lightHandler;
	

	void Update() {
		if(Input.GetButtonDown("Interact") && currInterObj) {
			if(currInterObjScript.storeable) {
				if(inventory.AddItem(currInterObj)) {
					currInterObj.SendMessage("setInactive");
					currInterObj = null;
					currInterObjScript = null;
				}
			}
			else if(currInterObjScript.type == "Fountain") {
				for(int i = 0; i < 100; i++) {
					healthHandler.heal(100);
				}
			}
		}

		if(Input.GetButtonDown("Use torch")) {
			GameObject torch = inventory.FindItemByType("Torch");
			Debug.Log("Pressing torch, torch item: " + torch.name);
			if(torch != null && !lightHandler.usingTorch) {
				lightHandler.turnOnTorch(torch);
				animator.SetBool("Torch", lightHandler.usingTorch);
				// make torch item start depleting
			}
			else {
				Debug.Log("turn off torch");
				lightHandler.turnOffTorch();
				animator.SetBool("Torch", lightHandler.usingTorch);
				// make torch item stop depleting
			}
		}

		if(Input.GetButtonDown("Eat food")) {
			GameObject food = inventory.FindItemByType("Food");
			if(food != null) {
				Debug.Log("Pressing Eat food, food item: " + food.name);
				healthHandler.heal(50); // change value to food heal value in future
				inventory.RemoveItem(food);
				Destroy(food);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			Debug.Log(other.name + " interactable collision");
			currInterObj = other.gameObject;
			currInterObjScript = currInterObj.GetComponent<InteractableObject>();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			if(other.gameObject == currInterObj) {
				currInterObj = null;
				currInterObjScript = null;
			}
		}
	}
}

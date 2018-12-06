﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
	public GameObject currInterObj = null;
	public InteractableObject currInterObjScript = null;
	public Inventory inventory;
	public HealthHandler healthHandler;
	public LightHandler lightHandler;
	

	void Update() {
		if(Input.GetButtonDown("Interact") && currInterObj) {
			if(currInterObjScript.storeable) {
				if(inventory.AddItem(currInterObj)) {
					currInterObj.SendMessage("setInactive");
					currInterObj = null;
					currInterObjScript = null;
					FindObjectOfType<AudioManager>().Play("ItemPickup");					
				}
			}
			else if(currInterObjScript.type == "Fountain") {
				healthHandler.heal(100);
			}
			else if(currInterObjScript.openable) {
				if(currInterObjScript.locked) {
					if(inventory.FindItem(currInterObjScript.unlocker)) {
						currInterObjScript.locked = false;
						inventory.RemoveItem(currInterObjScript.unlocker);
						currInterObjScript.setInactive();
						Debug.Log(currInterObj + " unlocked");
					}
					else {
						Debug.Log(currInterObj + " not unlocked");
					}
				}
				else {
					currInterObjScript.setInactive();
				}
			}
		}

		if(Input.GetButtonDown("Use torch")) {
			Debug.Log("slot1: " + inventory.inventory[0]);
			GameObject torch = inventory.FindItemByType("Torch");
			//Debug.Log("Pressing torch, torch item: " + torch.name);
			if(torch != null && !lightHandler.usingTorch) {
				lightHandler.turnOnTorch(torch);
				FindObjectOfType<AudioManager>().Play("TorchOn");
				FindObjectOfType<AudioManager>().Play("SmallBurn");
				// make torch item start depleting
			}
			else if(torch !=null && lightHandler.usingTorch) {
				Debug.Log("turn off torch");
				lightHandler.turnOffTorch();
				FindObjectOfType<AudioManager>().Stop("SmallBurn");
				FindObjectOfType<AudioManager>().Play("ExtinguishTorch");
				// make torch item stop depleting
			}
		}

		if(Input.GetButtonDown("Eat food")) {
			GameObject food = inventory.FindItemByType("Food");
			if(food != null) {
				Debug.Log("Pressing Eat food, food item: " + food.name);
				healthHandler.heal(50); // change value to food heal value in future
				inventory.RemoveItem(food);
				FindObjectOfType<AudioManager>().Play("Eat");
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

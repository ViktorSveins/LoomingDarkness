﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	public float durability = 100;
	public bool inUse = false;
	public float reduction = 1;
	public float secDelay;
	public float delayPeriod = 0.1f;
	public float lightMeter = 50;
	public float lightsecDelay;
	public float lightDelayPeriod = 0.05f;

	void Start() {
		secDelay = Time.time;
		lightsecDelay = Time.time;
	}
	// Update is called once per frame
	void Update () {
		if(inUse) {
			if(durability > 0) {
				if(Time.time > secDelay) {
					secDelay += delayPeriod;
					durability -= reduction;
				}
				if(Time.time > lightsecDelay) {
					if(durability < 25) {
						lightMeter -= (reduction / (float)1.5);
					}
					lightsecDelay += lightDelayPeriod;
				}
			}
		}else {
			if(Time.time > secDelay) {
				secDelay += delayPeriod;
			}
			if(Time.time > lightsecDelay) {
				lightsecDelay += lightDelayPeriod;
			}
		}
	}

	public float GetDurability() {
		return durability;
	}

	public void SetDurability(float durability) {
		this.durability = durability;
	}

	public float GetLightMeter() {
		return this.lightMeter;
	}

	public void SetLightMeter(float light) {
		this.lightMeter = light;
	}
}

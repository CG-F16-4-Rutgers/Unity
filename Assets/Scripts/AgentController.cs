﻿using UnityEngine;
using System.Collections;

public class AgentController : MonoBehaviour {

	public Vector3 target;
	public bool isSelected;

	private Rigidbody rigidbody;
	private NavMeshAgent navMeshAgent;
	private Color setColor;

	void Start () {
		isSelected = false;
		target = this.transform.position;
		rigidbody = GetComponent<Rigidbody> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		setColor = GetComponent<Renderer> ().material.color;
	}

	void Update () {
		if (this.isSelected) { 		// Change color of agent to orange represent selection
			this.GetComponent<Renderer> ().material.color = new Color (0.9f, 0.5f, 0.3f);
		} else { 					
			this.GetComponent<Renderer> ().material.color = setColor;  // Change color back to original material color;
		}					

		if (target != null && this.isSelected)
			navMeshAgent.SetDestination (target);	
		
	}
}

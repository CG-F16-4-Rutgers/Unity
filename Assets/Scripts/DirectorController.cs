using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DirectorController : MonoBehaviour {

	public Camera camera;

	List<GameObject> obstacles = new List<GameObject> ();
	List<GameObject> agents = new List<GameObject>();

	// Use this for initialization
	void Start () {
		ResetObstacles ();
		ResetAgents ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);

		if (Input.GetButtonDown ("Fire1")) { 
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.tag == "Player") {
					hit.collider.gameObject.GetComponent<AgentController> ().isSelected = true; // Change color
					agents.Add (hit.collider.gameObject);    // Add selected agent to agents list
					ResetObstacles (); 						 // Deselect all obstacles
				} else if (hit.collider.gameObject.tag == "Obstacle") {
					obstacles.Add (hit.collider.gameObject); // Add selected obstacles to obstacles list
					ResetAgents ();						     // Deselect all agents
				} else {
					foreach (GameObject go in agents) {
						go.GetComponent<AgentController> ().target = hit.point;
					}
				}
				
			}
		} else if (Input.GetButtonDown ("Fire2")) { // Deselect everything
			ResetObstacles (); 
			ResetAgents ();
		}
	}

	void ResetObstacles()
	{
		foreach (GameObject go in obstacles) 
		{
			go.GetComponent<ObstacleController> ().isSelected = false;
		}
		obstacles.Clear ();
	}

	// Deselect all agents
	void ResetAgents() 
	{
		foreach (GameObject go in agents) 
		{
			go.GetComponent<AgentController> ().isSelected = false;			
		}
		agents.Clear ();
	}
}

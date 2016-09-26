using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public Transform point1;
	public Transform point2;
	public float speed;

	private Rigidbody rb;
	private bool reverse = false;
	private Vector3 p1, p2;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		p1 = new Vector3 (point1.position.x, transform.position.y, transform.position.z);
		p2 = new Vector3 (point2.position.x, transform.position.y, transform.position.z);
	}
		
	void Update () {
		if (this.gameObject.transform.position.x > p2.x) {
			reverse = true;
		} else if (this.gameObject.transform.position.x < p1.x) {
			reverse = false;
		}

		if (reverse) {
			transform.Translate (-speed * Vector3.right * Time.deltaTime);
		}
		else { 
			transform.Translate (speed * Vector3.right * Time.deltaTime);
		}

		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}

}

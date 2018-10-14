using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPositionFromTracker : MonoBehaviour {
	public GameObject viveTracker;

	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(viveTracker.transform.position.x + 0.3f, viveTracker.transform.position.y + 0.2f, viveTracker.transform.position.z);
	}
}

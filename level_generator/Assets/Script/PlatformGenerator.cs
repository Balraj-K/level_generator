using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public Transform generationPoint;

	public float distanceBetween;
	private float platformWidth;

	public ObjectPooler[] theObjectPools;

	//public GameObject[] thePlatforms;
	private int platformSelector;

	// Use this for initialization
	void Start () {
		//platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) {
			transform.position = new Vector3 (transform.position.x +32+ distanceBetween, transform.position.y, transform.position.z);

			platformSelector = Random.Range (0, theObjectPools.Length);

			//Instantiate (/*thePlatform*/ thePlatforms[platformSelector], transform.position, transform.rotation);

			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);
		}

	}
}

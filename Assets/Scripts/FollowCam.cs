using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    static public FollowCam S; // a FollowCam Singleton

    // fields set in the Unity Inspector pane
    public bool __________________;

    // fields set dynamically
    public GameObject poi; // The point of interest
    public float camZ; // The desired Z pos of the camera

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

	// Update is called once per frame
	void Update () {
        if (poi == null) return; // return if there is no poi

        Vector3 destination = poi.transform.position; // Get the position of the poi
        destination.z = camZ; // Retain a destination.z of camZ
        transform.position = destination; // Set the camera to the destination
	
	}
}

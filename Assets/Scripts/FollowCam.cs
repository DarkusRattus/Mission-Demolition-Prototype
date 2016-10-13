using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    static public FollowCam S; // a FollowCam Singleton
    public float easing = 0.05f;
    public Vector2 minXY;
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

        // Limit the X & Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ; // Retain a destination.z of camZ
        transform.position = destination; // Set the camera to the destination
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;

    }
}

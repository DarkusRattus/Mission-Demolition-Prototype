using UnityEngine;
using System.Collections;

public class CloudCrafter : MonoBehaviour {

    // fields set in the Unity Inspector frame
    public int numClouds = 40; // The # of clouds to make
    public GameObject[] cloudPrefabs; // The prefabs for the clouds
    public Vector3 cloudPosMin; // Min position for each cloud
    public Vector3 cloudPosMax; // Max position for each cloud
    public float cloudScaleMin = 1; // Min scale of each cloud
    public float cloudScaleMax = 5; // Max scale of each cloud
    public float cloudSpeedMult = 0.5f; // Adjusts speed of clouds

    public bool _____________;

    // fields set dynamically
    public GameObject[] cloudInstances;

    void Awake()
    {
        cloudInstances = new GameObject(numClouds); // Make an array large enough to hold all the clouds
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud; 
        for(int i = 0; i < numClouds; i++)
        {
            int prefabNum = Random.Range(0, cloudPrefabs.Length);
            cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;
            // Position cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            // Scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // Smaller clouds (with smaller scaleU) should be nearer to the ground
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            // Smaller clouds should be further away
            cPos.z = 100 - 90 * scaleU;
            // Apply these transforms to the cloud
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            // Make cloud the child of the anchor
            cloud.transform.parent = anchor.transform;
            // Add the cloud to the cloudInstances
            cloudInstances[i] = cloud;
        }
    }


	// Update is called once per frame
	void Update () {
	    // Iterate over each cloud that was created
        foreach (GameObject cloud in cloudInstances)
        {
            // Get the cloud scale and position
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            // Move larger clouds faster
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            //If a cloud has moved too far to the left...
            if (cPos.x <= cloudPosMin.x)
            {
                // Move it far to the right
                cPos.x = cloudPosMax.x;
            }
            // Apply the new position to the cloud
            cloud.transform.position = cPos;
        }
	}
}

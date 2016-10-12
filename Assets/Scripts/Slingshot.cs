using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

    // Fields set in the Unity Inspector pane
    public GameObject prefabProjectile;
    public float velocityMult = 4f;

    public bool ________________;
    // Fields set dynamically
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    void Update()
    {
        if (!aimingMode) return; // If Slingshot is not in aimingMode
        Vector3 mousePos2D = Input.mousePosition; // Get the current mouse position in 2D screen coordinates

        // Convert the mouse position to 3D world coordinates
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; // Find the delta from the launchPos to the mousePos3D

        // Limit the mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            // The mouse has bee released
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCam.S.poi = projectile;
            projectile = null;
        }

    }

    void OnMouseEnter()
    {
        // print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        // print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true; // The player has pressed the mouse button while over Slingshot
        projectile = Instantiate(prefabProjectile) as GameObject; // Instantiate a Projectile
        projectile.transform.position = launchPos; // Start it at the LaunchPoint
        projectile.GetComponent<Rigidbody>().isKinematic = true; // Set it to isKinematic for now

    }
}

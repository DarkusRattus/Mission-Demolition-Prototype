using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

    // Fields set in the Unity Inspector pane
    public GameObject prefabProjectile;
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

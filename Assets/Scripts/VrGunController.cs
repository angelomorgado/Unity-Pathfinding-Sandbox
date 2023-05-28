using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrGunController : MonoBehaviour
{
    [SerializeField] private int gunDamage = 1;
    [SerializeField] private float fireRate = 0.25f; // Number in seconds which controls how often the player can fire
    [SerializeField] private float weaponRange = 50f; 
    [SerializeField] private float hitForce = 100f; 
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject frontOfGun;
    private AudioSource gunAudio; 
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Set the number of points in the line
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, frontOfGun.transform.position); // Set the start position of the line
        lineRenderer.SetPosition(1, frontOfGun.transform.position + frontOfGun.transform.forward * weaponRange); // Set the end position of the line

        if(Input.GetButtonDown("Fire1"))
        {
            // Shoot();
            InvokeRepeating("Shoot", 0f, fireRate); // Call the Shoot function repeatedly after a delay of 0 seconds, then repeat every 0.25 seconds
        }

        if(Input.GetButtonUp("Fire1"))
        {
            CancelInvoke("Shoot"); // Cancel the Shoot function
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunAudio.Play();

        // Shot origin, shot direction, info variable, range
        RaycastHit hit; // Declare a raycast hit to store information about what our raycast has hit
        if(Physics.Raycast(frontOfGun.transform.position, frontOfGun.transform.forward, out hit, weaponRange))
        {
            Debug.Log(hit.transform.name);

            GameObject hitObject = hit.transform.gameObject;

            // Change the color of the hit object
            hitObject.GetComponent<Renderer>().material.color = Color.black;
            
            // Check if the object tag is Target
            if(hitObject.CompareTag("Zombie"))
            {
                hitObject.GetComponent<GunTarget>().TakeDamage(gunDamage);
            }
            else if(hitObject.CompareTag("Target"))
            {
                hitObject.GetComponent<Rigidbody>().AddForce(-hit.normal * hitForce);
            }
        }
    }
}

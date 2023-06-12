using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private int gunDamage = 1;
    [SerializeField] private float fireRate = 0.25f; // Number in seconds which controls how often the player can fire
    [SerializeField] private float weaponRange = 50f; 
    [SerializeField] private float hitForce = 100f; 
    [SerializeField] private Camera fpsCam; 
    [SerializeField] private ParticleSystem muzzleFlash;
    private AudioSource gunAudio; 

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, weaponRange))
        {
            // Position of the raycast hit
            Debug.Log(hit.transform.name);

            GameObject hitObject = hit.transform.gameObject;
            
            // Check if the object tag is Target
            if(hitObject.CompareTag("Zombie"))
            {
                Debug.Log("Zombie hit!!");

                hitObject.GetComponent<ZombieController>().TakeDamage(gunDamage);
            }
            else if(hitObject.CompareTag("Target"))
            {
                hitObject.GetComponent<Rigidbody>().AddForce(-hit.normal * hitForce);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Public Variables
    [SerializeField] private int gunDamage = 1; // Set the number of hitpoints that this gun will take away from shot objects with a health script
    // [SerializeField] private float fireRate = 0.25f; // Number in seconds which controls how often the player can fire
    [SerializeField] private float weaponRange = 50f; // Distance in Unity units over which the player can fire
    // [SerializeField] private float hitForce = 100f; // Amount of force which will be added to objects with a rigidbody shot by the player
    [SerializeField] private Camera fpsCam; // Holds a reference to the first person camera
    [SerializeField] private ParticleSystem muzzleFlash; // Holds a reference to the muzzle flash particle system
    private AudioSource gunAudio; // Reference to the audio source which will play our shooting sound effect


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
            Shoot();
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
            Debug.Log(hit.transform.name);

            GameObject hitObject = hit.transform.gameObject;
            
            // Check if the object tag is Target
            if(hitObject.CompareTag("Target"))
            {
                hitObject.GetComponent<GunTarget>().TakeDamage(gunDamage);
            }
        }
    }
}

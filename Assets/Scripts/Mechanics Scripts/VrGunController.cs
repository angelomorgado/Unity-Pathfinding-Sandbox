using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VrGunController : MonoBehaviour
{
    [SerializeField] private int gunDamage = 1;
    [SerializeField] private float weaponRange = 50f; 
    [SerializeField] private float hitForce = 100f; 
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject frontOfGun;
    private AudioSource gunAudio; 
    private LineRenderer lineRenderer;

    // Input System
    [SerializeField] private InputActionReference shootActionReference;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Set the number of points in the line

        // Activate Input System
        shootActionReference.action.performed += OnShoot;
    }

    // Update is called once per frme
    void Update()
    {
        lineRenderer.SetPosition(0, frontOfGun.transform.position); // Set the start position of the line
        lineRenderer.SetPosition(1, frontOfGun.transform.position + frontOfGun.transform.forward * weaponRange); // Set the end position of the line
    }

    void OnShoot(InputAction.CallbackContext obj)
    {
        Shoot();
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
                hitObject.GetComponent<ZombieController>().TakeDamage(gunDamage);
            }
            else if(hitObject.CompareTag("Target"))
            {
                hitObject.GetComponent<Rigidbody>().AddForce(-hit.normal * hitForce);
            }
        }
    }
}

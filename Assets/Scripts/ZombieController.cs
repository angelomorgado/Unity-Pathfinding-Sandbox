using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int health = 3;
    private AudioSource gunAudio; 
    private ObjectSpawner objectSpawner;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
        objectSpawner = FindObjectOfType<ObjectSpawner>();
    } 

    // Update is called once per frame
    void Update()
    {  
        // // Kill the player if the zombie is close enough
        // // Get the correct player
        GameObject player = (GameObject.Find("PlayerCapsule") != null) ? GameObject.Find("PlayerCapsule") : GameObject.Find("XR Origin");

        if (Vector3.Distance(this.transform.position, player.transform.position) < 1.0f){
            Debug.Log("Player Morreu");
            unlockCursor();
            SceneManager.LoadScene(0);
        }

        // if (Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 1.0f){
        //     Debug.Log("Player Morreu");
        //     unlockCursor();
        //     SceneManager.LoadScene(0);
        // }


    }

    private void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gunAudio.Play();
        
        if(health == 0)
        {
            // Wait for 1 second
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        // Get DeathSound child
        AudioSource deathSound = transform.GetChild(2).GetComponent<AudioSource>();
        // Play the death sound
        deathSound.Play();

        // Remove the agent component
        Destroy(this.GetComponent<Agent>());
        // Stop the walk animation
        animator.SetBool("Walk", false);

        animator.SetTrigger("Fall");
        yield return new WaitForSeconds(0.5f);
        animator.speed = 0;
        yield return new WaitForSeconds(2f);
        
        // Freeze the animation
        // Play the Fall motion
        // Destroy the zombie
        Destroy(gameObject);
        // Remove the zombie from the spawnedObjects list in ObjectSpawner
        objectSpawner.RemoveObject(gameObject);
    } 
}
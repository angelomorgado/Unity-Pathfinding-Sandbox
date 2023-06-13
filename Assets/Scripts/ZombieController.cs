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
        // Kill the player if the zombie is close enough
        // Get the correct player
        GameObject player = (GameObject.Find("Player") != null) ? GameObject.Find("Player") : GameObject.Find("XR Origin");

        if (Vector3.Distance(this.transform.position, player.transform.position) < 1.0f){
            Debug.Log("Player Morreu");
            unlockCursor();
            SceneManager.LoadScene(0);
        }

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
        Debug.Log("Zombie health: " + health);
        if(health <= 0)
        {
            // Wait for 1 second
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        this.GetComponent<Agent>().isMoving = false;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Fall");
        // Play the Fall motion
        yield return new WaitForSeconds(1.5f);
        // Destroy the zombie
        Destroy(gameObject);
        // Remove the zombie from the spawnedObjects list in ObjectSpawner
        objectSpawner.RemoveObject(gameObject);
    } 
}
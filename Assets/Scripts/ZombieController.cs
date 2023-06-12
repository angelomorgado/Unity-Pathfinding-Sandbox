using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int health = 3;
    private AudioSource gunAudio; 
    [SerializeField] private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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
        animator.SetTrigger("Fall");
        // Play the Fall motion
        yield return new WaitForSeconds(2f);
        // Destroy the zombie
        Destroy(gameObject);
    }
}

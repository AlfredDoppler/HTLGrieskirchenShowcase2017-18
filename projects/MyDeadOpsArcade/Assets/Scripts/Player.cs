using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health;
    public int shotingdelay;

    public GameObject bullet;

	// Use this for initialization
    IEnumerator Start()
	{
        health = 100;

        while(true){
            Shot(transform);
			yield return new WaitForSeconds(shotingdelay);

		}
	}

	// Movement Speed
	public float speed = 5;

	void Update()
	{
		// Right, Left
		float x = Input.GetAxisRaw("Horizontal");

		// Up, Down
		float y = Input.GetAxisRaw("Vertical");

		// Get the direction of movement
		Vector2 direction = new Vector2(x, y).normalized;

		// Assign the movement speed and direction
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	// Called at the moment a collision happens
	void OnTriggerEnter2D(Collider2D c)
	{
        //Destroy(gameObject);
        //health =- 50;
        //if(health == 0){
		//	Destroy(gameObject);
        //}
		
	}

    void Shot(Transform origin){
        Instantiate(bullet, origin.position, origin.rotation);
    }
}

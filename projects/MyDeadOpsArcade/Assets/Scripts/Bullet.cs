using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Bullet Movement Speed
    public float speed = 0.001f;

	// How long between this object’s creation, and its deletion
	public float lifeTime = 100;

	// Attack Power
	public int power = 1;

    GameObject Player;

	void Start() 
	{
        

        // Move in the positive Y-Axis of the local coordinate system.
        Vector2 mouse = Input.mousePosition;
        GetComponent<Rigidbody2D>().velocity =  mouse * speed;

        //Player = GameObject.Find("Player");
		//gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Player.transform.position - gameObject.transform.position);
		//gameObject.transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

		// After lifeTime seconds, delete the bullet.
		Destroy(gameObject, lifeTime);
	}
}

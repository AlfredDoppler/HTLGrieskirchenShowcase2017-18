using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDialogue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("show Text");
        }
    }
    
}

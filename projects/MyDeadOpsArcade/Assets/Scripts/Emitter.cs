using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

    public GameObject[] waves;

    private int maxEnemies = 24;
    private int currEnemies = 0;

    private int currWave;

	// Use this for initialization
    IEnumerator Start () {
		if (waves.Length == 0)
		{
			yield break;
		}

        while(true){
            if(currEnemies < maxEnemies){
                SpawnEnemy();
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy ()
    {
        GameObject Enemy = (GameObject)Instantiate(waves[currWave], gameObject.transform.position, Quaternion.identity);
        currEnemies++;
    }


    IEnumerator OtherSpawn(){
		GameObject g = (GameObject)Instantiate(waves[currWave], transform.position, Quaternion.identity);
		g.transform.parent = transform;
		while (g.transform.childCount != 0)
		{
			yield return new WaitForEndOfFrame();
		}

		Destroy(g);


		if (waves.Length <= ++currWave)
		{
			currWave = 0;
		}
    }
}

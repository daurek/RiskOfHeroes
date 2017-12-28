using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    private int enemysAlive;
    private Transform tr;
    private int numberOfSpawns;
    private bool canSpawn;
    private Vector2[] spawnsPositions;


    [SerializeField] private GameObject enemy;
    [SerializeField] private int maxEnemysN;
    [SerializeField] private float spawnCoolDownTime;
    [SerializeField] private GameObject parent;

	void Start () {
        canSpawn = true;

        enemysAlive = 0;
        tr = transform;

        if (maxEnemysN <= 0)
        {
           maxEnemysN = 5;
        }
           
        if(spawnCoolDownTime <= 0.0)
        {
            spawnCoolDownTime = 5;
        }
        setSpawnsPositions();
    }
	

	void Update ()
    {
        if (enemysAlive < maxEnemysN)
        {
            StartCoroutine(Spawn());
        }
    }


    private void setSpawnsPositions()
    {
        numberOfSpawns = tr.childCount;
        spawnsPositions = new Vector2[numberOfSpawns];
        for (int i = 0; i < numberOfSpawns; i++)
        {
            spawnsPositions[i] = tr.GetChild(i).transform.position;
        }
    }

    IEnumerator Spawn()
    {
        if (canSpawn)
        {
            SpawnEnemy();
            canSpawn = false;

            yield return new WaitForSeconds(spawnCoolDownTime);
            canSpawn = true;
        }


    }

    private GameObject enemyTemp = null;

    private void SpawnEnemy()
    {
        enemyTemp = Instantiate(enemy, spawnsPositions[Random.Range(0,numberOfSpawns)], transform.rotation);
        enemyTemp.transform.SetParent(parent.transform);
        enemysAlive++;

        enemyTemp = null;
        
    }




}

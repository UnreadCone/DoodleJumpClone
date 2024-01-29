using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerate : MonoBehaviour
{
    public GameObject mobPrefab;
    public float spawnDelay = 5f;
    private bool canSpawn = true;


    private void Start()
    {

        StartCoroutine(SpawnMob());
    }

    private IEnumerator SpawnMob()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            if (canSpawn)
            {
                GenerateMob();
                canSpawn = false;
                yield return new WaitForSeconds(spawnDelay);
                canSpawn = true; 
            }
            yield return null;
        }
    }

    private void GenerateMob()
    {
        float randomX = UnityEngine.Random.Range(-2, 2);
        GameObject mob = Instantiate(mobPrefab, new Vector3(randomX, 3, transform.position.z), Quaternion.identity);
    }
}
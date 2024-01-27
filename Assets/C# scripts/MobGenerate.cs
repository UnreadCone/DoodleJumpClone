using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerate : MonoBehaviour
{
    public GameObject mobPrefab; // префаб объекта, который будет появляться
    public float spawnDelay = 10f; // задержка перед появлением объекта
private bool canSpawn = true; // флаг для проверки возможности появления

    private void Start()
    {
        StartCoroutine(SpawnMob());
    }

    private IEnumerator SpawnMob()
    {
        yield return new WaitForSeconds(spawnDelay); // ждем 10 секунд перед первым появлением объекта

        while (true)
        {
            if (canSpawn)
            {
                GenerateMob(); // вызываем функцию для создания объекта
                canSpawn = false; // отключаем возможность появления объекта
                yield return new WaitForSeconds(spawnDelay); // ждем 10 секунд перед следующим появлением
                canSpawn = true; // включаем возможность появления объекта
            }
            yield return null;
        }
    }

    private void GenerateMob()
    {
        float randomX = UnityEngine.Random.Range(-3, 3);
        GameObject mob = Instantiate(mobPrefab, new Vector3(randomX, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerate : MonoBehaviour
{
    public GameObject mobPrefab; // ������ �������, ������� ����� ����������
    public float spawnDelay = 10f; // �������� ����� ���������� �������
private bool canSpawn = true; // ���� ��� �������� ����������� ���������

    private void Start()
    {
        StartCoroutine(SpawnMob());
    }

    private IEnumerator SpawnMob()
    {
        yield return new WaitForSeconds(spawnDelay); // ���� 10 ������ ����� ������ ���������� �������

        while (true)
        {
            if (canSpawn)
            {
                GenerateMob(); // �������� ������� ��� �������� �������
                canSpawn = false; // ��������� ����������� ��������� �������
                yield return new WaitForSeconds(spawnDelay); // ���� 10 ������ ����� ��������� ����������
                canSpawn = true; // �������� ����������� ��������� �������
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
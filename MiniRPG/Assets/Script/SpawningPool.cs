using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int monsterCount = 0;
    int reserveCount = 0;

    [SerializeField]
    int keepMonsterCount = 0;
    [SerializeField]
    float  spawnRadius = 15.0f;
    [SerializeField]
    float spawnTime = 5.0f;
    [SerializeField]
    Vector3 spawnPos;

    public void AddMonsterCount(int v) { monsterCount += v; }
    public void SetKeepMonsterCount(int c) { keepMonsterCount = c; }

    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }
    void Update()
    {
        while (reserveCount + monsterCount < keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }
    IEnumerator ReserveSpawn()
    {
        reserveCount++;
        yield return new WaitForSeconds(Random.Range(0,spawnTime));
        
        GameObject ob = Managers.Game.Spawn(Define.WorldObject.Monster,"Knight");
        NavMeshAgent nma = ob.GetOrAddComponent<NavMeshAgent>();
        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, spawnRadius);
            randDir.y = 0;
            randPos = spawnPos + randDir;

            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
                break;
        }
        ob.transform.position = randPos;
        reserveCount--;
    }
}

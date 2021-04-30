using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;
    [SerializeField] Attacker[] attackerPrefabArray = null;
    [SerializeField] float minSpawnDelay = 0;
    [SerializeField] float maxSpawnDelay = 0;

    // Start is called before the first frame update

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            if (spawn)
            {
                SpawnAttacker();
            }
            
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        int index = Random.Range(0, attackerPrefabArray.Length);
        Spawn(attackerPrefabArray[index]);

    }

    private void Spawn(Attacker attackerPrefab)
    {
        Attacker newAttacker = Instantiate
                    (attackerPrefab, transform.position, Quaternion.identity)
                    as Attacker;
        newAttacker.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopPool : MonoBehaviour
{

    public static PoopPool instance;

    public GameObject poopPrefab;

    GameObject[] pool;
    public int poolSize = 100;
    int currentIndex = 0;

    public LinkedList<Poop> flyingPoop;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        pool = new GameObject[poolSize];
        flyingPoop = new LinkedList<Poop>();

        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(poopPrefab, instance.transform) as GameObject;
            pool[i].SetActive(false);
            pool[i].transform.SetParent(this.transform);
        }
    }

    public static void TakeAPoop(Vector3 startPos, Vector3 velocity, Vector3 gravity, float timeToFinish)
    {
        if (++instance.currentIndex >= instance.poolSize) instance.currentIndex = 0;

        GameObject selectedPoopGameObject = instance.pool[instance.currentIndex];
        selectedPoopGameObject.SetActive(false);
        Poop selectedPoop = selectedPoopGameObject.GetComponent<Poop>();
        selectedPoop.InitPoop(startPos, velocity, gravity, timeToFinish);
        instance.flyingPoop.AddLast(selectedPoop);
        instance.pool[instance.currentIndex].SetActive(true);
        Debug.Log("poop is initiated");
    }
}

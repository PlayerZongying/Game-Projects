using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsGenerator : MonoBehaviour
{
    public Dot[] dotsPrefabs;
    public DotsContainer dotsContainer;
    public int capacity;


    // Start is called before the first frame update
    void Start()
    {
        //GenerateDot();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GeneratorInit()
    {
        capacity = dotsContainer.capacity;
        transform.position += new Vector3(0, capacity, 0);

        for (int i = 0; i < capacity; i++)
        {
            if (dotsPrefabs.Length != 0)
            {
                int index = Random.Range(0, dotsPrefabs.Length);
                Dot dot = Instantiate(dotsPrefabs[index], transform.position + new Vector3(0, dotsContainer.pointHeight[i], 0), Quaternion.identity);
                dot.transform.parent = transform;
                dot.targetPoint = dotsContainer.points[i];
            }
        }

    }



    public void RelocateAndRefillDots()
    {
        // Relocate the remained dots
        //Debug.Log("Relocated");
        int i = 0;
        foreach (Transform childDot in transform)
        {
            //Debug.Log(childDot.gameObject);
            childDot.gameObject.GetComponent<Dot>().targetPoint = dotsContainer.points[i];
            childDot.gameObject.GetComponent<Animator>().SetBool("isAtTarget", false);
            i++;
        }

        //Refill the container with new dots
        for(int j = i; j < capacity; j++)
        {
            int index = Random.Range(0, dotsPrefabs.Length);
            Dot dot = Instantiate(dotsPrefabs[index], transform.position + new Vector3(0, j - i, 0), Quaternion.identity);
            dot.transform.parent = transform;
            dot.targetPoint = dotsContainer.points[j];
        }

    }
}

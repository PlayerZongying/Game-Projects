using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public List<GameObject> Points;

    public GameObject PointPrefab;
    public int PointCount;

    // Start is called before the first frame update
    void Start()
    {
        Points = new List<GameObject>();
        for(int i = 0; i < PointCount; i++)
        {
            GameObject newPoint = Instantiate(PointPrefab, this.transform);
            newPoint.transform.SetParent(this.transform);
            Points.Add(newPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

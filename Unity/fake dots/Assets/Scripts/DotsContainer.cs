using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsContainer : MonoBehaviour
{
    public DotsGenerator dotsGenerator;
    public int capacity = 0;
    public Point pointPrefab;

    public int[] pointHeight;
    public Point[] points;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ContainerInit()
    {
        points = new Point[capacity];
        // defualt
        if (pointHeight.Length == 0)
        {
            pointHeight = new int[capacity];
            for (int i = 0; i < capacity; i++)
            {
                pointHeight[i] = i;
                Point point =  GeneratePoint(i);
                points[i] = point;
            }

        }

        // adjust
        else
        {
            for (int i = 0; i < capacity; i++)
            {
                Point point = GeneratePoint(pointHeight[i]);
                points[i] = point;
            }
        }

    }

    private Point GeneratePoint(int height)
    {
        Point point = Instantiate(pointPrefab,
                                  transform.position + new Vector3(0, height, 0),
                                  Quaternion.identity);// one way to set up, with equal spacing.
        point.transform.parent = transform;
        return point;
    }
}

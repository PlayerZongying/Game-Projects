using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path Instance;
    public List<Transform> ControlPoints;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector3 PosInPathAt(float t)
    {
        t %= ControlPoints.Count - 1;
        int lastCP = Mathf.FloorToInt(t);
        int nextCP = lastCP + 1;
        Vector3 PosInPathAtT = Vector3.Lerp(ControlPoints[lastCP].position, ControlPoints[nextCP].position, t - lastCP);
        return PosInPathAtT;
    }
}

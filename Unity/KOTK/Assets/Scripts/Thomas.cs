using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thomas : Attractor
{
    [Header("Thomas Parameters")]
    public float b = 0.09f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public override void Move(Transform transform, GameObject note)
    {
        Vector3 worldPos = note.transform.position;
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        float x = localPos.x;
        float y = localPos.y;
        float z = localPos.z;

        float dx = -b * x + Mathf.Sin(y);
        float dy = -b * y + Mathf.Sin(z);
        float dz = -b * z + Mathf.Sin(x);

        localPos += new Vector3(dx, dy, dz) * Time.deltaTime * speed;

        worldPos = transform.TransformPoint(localPos);

        note.transform.position = worldPos;
    }
}

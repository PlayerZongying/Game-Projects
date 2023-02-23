using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToggleButton : MonoBehaviour
{
    public RectTransform buttonTransform;
    public float rotationAngle;
    // Start is called before the first frame update
    void Start()
    {
        rotationAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (HeatingSystem.Instance.IsOpen)
        {
            rotationAngle += Time.deltaTime * 10 * Mathf.Log(1 + HeatingSystem.Instance.VolumeFlowRate);
            buttonTransform.eulerAngles = new Vector3( 0, 0, -rotationAngle);
        }
    }
}

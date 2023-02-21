using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCube : MonoBehaviour
{
    public static HeatingSystem heatingSystem;
    public static Path path;
    public static float speed;
    public static float powerForCube;

    // offset, paramiter with volume unit 'L', indicating the absolute position of this water cube  start from heater;
    // 0 <= offset < total water volume;
    public float offset = 0;

    // t, paramiter with no unit, indicating the relative position of this water cube start from heater;
    // in heater: 0 <= t < 1;
    // in pipe1:  1 <= t < 2;
    // in stock:  2 <= t < 3;
    // in pipe2:  3 <= t < 4;
    public float t = 0; 
    public float temperature = 0;

    // unit: J/(kg * K)
    public static float HeatCapacity = 4.2f * 1000;
    // unit: kg/L 
    public static float Density = 1f;

    // Start is called before the first frame update
    void Start()
    {
        heatingSystem = HeatingSystem.Instance;
        path = Path.Instance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveOnPath()
    {
        HeatExchange();
        //SetSytemTemperature();
        offset += Time.deltaTime * speed;
        offset %= heatingSystem.V4;
        t = OffsetToT(offset);
        Vector3 posInPath = path.PosInPathAt(t);
        this.transform.position = posInPath;
    }

    public void ObsorbHeat()
    {
        float deltaQ = heatingSystem.PowerForCube * Time.deltaTime;
        temperature += deltaQ / (HeatCapacity * Density * heatingSystem.CubeVolume);
        temperature = Mathf.Clamp(temperature, 0, 100);
    }
    public void ChangeColor()
    {
        Color newColor = WaterCubeGenerator.Instance.ColorGradient.Evaluate(Mathf.InverseLerp(0, 100, temperature));
        this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    public void HeatExchange()
    {
        if (Mathf.Floor(t) < 1)
        {
            ObsorbHeat();
            ChangeColor();
        }
    }

    public void SetTemperature(float temp)
    {
        temperature = temp;
        Color newColor = WaterCubeGenerator.Instance.ColorGradient.Evaluate(Mathf.InverseLerp(0, 100, temperature));
        this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    // remap offset to t;
    float OffsetToT(float offset)
    {
        float t;
        if (offset < heatingSystem.V1)
        {
            t = Remap(0, heatingSystem.V1, offset, 0, 1);
        }
        else if(offset < heatingSystem.V2)
        {
            t = Remap(heatingSystem.V1, heatingSystem.V2, offset, 1, 2);
        }
        else if (offset < heatingSystem.V3)
        {
            t = Remap(heatingSystem.V2, heatingSystem.V3, offset, 2, 3);
        }
        else
        {
            t = Remap(heatingSystem.V3, heatingSystem.V4, offset, 3, 4);
        }
        return t;
    }

    float Remap(float inputMin, float inputMax, float value, float outputMin, float outputMax)
    {
        float temp = Mathf.InverseLerp(inputMin, inputMax, value);
        return Mathf.Lerp(outputMin, outputMax, temp);
    }

    public void SetOffset(float _offset)
    {
        offset = _offset;
        t = OffsetToT(_offset);
        Vector3 posInPath = path.PosInPathAt(t);
        this.transform.position = posInPath;
    }

}

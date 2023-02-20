using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCube : MonoBehaviour
{
    public static HeatingSystem heatingSystem;
    public static Path path;
    public float offset = 0;
    public float t = 0;
    public float speed = 1;
    public float temperature = 0;

    // unit: J/(kg * K)
    static float HeatCapacity = 4.2f * 1000;
    // unit: kg/L 
    static float Density = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //t += offset;
        heatingSystem = HeatingSystem.Instance;
        path = Path.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveOnPath();
    }

    public void MoveOnPath()
    {
        HeatExchange();
        SetSytemTemperature();
        speed = GetSpeed(t);
        t += Time.deltaTime * speed;
        t %= path.ControlPoints.Count - 1;
        Vector3 posInPath = path.PosInPathAt(t);
        this.transform.position = posInPath;
    }

    float GetSpeed(float t)
    {
        if (Mathf.Floor(t) == 0)
        {
            return heatingSystem.SpeedInHeater;
        }
        else if (Mathf.Floor(t) == 1)
        {
            return heatingSystem.SpeedInPipe1;
        }
        else if (Mathf.Floor(t) == 2)
        {
            return heatingSystem.SpeedInStock;
        }
        else
        {
            return heatingSystem.SpeedInPipe2;
        }
    }

    public void ObsorbHeat()
    {
        float deltaQ = heatingSystem.PowerOfHeater * Time.deltaTime;
        temperature += deltaQ / (HeatCapacity * Density * heatingSystem.CubeVolume);
        temperature = Mathf.Clamp(temperature, 0, 100);
    }
    public void ChangeColor()
    {
        Color newColor = WaterCubeGenerator.Instance.ColorGradient.Evaluate(Mathf.InverseLerp(0, 100, temperature));
        this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    public void SetSytemTemperature()
    {
        if (t < 2 && t + Time.deltaTime * speed > 2)
        {
            heatingSystem.TempAtTop = temperature;
            heatingSystem.TempAvrage += temperature / heatingSystem.CubeCountInStock;
        }
        else if (t < 3 && t + Time.deltaTime * speed > 3)
        {
            heatingSystem.TempAtBottom = temperature;
            heatingSystem.TempAvrage -= temperature / heatingSystem.CubeCountInStock;
        }
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


    
}

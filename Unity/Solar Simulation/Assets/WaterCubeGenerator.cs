using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCubeGenerator : MonoBehaviour
{
    public static WaterCubeGenerator Instance;
    public GameObject WaterCubePrefab;
    public Gradient ColorGradient;

    HeatingSystem HeatingSystem;
    Path Path;
    private List<WaterCube> WaterCubes;

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

    // Start is called before the first frame update
    void Start()
    {
        HeatingSystem = HeatingSystem.Instance;
        HeatingSystem.TempAvrage = HeatingSystem.InitTemp;
        Path = Path.Instance;
        WaterCubes = new List<WaterCube>();
        GenerateWaterCubes(HeatingSystem.CubeCount, Path.ControlPoints.Count - 1);
    }

    // Update is called once per frame
    void Update()
    {
        MoveWaterCubes();
    }

    void GenerateWaterCubes(int WaterCubeCount, int CPCount)
    {
        for (int i = 0; i < WaterCubeCount; i++)
        {
            GameObject NewWaterCubeObj = Instantiate(WaterCubePrefab);
            WaterCube NewWaterCube = NewWaterCubeObj.GetComponent<WaterCube>();
            WaterCubes.Add(NewWaterCube);
            //float offset = (float)i * CPCount / WaterCubeCount;
            float offset = GetOffset(i);
            NewWaterCube.offset = offset;
            NewWaterCube.t = offset;
            NewWaterCube.transform.position = Path.PosInPathAt(offset);
            NewWaterCube.temperature = HeatingSystem.InitTemp;
            NewWaterCube.ChangeColor();

            //NewWaterCubeObj.GetComponent<SpriteRenderer>().color = ColorGradient.Evaluate(HeatingSystem.InitTemp / 100);
            NewWaterCubeObj.transform.SetParent(this.transform);
        }
    }

    float GetOffset(int CubeIndex)
    {
        float offset;
        if (CubeIndex < HeatingSystem.CubeCountInHeater)
        {
            if(HeatingSystem.CubeCountInHeater == 0)
            {
                return 0;
            }
            offset = 0 + (float)CubeIndex / HeatingSystem.CubeCountInHeater;

        }
        else if (CubeIndex < HeatingSystem.CubeCountInHeater + HeatingSystem.CubeCountInPipe1)
        {
            if (HeatingSystem.CubeCountInPipe1 == 0)
            {
                return 1;
            }
            offset = 1 + ((float)CubeIndex - HeatingSystem.CubeCountInHeater) / HeatingSystem.CubeCountInPipe1;
        }
        else if (CubeIndex < HeatingSystem.CubeCountInHeater + HeatingSystem.CubeCountInPipe1 + HeatingSystem.CubeCountInStock)
        {
            if (HeatingSystem.CubeCountInStock == 0)
            {
                return 2;
            }
            offset = 2 + ((float)CubeIndex - HeatingSystem.CubeCountInHeater - HeatingSystem.CubeCountInPipe1) / HeatingSystem.CubeCountInStock;
        }
        else
        {
            if (HeatingSystem.CubeCountInPipe2 == 0)
            {
                return 3;
            }
            offset = 3 + ((float)CubeIndex - HeatingSystem.CubeCountInHeater - HeatingSystem.CubeCountInPipe1 - HeatingSystem.CubeCountInStock) / HeatingSystem.CubeCountInPipe2;
        }

        return offset;
    }


    void MoveWaterCubes()
    {
        if (HeatingSystem.IsOpen)
        {
            HeatingSystem.SimulationTime += Time.deltaTime;
            foreach (WaterCube waterCube in WaterCubes)
            {
                waterCube.MoveOnPath();
            }
        }
    }


    public void ResetWaterCubes()
    {
        foreach (WaterCube waterCube in WaterCubes)
        {
            waterCube.t = waterCube.offset;
            waterCube.transform.position = Path.PosInPathAt(waterCube.offset);
            waterCube.temperature = HeatingSystem.InitTemp;
            waterCube.ChangeColor();
        }
    }
    

    public void StartSimulation()
    {

    }

    public void PauseSimulation()
    {

    }

}

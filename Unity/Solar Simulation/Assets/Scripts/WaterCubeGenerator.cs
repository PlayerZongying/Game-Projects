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

    private float energy;

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
        energy = 0;
        Path = Path.Instance;
        WaterCube.heatingSystem = HeatingSystem;
        WaterCube.path = Path;
        WaterCubes = new List<WaterCube>();
        GenerateWaterCubes(HeatingSystem.CubeCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (HeatingSystem.IsOpen)
        {
            MoveWaterCubes();
            UpdateTemperature();
            //VerifyEnergyConservation();
        }
        OutputManager.Instance.UpdateDynamicOutputText();
    }

    void GenerateWaterCubes(int WaterCubeCount)
    {
        for (int i = 0; i < WaterCubeCount; i++)
        {
            GameObject NewWaterCubeObj = Instantiate(WaterCubePrefab);
            WaterCube NewWaterCube = NewWaterCubeObj.GetComponent<WaterCube>();
            WaterCubes.Add(NewWaterCube);
            float offset = HeatingSystem.CubeVolume * i;
            NewWaterCube.SetOffset(offset);
            NewWaterCube.temperature = HeatingSystem.InitTemp;
            NewWaterCube.ChangeColor();
            NewWaterCubeObj.transform.SetParent(this.transform);
        }
    }


    void MoveWaterCubes()
    {
        if (HeatingSystem.IsOpen)
        {
            HeatingSystem.SimulationTime += Time.deltaTime;
            WaterCube.speed = HeatingSystem.VolumeFlowRate;
            //WaterCube.powerForCube = HeatingSystem.PowerOfHeater * HeatingSystem.CubeVolume / HeatingSystem.VolumeOfHeater;

            foreach (WaterCube waterCube in WaterCubes)
            {
                // move
                waterCube.MoveOnPath();
            }
        }
    }

    void UpdateTemperature()
    {
        HeatingSystem.TotalTempInHeater = 0;
        HeatingSystem.TotalTempInStock = 0;
        int countForHeater = 0;
        int countForStock = 0;
        float TempHeaterMax = float.MinValue;
        float TempHeaterMin = float.MaxValue;
        float TempStockMax = float.MinValue;
        float TempStockMin = float.MaxValue;
        foreach (WaterCube waterCube in WaterCubes)
        {
            // prepare to update temperature in heater 
            if (Mathf.FloorToInt(waterCube.t) == 0)
            {
                countForHeater += 1;
                HeatingSystem.TotalTempInHeater += waterCube.temperature;
                TempHeaterMax = waterCube.temperature > TempHeaterMax ? waterCube.temperature : TempHeaterMax;
                TempHeaterMin = waterCube.temperature < TempHeaterMin ? waterCube.temperature : TempHeaterMin;
            }

            // prepare to update temperature in stock
            else if (Mathf.FloorToInt(waterCube.t) == 2)
            {
                countForStock += 1;
                HeatingSystem.TotalTempInStock += waterCube.temperature;
                TempStockMax = waterCube.temperature > TempStockMax ? waterCube.temperature : TempStockMax;
                TempStockMin = waterCube.temperature < TempStockMin ? waterCube.temperature : TempStockMin;
            }
        }
        // update system temperature in stock
        if (countForHeater > 0)
        {
            HeatingSystem.TempHeaterMax = TempHeaterMax;
            HeatingSystem.TempHeaterMin = TempHeaterMin;
            HeatingSystem.TempHeaterAvrage = HeatingSystem.TotalTempInHeater / countForHeater;
        }
        // update system temperature in stock
        if (countForStock > 0)
        {
            HeatingSystem.TempStockMax = TempStockMax;
            HeatingSystem.TempStockMin = TempStockMin;
            HeatingSystem.TempStockAvrage = HeatingSystem.TotalTempInStock / countForStock;
        }
    }


    public void ResetWaterCubes()
    {
        for (int i = 0; i < WaterCubes.Count; i++)
        {
            WaterCube waterCube = WaterCubes[i];
            float offset = HeatingSystem.CubeVolume * i;
            waterCube.SetOffset(offset);
            waterCube.temperature = HeatingSystem.InitTemp;
            waterCube.ChangeColor();
            energy = 0;
        }
    }

    void VerifyEnergyConservation()
    {
        if (HeatingSystem.IsOpen)
        {
            energy += HeatingSystem.PowerOfHeater * Time.deltaTime;
            float energyObsorbed = 0;
            foreach (WaterCube waterCube in WaterCubes)
            {
                float cubeEnergy = (waterCube.temperature - HeatingSystem.InitTemp) * HeatingSystem.CubeVolume * WaterCube.HeatCapacity * WaterCube.Density;
                energyObsorbed += cubeEnergy;
            }
            Debug.Log(energyObsorbed / energy);
        }
    }


    public void StartSimulation()
    {

    }

    public void PauseSimulation()
    {

    }

}

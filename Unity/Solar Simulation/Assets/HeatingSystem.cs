using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingSystem : MonoBehaviour
{
    public static HeatingSystem Instance;

    [Header("System")]
    public bool isStarted;

    [Header("Heater")]
    [Tooltip("power of heating, unit: W")]
    public float PowerOfHeater;
    [Tooltip("volume of heating, unit: L")]
    public float VolumeOfHeater;
    public float TempHeaterMax;
    public float TempHeaterMin;
    public float TempHeaterAvrage;
    public float TotalTempInHeater;


    [Header("Two Pipes")]

    public float RadiusOfPipe1;
    public float LengthOfPipe1;

    public float RadiusOfPipe2;
    public float LengthOfPipe2;

    [Header("Valve")]
    public bool IsOpen = false;
    public float VolumeFlowRate;
    public float SimulationTime;

    [Header("Stock")]
    public float VolumeOfStock;
    public float TempStockMax;
    public float TempStockMin;
    public float TempStockAvrage;
    public float TotalTempInStock;

    public float VolumeOfPipe1 => Mathf.PI * RadiusOfPipe1 * RadiusOfPipe1 * LengthOfPipe1 * 1000; //(m^3 = 1000L)
    public float VolumeOfPipe2 => Mathf.PI * RadiusOfPipe2 * RadiusOfPipe2 * LengthOfPipe2 * 1000; //(m^3 = 1000L)
    public float V1 => VolumeOfHeater;
    public float V2 => VolumeOfHeater + VolumeOfPipe1;
    public float V3 => VolumeOfHeater + VolumeOfPipe1 + VolumeOfStock;
    public float V4 => VolumeOfHeater + VolumeOfPipe1 + VolumeOfStock + VolumeOfPipe2;

    public float SpeedInPipe1 => VolumeFlowRate / VolumeOfPipe1;
    public float SpeedInPipe2 => VolumeFlowRate / VolumeOfPipe2;
    public float SpeedInHeater => VolumeFlowRate / VolumeOfHeater;
    public float SpeedInStock => VolumeFlowRate / VolumeOfStock;

    private float VolumeTotal => VolumeOfHeater + VolumeOfStock + VolumeOfPipe1 + VolumeOfPipe2;

    [Header("Water")]
    public float InitTemp = 0;
    public int CubeCount = 1000;
    public float CubeVolume => VolumeTotal / CubeCount;
    public int CubeCountInHeater => Mathf.RoundToInt(CubeCount * VolumeOfHeater / VolumeTotal);
    public int CubeCountInPipe1 => Mathf.RoundToInt(CubeCount * VolumeOfPipe1 / VolumeTotal);
    public int CubeCountInStock => Mathf.RoundToInt(CubeCount * VolumeOfStock / VolumeTotal);
    public int CubeCountInPipe2 => Mathf.RoundToInt(CubeCount * VolumeOfPipe2 / VolumeTotal);
    public float PowerForCube => PowerOfHeater * CubeVolume / VolumeOfHeater;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetSystem()
    {
        IsOpen = false;
        isStarted = false;
        WaterCubeGenerator.Instance.ResetWaterCubes();
        InputManager.Instance.ActivateStaticInput(true);
        SimulationTime = 0;
        TempHeaterMax = InitTemp;
        TempHeaterMin = InitTemp;
        TempHeaterAvrage = InitTemp;
        TempStockMax = InitTemp;
        TempStockMin = InitTemp;
        TempStockAvrage = InitTemp;
    }

    public void ToggleSystem()
    {
        if (!isStarted)
        {
            WaterCubeGenerator.Instance.ResetWaterCubes();
            InputManager.Instance.ActivateStaticInput(false);
            OutputManager.Instance.SetStaticOutputText();
            isStarted = true;
        }
        IsOpen = !IsOpen;
    }

}

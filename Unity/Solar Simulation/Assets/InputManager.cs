using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [Header("Valve")]
    public TMP_InputField ValveFlowRateInput;
    public float default_VFR = 5f;

    [Header("Heater")]
    public TMP_InputField HeaterVolumeInput;
    public float default_HV = 31.41593f;
    public TMP_InputField HeaterPowerInput;
    public float defualt_HP = 31.41593f;

    [Header("Pipe1")]
    public TMP_InputField Pipe1RadiusInput;
    public float default_PR1 = 1f;
    public TMP_InputField Pipe1LengthInput;
    public float default_PL1 = 1f;

    [Header("Stock")]
    public TMP_InputField StockVolumeInput;
    public float default_SV = 31.41593f;

    [Header("Pipe2")]
    public TMP_InputField Pipe2RadiusInput;
    public float default_PR2 = 1f;
    public TMP_InputField Pipe2LengthInput;
    public float default_PL2 = 1f;

    [Header("Water")]
    public TMP_InputField InitTempInput;
    public float default_IT = 0;
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
        SetDefaultInput();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHeaterVolume()
    {
        if (HeaterVolumeInput.text == "")
        {
            HeaterVolumeInput.text = HeatingSystem.Instance.VolumeOfHeater.ToString();
            return;
        }
        float dataFromInput = float.Parse(HeaterVolumeInput.text);
        if (dataFromInput <= 0)
        {
            HeaterVolumeInput.text = HeatingSystem.Instance.VolumeOfHeater.ToString();
            return;
        }
        HeatingSystem.Instance.VolumeOfHeater = dataFromInput;
        HeaterVolumeInput.text = dataFromInput.ToString();
    }

    public void SetPipe1Radius()
    {
        if (Pipe1RadiusInput.text == "")
        {
            Pipe1RadiusInput.text = HeatingSystem.Instance.RadiusOfPipe1.ToString();
            return;
        }
        float dataFromInput = float.Parse(Pipe1RadiusInput.text);
        if (dataFromInput <= 0)
        {
            Pipe1RadiusInput.text = HeatingSystem.Instance.RadiusOfPipe1.ToString();
            return;
        }
        HeatingSystem.Instance.RadiusOfPipe1 = dataFromInput;
        Pipe1RadiusInput.text = dataFromInput.ToString();
    }

    public void SetPipe1Length()
    {
        if (Pipe1LengthInput.text == "")
        {
            Pipe1LengthInput.text = HeatingSystem.Instance.LengthOfPipe1.ToString();
            return;
        }
        float dataFromInput = float.Parse(Pipe1LengthInput.text);
        if (dataFromInput <= 0)
        {
            Pipe1LengthInput.text = HeatingSystem.Instance.LengthOfPipe1.ToString();
            return;
        }
        HeatingSystem.Instance.LengthOfPipe1 = dataFromInput;
        Pipe1LengthInput.text = dataFromInput.ToString();
    }

    public void SetPipe2Radius()
    {
        if (Pipe2RadiusInput.text == "")
        {
            Pipe2RadiusInput.text = HeatingSystem.Instance.RadiusOfPipe2.ToString();
            return;
        }
        float dataFromInput = float.Parse(Pipe2RadiusInput.text);
        if (dataFromInput <= 0)
        {
            Pipe2RadiusInput.text = HeatingSystem.Instance.RadiusOfPipe2.ToString();
            return;
        }
        HeatingSystem.Instance.RadiusOfPipe2 = dataFromInput;
        Pipe2RadiusInput.text = dataFromInput.ToString();
    }

    public void SetPipe2Length()
    {
        if (Pipe2LengthInput.text == "")
        {
            Pipe2LengthInput.text = HeatingSystem.Instance.LengthOfPipe2.ToString();
            return;
        }
        float dataFromInput = float.Parse(Pipe2LengthInput.text);
        if (dataFromInput <= 0)
        {
            Pipe2LengthInput.text = HeatingSystem.Instance.LengthOfPipe2.ToString();
            return;
        }
        HeatingSystem.Instance.LengthOfPipe2 = dataFromInput;
        Pipe2LengthInput.text = dataFromInput.ToString();
    }

    public void SetStockVolume()
    {
        if (StockVolumeInput.text == "")
        {
            StockVolumeInput.text = HeatingSystem.Instance.VolumeOfStock.ToString();
            return;
        }
        float dataFromInput = float.Parse(StockVolumeInput.text);
        if (dataFromInput <= 0)
        {
            StockVolumeInput.text = HeatingSystem.Instance.VolumeOfStock.ToString();
            return;
        }
        HeatingSystem.Instance.VolumeOfStock = dataFromInput;
        StockVolumeInput.text = dataFromInput.ToString();
    }

    public void SetWaterInitTemp()
    {
        if (InitTempInput.text == "")
        {
            InitTempInput.text = HeatingSystem.Instance.InitTemp.ToString();
            return;
        }
        float dataFromInput = float.Parse(InitTempInput.text);
        if (dataFromInput < 0)
        {
            InitTempInput.text = HeatingSystem.Instance.InitTemp.ToString();
            return;
        }
        HeatingSystem.Instance.InitTemp = dataFromInput;
        InitTempInput.text = dataFromInput.ToString();

        HeatingSystem.Instance.TempHeaterMax = dataFromInput;
        HeatingSystem.Instance.TempHeaterAvrage = dataFromInput;
        HeatingSystem.Instance.TempHeaterMin = dataFromInput;

        HeatingSystem.Instance.TempStockMax = dataFromInput;
        HeatingSystem.Instance.TempStockAvrage = dataFromInput;
        HeatingSystem.Instance.TempStockMin = dataFromInput;

        WaterCubeGenerator.Instance.ResetWaterCubes();
    }

    public void SetHeaterPower()
    {
        if (HeaterPowerInput.text == "")
        {
            HeaterPowerInput.text = "0";
            HeatingSystem.Instance.PowerOfHeater = 0;
            return;
        }
        float dataFromInput = float.Parse(HeaterPowerInput.text);
        if (dataFromInput < 0)
        {
            HeaterPowerInput.text = HeatingSystem.Instance.PowerOfHeater.ToString();
            return;
        }
        HeatingSystem.Instance.PowerOfHeater = dataFromInput;
        HeaterPowerInput.text = dataFromInput.ToString();
    }

    public void SetFlowRate()
    {
        if (ValveFlowRateInput.text == "")
        {
            ValveFlowRateInput.text = "0";
            HeatingSystem.Instance.VolumeFlowRate = 0;
            return;
        }
        float dataFromInput = float.Parse(ValveFlowRateInput.text);
        if (dataFromInput < 0)
        {
            ValveFlowRateInput.text = HeatingSystem.Instance.VolumeFlowRate.ToString();
            return;
        }
        HeatingSystem.Instance.VolumeFlowRate = dataFromInput;
        ValveFlowRateInput.text = dataFromInput.ToString();
    }

    public void SetDefaultInput()
    {
        HeatingSystem.Instance.VolumeOfHeater = default_HV;
        HeatingSystem.Instance.LengthOfPipe1 = default_PL1;
        HeatingSystem.Instance.RadiusOfPipe1 = default_PR1;
        HeatingSystem.Instance.LengthOfPipe2 = default_PL2;
        HeatingSystem.Instance.RadiusOfPipe2 = default_PR2;
        HeatingSystem.Instance.VolumeOfStock = default_SV;
        HeatingSystem.Instance.InitTemp = default_IT;
        HeatingSystem.Instance.VolumeFlowRate = default_VFR;
        HeatingSystem.Instance.PowerOfHeater = defualt_HP;

        HeaterVolumeInput.text = default_HV.ToString();
        Pipe1LengthInput.text = default_PL1.ToString();
        Pipe1RadiusInput.text = default_PR1.ToString();
        Pipe2LengthInput.text = default_PL2.ToString();
        Pipe2RadiusInput.text = default_PR2.ToString();
        StockVolumeInput.text = default_SV.ToString();
        InitTempInput.text = default_IT.ToString();
        ValveFlowRateInput.text = default_VFR.ToString();
        HeaterPowerInput.text = defualt_HP.ToString();
    }


    public void ActivateStaticInput(bool isActivate)
    {
        HeaterVolumeInput.gameObject.SetActive(isActivate);

        Pipe1LengthInput.gameObject.SetActive(isActivate);
        Pipe1RadiusInput.gameObject.SetActive(isActivate);

        Pipe2LengthInput.gameObject.SetActive(isActivate);
        Pipe2RadiusInput.gameObject.SetActive(isActivate);

        StockVolumeInput.gameObject.SetActive(isActivate); 

        InitTempInput.gameObject.SetActive(isActivate);
    }
}

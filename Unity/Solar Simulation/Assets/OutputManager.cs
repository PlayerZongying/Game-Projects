using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutputManager : MonoBehaviour
{
    public static OutputManager Instance;

    [Header("static during simulation")]
    public TextMeshProUGUI OutputHeaterVolume;
    public TextMeshProUGUI OutputPipe1Radius;
    public TextMeshProUGUI OutputPipe1Length;
    public TextMeshProUGUI OutputPipe2Radius;
    public TextMeshProUGUI OutputPipe2Length;
    public TextMeshProUGUI OutputStockVolume;
    public TextMeshProUGUI OutputInitTemp;

    [Header("dynamic during simulation")]
    public TextMeshProUGUI OutputHeaterMaxT;
    public TextMeshProUGUI OutputHeaterAvgT;
    public TextMeshProUGUI OutputHeaterMinT;
    public TextMeshProUGUI OutputStockMaxT;
    public TextMeshProUGUI OutputStockAvgT;
    public TextMeshProUGUI OutputStockMinT;
    //public TextMeshProUGUI OutputHeaterPower;
    //public TextMeshProUGUI OutputFlowRate;
    public TextMeshProUGUI OutputSimulationTime;



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

    public void SetStaticOutputText()
    {
        OutputHeaterVolume.text = HeatingSystem.Instance.VolumeOfHeater.ToString();
        OutputPipe1Radius.text = HeatingSystem.Instance.RadiusOfPipe1.ToString();
        OutputPipe1Length.text = HeatingSystem.Instance.LengthOfPipe1.ToString();
        OutputPipe2Radius.text = HeatingSystem.Instance.RadiusOfPipe2.ToString();
        OutputPipe2Length.text = HeatingSystem.Instance.LengthOfPipe2.ToString();
        OutputStockVolume.text = HeatingSystem.Instance.VolumeOfStock.ToString();
        OutputInitTemp.text = HeatingSystem.Instance.InitTemp.ToString();
    }
    public void UpdateDynamicOutputText()
    {
        // temperature
        float temp;
        float t;

        temp = HeatingSystem.Instance.TempHeaterMax;
        t = Mathf.InverseLerp(0, 100, temp);
        OutputHeaterMaxT.color = WaterCubeGenerator.Instance.ColorGradient.Evaluate(t);
        temp = Mathf.Round(temp * 100f) / 100f;
        OutputHeaterMaxT.text = temp.ToString();

        temp = HeatingSystem.Instance.TempHeaterAvrage;
        t = Mathf.InverseLerp(0, 100, temp);
        OutputHeaterAvgT.color = WaterCubeGenerator.Instance.ColorGradient.Evaluate(t);
        temp = Mathf.Round(temp * 100f) / 100f;
        OutputHeaterAvgT.text = temp.ToString();

        temp = HeatingSystem.Instance.TempHeaterMin;
        t = Mathf.InverseLerp(0, 100, temp);
        OutputHeaterMinT.color = WaterCubeGenerator.Instance.ColorGradient.Evaluate(t);
        temp = Mathf.Round(temp * 100f) / 100f;
        OutputHeaterMinT.text = temp.ToString();

        temp = HeatingSystem.Instance.TempStockMax;
        t = Mathf.InverseLerp(0, 100, temp);
        OutputStockMaxT.color = WaterCubeGenerator.Instance.ColorGradient.Evaluate(t);
        temp = Mathf.Round(temp * 100f) / 100f;
        OutputStockMaxT.text = temp.ToString();

        temp = HeatingSystem.Instance.TempStockAvrage;
        t = Mathf.InverseLerp(0, 100, temp);
        OutputStockAvgT.color = WaterCubeGenerator.Instance.ColorGradient.Evaluate(t);
        temp = Mathf.Round(temp * 100f) / 100f;
        OutputStockAvgT.text = temp.ToString();

        temp = HeatingSystem.Instance.TempStockMin;
        t = Mathf.InverseLerp(0, 100, temp);
        OutputStockMinT.color = WaterCubeGenerator.Instance.ColorGradient.Evaluate(t);
        temp = Mathf.Round(temp * 100f) / 100f;
        OutputStockMinT.text = temp.ToString();

        // time
        float time = HeatingSystem.Instance.SimulationTime;
        time = Mathf.Round(time * 100f) / 100f;
        OutputSimulationTime.text = time.ToString();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TextMeshProUGUI StartTMP;
    public TextMeshProUGUI TipsTMP1;
    public TextMeshProUGUI TipsTMP2;
    public TextMeshProUGUI TipsTMP3;
    public Canvas EndingCanvas;

    public Button reloadBtn;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
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

    public void Clear()
    {
        StartTMP.gameObject.SetActive(false);
        TipsTMP1.gameObject.SetActive(false);
        TipsTMP2.gameObject.SetActive(false);
        TipsTMP3.gameObject.SetActive(false);
        EndingCanvas.gameObject.SetActive(false);
    }

    public void ShowStart()
    {
        Clear();
        StartTMP.gameObject.SetActive(true);
    }
    public void ShowTips1()
    {
        Clear();
        TipsTMP1.gameObject.SetActive(true);
    }
    public void ShowTips2()
    {
        Clear();
        TipsTMP2.gameObject.SetActive(true);
    }
    public void ShowTips3()
    {
        Clear();
        TipsTMP3.gameObject.SetActive(true);
    }
    public void ShowEnd()
    {
        Clear();
        EndingCanvas.gameObject.SetActive(true);
    }

    public void ShowButton()
    {
        reloadBtn.gameObject.SetActive(true);
    }

    public void SetTips1Text(string s)
    {
        TipsTMP1.text = s;
    }
}

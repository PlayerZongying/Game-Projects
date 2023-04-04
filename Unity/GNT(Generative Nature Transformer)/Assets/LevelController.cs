using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public List<Dragger> Movers;

    public List<Filler> Fillers;

    public List<Construction> Constructions;

    public GameObject ColliderStart;
    public GameObject ColliderForMovers;
    public GameObject ColliderForFillers;
    public GameObject ColliderForConstructions;

    public GameObject result;

    UIController UIC;

    bool Started = false;

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
        UIC = UIController.instance;
        UIC.ShowStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Started && Input.GetMouseButtonDown(0))
        {
            Started = true;
            ClearColliders();
            ColliderForMovers.SetActive(true);
            UIC.ShowTips1();
            UIC.ShowButton();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public int MoversIntargetCount()
    {
        int i = 0;
        foreach (Dragger mover in Movers)
        {
            if (mover.isInTarget) i++;
        }
        return i;
    }

    public void CheckMoversIntarget()
    {
        //foreach (Dragger mover in Movers)
        //{
        //    if (!mover.isInTarget) return;
        //}
        int i = MoversIntargetCount();

        if (i == Movers.Count)
        {
            ShowFiller();
            UIC.ShowTips2();
            ClearColliders();
            ColliderForFillers.SetActive(true);
        }
        else
        {
            UIC.SetTips1Text("ÍÏ×§ÄÌÅ££¬×°µãÌì¿Õ£¨" + i + "/" + Movers.Count + "£©");
        }


    }

    void ShowFiller()
    {
        foreach (Dragger filler in Fillers)
        {
            float x = Random.Range(-5, 5);
            float y = Random.Range(-4, -2);
            filler.gameObject.transform.position = new Vector3(x, y, 0);
            filler.gameObject.SetActive(true);
        }
    }

    public void CheckFillerIntarget()
    {
        foreach (Filler filler in Fillers)
        {
            if (!filler.isInTarget) return;
        }


        PrepareConstructor();
        UIC.ShowTips3();

        ClearColliders();
        ColliderForConstructions.SetActive(true);
    }

    private void PrepareConstructor()
    {
        foreach (Filler filler in Fillers)
        {
            filler.gameObject.SetActive(false);
        }

        foreach (Dragger construction in Constructions)
        {
            float x = Random.Range(-5, 5);
            float y = Random.Range(-4, -2);
            construction.gameObject.transform.position = new Vector3(x, y, 0);
            construction.gameObject.SetActive(true);
        }

        ColliderForConstructions.SetActive(true);

    }

    public void CheckConstructionsIntarget()
    {
        foreach (Construction c in Constructions)
        {
            if (!c.isInTarget) return;
        }


        ShowFinalResult();
        UIC.ShowEnd();
    }

    private void ShowFinalResult()
    {
        result.SetActive(true);
    }


    private void ClearColliders()
    {
        ColliderStart.gameObject.SetActive(false);
        ColliderForMovers.gameObject.SetActive(false);
        ColliderForFillers.gameObject.SetActive(false);
        ColliderForConstructions.gameObject.SetActive(false);
    }
}

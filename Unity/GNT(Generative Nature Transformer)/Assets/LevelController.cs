using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public List<Dragger> Movers;

    public List<Filler> Fillers;

    public List<Construction> Constructions;

    public GameObject ColliderForConstructions;

    public GameObject result;


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

    public void CheckMoversIntarget()
    {
        foreach(Dragger mover in Movers)
        {
            if (!mover.isInTarget) return;
        }


        ShowFiller();
    }

    void ShowFiller()
    {
        foreach(Dragger filler in Fillers)
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
    }

    private void PrepareConstructor()
    {
        foreach(Filler filler in Fillers)
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
    }

    private void ShowFinalResult()
    {
        result.SetActive(true);
    }
}

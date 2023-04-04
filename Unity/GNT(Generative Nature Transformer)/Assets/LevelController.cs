using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public List<Dragger> Movers;

    public List<Dragger> Fillers;

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
            float y = Random.Range(-4, 4);
            filler.gameObject.transform.position = new Vector3(x, y, 0);
            filler.gameObject.SetActive(true);
        }
    }
}

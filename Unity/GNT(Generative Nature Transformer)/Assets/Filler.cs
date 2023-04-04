using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filler : Dragger
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    base.Start();
    //}


    protected override void OnMouseUp()
    {
        if (!isInTarget)
        {
            StartCoroutine(Back(targetPos));
        }
        else
        {
            dragable = false;
            lctrl.CheckFillerIntarget();
        }
    }

    
}

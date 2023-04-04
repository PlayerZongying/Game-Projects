using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Dragger
{
    protected override void OnMouseUp()
    {
        if (!isInTarget)
        {
            StartCoroutine(Back(targetPos));
        }
        else
        {
            dragable = false;
            lctrl.CheckConstructionsIntarget();
        }
    }
}

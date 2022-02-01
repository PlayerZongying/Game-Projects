using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public DotsGenerator dotsGenerator;
    public DotsContainer dotsContainer;
    // Start is called before the first frame update
    void Start()
    {
        dotsContainer.ContainerInit();
        dotsGenerator.GeneratorInit();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

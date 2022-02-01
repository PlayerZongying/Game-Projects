using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Camera cam;
    private Ray ray;
    private RaycastHit hitInfo;

    private List<GameObject> selectedDots;
    private Color selectedColor;
    private int curIndex;
    private int prevIndex;
    private bool isLoop;
    private int loopIndex;

    public GameObject pathUnitPrefab;
    public GameObject pathObject;
    public float maxPathGaping = 1.2f;
    private List<GameObject> path;

    //private HashSet<GameObject>

    void Start()
    {
        cam = Camera.main;
        Reset();
    }

    void Update()
    {
        SelectDots();
        DestroyDots();

    }

    public void SelectDots()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        // select an object when mouse is down
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            // if selected object is a dot
            if (hitInfo.collider.gameObject.GetComponent<Dot>())
            {
                GameObject selectedDot = hitInfo.collider.gameObject;

                // if there is no dots selected, then add the dot, determin the path clor;
                if (selectedDots.Count == 0)
                {
                    selectedDots.Add(selectedDot);
                    curIndex++;
                    prevIndex++;

                    selectedColor = selectedDot.GetComponent<Dot>().color;
                    Debug.Log("Dot " + curIndex + "is " + selectedDot);
                }

                // else, there are already dots selected
                else
                {   // add a new, same colored, adjacent dot 
                    if (selectedDots[curIndex] != selectedDot
                        && selectedColor == selectedDot.GetComponent<Dot>().color
                        && Vector3.Distance(selectedDots[curIndex].transform.position, selectedDot.transform.position) < maxPathGaping)
                    {
                        // if currently only one dot in the path, then just add the new dot 
                        if (selectedDots.Count == 1)
                        {
                            selectedDots.Add(selectedDot);
                            curIndex++;
                            prevIndex++;
                            Debug.Log("Dot " + curIndex + "is " + selectedDot);

                            PathAppend();
                        }

                        // else if there are more than one dot in the path, need to consider 3 situations:
                        // 1. going back, pop out the last one from the path
                        // 2. going forward, generating loop
                        // 3. going forward, not generating loop
                        else
                        {
                            // 1. going back, pop out the last dot from the path
                            if (selectedDot == selectedDots[prevIndex])
                            {

                                Debug.Log(selectedDots[curIndex] + " at " + curIndex + " is removed");
                                selectedDots.RemoveAt(curIndex);
                                curIndex--;
                                prevIndex--;
                                if (curIndex < loopIndex)
                                {
                                    isLoop = false;
                                    loopIndex = -1;
                                    Debug.Log("No Loop now");
                                }

                                Destroy(path[curIndex]);
                                path.RemoveAt(curIndex);

                            }

                            // 2. going forward, generating loop
                            else if (selectedDots.Contains(selectedDot) && !isLoop)
                            {
                                selectedDots.Add(selectedDot);
                                curIndex++;
                                prevIndex++;
                                isLoop = true;
                                loopIndex = curIndex;
                                Debug.Log("Dot " + curIndex + "is " + selectedDot);
                                Debug.Log("There is a Loop");

                                PathAppend();

                            }

                            // 3. going forward, not generating loop
                            else
                            {
                                selectedDots.Add(selectedDot);
                                curIndex++;
                                prevIndex++;
                                Debug.Log("Dot " + curIndex + "is " + selectedDot);

                                PathAppend();
                            }
                        }
                    }
                }
            }
        }
    }

    public void DestroyDots()
    {
        List<GameObject> modifiedGenerators = new List<GameObject>();

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("up");

            //
            if (selectedDots.Count > 1)
            {
                if (!isLoop)
                {
                    foreach (GameObject dot in selectedDots)
                    {
                        //Debug.Log(dot.transform.parent.name);
                        GameObject generator = dot.transform.parent.gameObject;
                        if (!modifiedGenerators.Contains(generator))
                        {
                            modifiedGenerators.Add(generator);
                        }
                        DestroyImmediate(dot, true);
                    }
                    Debug.Log("selectedDots.Count = " + selectedDots.Count);
                    Debug.Log("more than 1 dots has been destroyed");
                }

                else
                {
                    Dot[] allDots = FindObjectsOfType<Dot>();
                    int i = 0;
                    foreach (Dot dot in allDots)
                    {
                        if (dot.color == selectedColor)
                        {
                            Debug.Log(dot.gameObject.transform.parent.name);
                            GameObject generator = dot.gameObject.transform.parent.gameObject;
                            if (!modifiedGenerators.Contains(generator))
                            {
                                modifiedGenerators.Add(generator);
                            }
                            DestroyImmediate(dot.gameObject, true);
                            i++;
                        }
                    }
                    Debug.Log(i + " " + selectedColor + " dots are destroyed");

                }

                foreach (GameObject generator in modifiedGenerators)
                {
                    generator.GetComponent<DotsGenerator>().RelocateAndRefillDots();
                }
            }

            Reset();
        }
    }

    public void Reset()
    {
        selectedDots = new List<GameObject>();
        curIndex = -1;
        prevIndex = -2;
        isLoop = false;
        loopIndex = -1;

        if (path != null)
        {
            foreach (GameObject pathUnit in path)
            {
                Destroy(pathUnit);
            }
        }
        path = new List<GameObject>();
    }

    public void PathAppend()
    {
        pathUnitPrefab.GetComponent<SpriteRenderer>().color = selectedColor;
        GameObject pathUnit = Instantiate(
                                  pathUnitPrefab,
                                  (selectedDots[curIndex].transform.position + selectedDots[prevIndex].transform.position) / 2,
                                  Quaternion.identity);
        pathUnit.transform.parent = pathObject.transform;
        path.Add(pathUnit);
    }


}

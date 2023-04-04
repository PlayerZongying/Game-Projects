using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    protected Camera camera;

    public Transform pivot;

    Vector3 offsetToPivot;
    Vector3 offsetToMouse;

    public Collider2D TargetCollider;
    protected Vector3 targetPos;
    protected Vector3 startPos;

    public bool dragable;
    public bool isInTarget;

    protected LevelController lctrl;

    protected void Start()
    {
        camera = Camera.main;
        offsetToPivot = pivot.position - this.transform.position;
        dragable = true;
        startPos = this.transform.position;
        targetPos = startPos;
        lctrl = LevelController.instance;
    }

    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name);
        offsetToMouse = pivot.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        if (dragable)
        {
            transform.position = GetMousePosition() - offsetToPivot + offsetToMouse;
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    virtual protected void OnMouseUp()
    {
        if (!isInTarget)
        {
            StartCoroutine(Back(targetPos));
        }
        else
        {
            dragable = false;
            lctrl.CheckMoversIntarget();
        }

    }

    protected IEnumerator Back(Vector3 targetPos)
    {
        if (!dragable)
        {
            yield break;
        }

        Debug.Log("back!!!");
        dragable = false;
        Vector3 startPos = this.transform.position;
        float T = 0;
        float t = 0;

        while (t < 0.99)
        {
            T += Time.deltaTime;
            t = Mathf.Sin((T - 0.5f) * Mathf.PI) / 2 + 0.5f;
            this.transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return new WaitForEndOfFrame();
        }
        this.transform.position = targetPos;
        dragable = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        //targetPos = collision.gameObject.transform.position - offsetToPivot;
        if (collision == TargetCollider)
        {
            isInTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTarget = false;
        targetPos = startPos;
    }

}

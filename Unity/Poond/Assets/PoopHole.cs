using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopHole : MonoBehaviour
{
    public GameObject Poop;
    public float followSpeed;
    public Trajectory trajectory;
    public Vector3 vel;
    [Range(-100f, 0f)]
    public float baseGravity;
    public Vector3 gravity;
    public float skyglobeRadius;
    public GameObject target;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        target.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        DetermineTargetPoint();
        PointPoopToMouse();
        DrawTrajectory();
        if (Input.GetMouseButtonDown(0))
        {
            PoopOutAPoop();
        }
        UpdatePoop();

    }

    void PointPoopToMouse()
    {
        Poop.transform.forward = target.transform.position - this.transform.position;
    }

    void DrawTrajectory()
    {
        float dist = Vector3.Distance(targetPosition, this.transform.position);
        gravity.y = baseGravity - Mathf.Sqrt(dist);

        float vx = target.transform.position.x - this.transform.position.x;
        float vy = target.transform.position.y - this.transform.position.y - 0.5f * gravity.y;
        float vz = target.transform.position.z - this.transform.position.z;
        vel = new Vector3(vx, vy, vz);

        for (int i = 0; i < trajectory.PointCount; i++)
        {
            float t = Mathf.Pow((float)i / trajectory.PointCount, 2) * Mathf.Sqrt(skyglobeRadius / dist);
            if (t <= 1)
            {
                Vector3 pointPos = this.transform.position + vel * t + 0.5f * gravity * t * t;
                trajectory.Points[i].transform.position = trajectory.transform.localRotation * pointPos;
            }
            else
            {
                Vector3 pointPos = Vector3.zero;
                trajectory.Points[i].transform.position = trajectory.transform.localRotation * pointPos;
            }
        }
    }

    void DetermineTargetPoint()
    {
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        Vector3 mouseDir = mousePosInWorld - Camera.main.transform.position;
        mouseDir = Vector3.Normalize(mouseDir);

        Vector3 hitPosition;
        // determin the hit point
        if (mouseDir.y < 0)//has hit point on plane y = 0
        {
            float t = 0f - Camera.main.transform.position.y / mouseDir.y;
            hitPosition = Camera.main.transform.position + t * mouseDir;
            //Debug.Log("hit ground: " + hitPosition.magnitude);

            if (hitPosition.magnitude > skyglobeRadius)
            {
                Vector3 nearestPoint = Vector3.Dot(mouseDir, Vector3.zero - Camera.main.transform.position) * mouseDir + Camera.main.transform.position;
                float distToSkyglobe = Mathf.Sqrt(skyglobeRadius * skyglobeRadius - nearestPoint.magnitude * nearestPoint.magnitude);
                //Debug.Log("blur lines distTosky: " + distToSkyglobe);
                hitPosition = nearestPoint + mouseDir * distToSkyglobe;
                //targetPosition = Vector3.Lerp(targetPosition, hitPosition, followSpeed * Time.deltaTime);
                //target.transform.position = targetPosition;

                //Debug.Log("blur lines: " + hitPosition.magnitude);

            }
        }
        else
        {
            //calculate the hit point on the sky globe with r = skyglobeRadius
            Vector3 nearestPoint = Vector3.Dot(mouseDir, Vector3.zero - Camera.main.transform.position) * mouseDir + Camera.main.transform.position;
            float distToSkyglobe = Mathf.Sqrt(skyglobeRadius * skyglobeRadius - nearestPoint.magnitude * nearestPoint.magnitude);
            hitPosition = nearestPoint + mouseDir * distToSkyglobe;
            //targetPosition = Vector3.Lerp(targetPosition, hitPosition, followSpeed * Time.deltaTime);
            //target.transform.position = targetPosition;
            //Debug.Log("hit sky: " + hitPosition.magnitude);
        }

        //derive target position from hit point
        targetPosition = Vector3.Lerp(targetPosition, hitPosition, followSpeed * Time.deltaTime);
        target.transform.position = targetPosition;

    }

    void PoopOutAPoop()
    {
        PoopPool.TakeAPoop(this.transform.position, vel, gravity, Mathf.Sqrt(Vector3.Distance(this.transform.position, targetPosition)));
    }

    void UpdatePoop()
    {
        LinkedListNode<Poop> currentPoop = PoopPool.instance.flyingPoop.First;
        while (currentPoop != null)
        {
            currentPoop.Value.MoveAlongTrajectory();

            if (currentPoop.Value.gameObject.activeSelf == false)
            {
                LinkedListNode<Poop> nextPoop = currentPoop.Next;
                PoopPool.instance.flyingPoop.Remove(currentPoop);
                currentPoop = nextPoop;
            }
            else
            {
                currentPoop = currentPoop.Next;
            }
        }

        Debug.Log("Flying Poop Count: " + PoopPool.instance.flyingPoop.Count);
    }

}

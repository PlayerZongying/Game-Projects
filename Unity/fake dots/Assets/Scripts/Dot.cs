using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public Point targetPoint = null;
    public float velocity = 10f;
    public Color color;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTargetPoint();
    }

    private void MoveToTargetPoint()
    {
        if (targetPoint && transform.position != targetPoint.transform.position)
        {
            Vector3 currentPointPosition = targetPoint.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, currentPointPosition, velocity * Time.deltaTime);

        }
        else
        {
            animator.SetBool("isAtTarget", true);
        }
    }
}

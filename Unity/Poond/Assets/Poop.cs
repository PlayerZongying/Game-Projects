using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    Vector3 startPos;
    Vector3 velocity;
    Vector3 gravity;
    float timeToFinish;

    Vector3 randomEulerAngles;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPoop(Vector3 _startPos, Vector3 _velocity, Vector3 _gravity, float _timToFinish)
    {
        this.t = 0;
        this.startPos = _startPos;
        this.velocity = _velocity;
        this.gravity = _gravity;
        this.timeToFinish = _timToFinish;

        randomEulerAngles = new Vector3(Random.Range(-360.0f, 360.0f), Random.Range(-360.0f, 360.0f), Random.Range(-360.0f, 360.0f));
    }

    public void MoveAlongTrajectory()
    {
        this.transform.position = startPos + velocity * t + 0.5f * gravity * t * t;
        
        this.transform.rotation *= Quaternion.Euler(randomEulerAngles * Time.deltaTime);
        t += Time.deltaTime / timeToFinish;
        if(t > 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination()
    {
        this.gameObject.SetActive(false);
    }
}

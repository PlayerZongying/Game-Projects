using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIGH : MonoBehaviour
{
    [SerializeField] Mic mic = null;
    [SerializeField] AudioClip highSFX = null;

    // Start is called before the first frame update
    void Start()
    {
        for(int line = 1; line < 6; line++)
        {
            Mic newMic = Instantiate(mic, new Vector2(0,line), transform.rotation);
            newMic.transform.parent = transform;
        }

        AudioSource.PlayClipAtPoint(highSFX, Camera.main.transform.position);

        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(9);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

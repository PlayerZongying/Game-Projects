using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public GameObject NotePrefab;
    public int NoteCount = 10;
    public float HalvorsenParam = 1.4f;
    public float speed = 0.1f;
    public GameObject AllNotes;
    List<GameObject> NoteList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;

        for (int i = 0; i < NoteCount; i++)
        {

            GameObject newNote = Instantiate(NotePrefab, RomdomPos(), Quaternion.identity);
            newNote.transform.parent = AllNotes.transform;
            NoteList.Add(newNote);
        }

    }


    // Update is called once per frame
    void Update()
    {
        //speed = Mathf.Sin(Time.realtimeSinceStartup) * 0.05f + 0.1f;

        foreach (GameObject note in NoteList)
        {
            if (note.transform.position.magnitude > 1000000)
            {
                Debug.Log(note.name + " is popped out");
                note.transform.position = RomdomPos();
            }
            HalvorsenMove(note);
            //note.transform.LookAt( - Camera.main.transform.position);
            //Debug.Log(Camera.main.transform.position);
        }
    }

    Vector3 RomdomPos()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        Vector3 randomPos = new Vector3(x, y, z);
        return randomPos;
    }

    void HalvorsenMove(GameObject note)
    {
        float x = note.transform.position.x;
        float y = note.transform.position.y;
        float z = note.transform.position.z;

        float dx = -HalvorsenParam * x - 4 * y - 4 * z - y * y;
        float dy = -HalvorsenParam * y - 4 * z - 4 * x - z * z;
        float dz = -HalvorsenParam * z - 4 * x - 4 * y - x * x;

        note.transform.position += new Vector3(dx, dy, dz) * Time.deltaTime * speed;
    }
}

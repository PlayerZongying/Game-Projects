using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorMovement : MonoBehaviour
{
    public static AttractorMovement instance;

    [Header("Halvorsen")]
    public float Halvorsen_a = 1.4f;
    public float speed_Halvorsen = 0.1f;
    public GameObject Halvorsen_Note;
    public Material Mat_Halvorsen_Note;
    public Material Mat_Halvorsen_Note_Trail;
    public GameObject Halvorsen_Touch;

    [Header("Dequanli")]
    public float DequanLi_a = 40;
    public float DequanLi_c = 1.833f;
    public float DequanLi_d = 0.16f;
    public float DequanLi_e = 0.65f;
    public float DequanLi_k = 55;
    public float DequanLi_f = 20;
    public float speed_DequanLi = 0.02f;

    [Header("Aizawa")]
    public float Aizawa_a = 0.95f;
    public float Aizawa_b = 0.7f;
    public float Aizawa_c = 0.6f;
    public float Aizawa_d = 3.5f;
    public float Aizawa_e = 0.25f;
    public float Aizawa_f = 0.1f;
    public float speed_Aizawa = 0.1f;

    [Header("Lorenz")]
    public float Lorenz_sigma = 10;
    public float Lorenz_rho = 28;
    public float Lorenz_beta = 8 / 3;
    public float speed_Lorenz;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void HalvorsenMove(Transform transform, GameObject note)
    {
        //float x = note.transform.position.x;
        //float y = note.transform.position.y;
        //float z = note.transform.position.z;

        //float dx = -param_Halvorsen * x - 4 * y - 4 * z - y * y;
        //float dy = -param_Halvorsen * y - 4 * z - 4 * x - z * z;
        //float dz = -param_Halvorsen * z - 4 * x - 4 * y - x * x;

        //note.transform.position += new Vector3(dx, dy, dz) * Time.deltaTime * speed;


        Vector3 worldPos = note.transform.position;
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        float x = localPos.x;
        float y = localPos.y;
        float z = localPos.z;

        float dx = -Halvorsen_a * x - 4 * y - 4 * z - y * y;
        float dy = -Halvorsen_a * y - 4 * z - 4 * x - z * z;
        float dz = -Halvorsen_a * z - 4 * x - 4 * y - x * x;

        localPos += new Vector3(dx, dy, dz) * Time.deltaTime * speed_Halvorsen;

        worldPos = transform.TransformPoint(localPos);

        note.transform.position = worldPos;
    }

    public void DequanLiMove(Transform transform, GameObject note)
    {
        Vector3 worldPos = note.transform.position;
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        float x = localPos.x;
        float y = localPos.y;
        float z = localPos.z;

        float dx = DequanLi_a * (y - x) + DequanLi_d * x * z;
        float dy = DequanLi_k * x + DequanLi_f * y - x * z;
        float dz = DequanLi_c * z + x * y - DequanLi_e * x * x;

        localPos += new Vector3(dx, dy, dz) * Time.deltaTime * speed_DequanLi;

        worldPos = transform.TransformPoint(localPos);

        note.transform.position = worldPos;
    }

    public void AizawaMove(Transform transform, GameObject note)
    {
        Vector3 worldPos = note.transform.position;
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        float x = localPos.x;
        float y = localPos.y;
        float z = localPos.z;

        float dx = (z - Aizawa_b) * x - Aizawa_d * y;
        float dy = Aizawa_d * x + (z - Aizawa_b) * y;
        float dz =
            Aizawa_c
            + Aizawa_a * z
            - Mathf.Pow(z, 3) / 3
            - (x * x + y * y) * (1 + Aizawa_e * z)
            + Aizawa_f * z * x * x * x;

        localPos += new Vector3(dx, dy, dz) * Time.deltaTime * speed_Aizawa;

        worldPos = transform.TransformPoint(localPos);

        note.transform.position = worldPos;
    }

    public void LorenzMove(Transform transform, GameObject note)
    {
        Vector3 worldPos = note.transform.position;
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        float x = localPos.x;
        float y = localPos.y;
        float z = localPos.z;

        float dx = Lorenz_sigma * (y - x);
        float dy = x * (Lorenz_rho - z) - y;
        float dz = x * y - Lorenz_beta * z;

        localPos += new Vector3(dx, dy, dz) * Time.deltaTime * speed_Lorenz;

        worldPos = transform.TransformPoint(localPos);

        note.transform.position = worldPos;
    }
}

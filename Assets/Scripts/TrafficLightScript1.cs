using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightScript1 : MonoBehaviour
{
    public float redTime = 5.0f; // Waktu untuk lampu merah
    public float yellowTime = 2.0f; // Waktu untuk lampu kuning
    public float greenTime = 3.0f; // Waktu untuk lampu hijau

    private float timeSinceLastLightChange = 0.0f; // Waktu yang telah berlalu sejak pergantian lampu terakhir
    private int currentLightIndex = 0; // Indeks lampu yang sedang menyala saat ini

    private List<GameObject> lights = new List<GameObject>(); // List untuk menyimpan GameObject lampu

    // Start is called before the first frame update
    void Start()
    {
        // Mengisi list lights dengan GameObject lampu
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<Light>() != null)
            {
                lights.Add(child);
                child.SetActive(false); // Mematikan semua lampu pada awalnya
            }
        }
        lights[0].SetActive(true); // Mengaktifkan lampu merah pada awalnya
    }

    // Update is called once per frame
    void Update()
    {
        // Mengecek apakah sudah waktunya untuk mengubah lampu
        if (Time.time - timeSinceLastLightChange >= GetCurrentLightTime())
        {
            // Mematikan lampu sebelumnya dan mengaktifkan lampu baru
            lights[currentLightIndex].SetActive(false);
            currentLightIndex = (currentLightIndex + 1) % lights.Count;
            lights[currentLightIndex].SetActive(true);

            // Mengatur ulang waktu sejak pergantian lampu terakhir
            timeSinceLastLightChange = Time.time;
        }
    }

    // Mendapatkan waktu yang sesuai untuk lampu saat ini
    private float GetCurrentLightTime()
    {
        switch (currentLightIndex)
        {
            case 0: // Lampu merah
                return greenTime;
            case 1: // Lampu kuning
                return yellowTime;
            case 2: // Lampu hijau
                return redTime;
            default:
                return 0.0f;
        }
    }
}
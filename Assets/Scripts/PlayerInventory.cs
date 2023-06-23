using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI GasScoreText;
    public TextMeshProUGUI notificationText;
    public AudioSource Suara;
    public int maxGasCan = 5; // Batas maksimum item yang dapat diambil
    private int numberOfGasCan;
    private bool gameEnded = false;

    public int NumberOfGasCan
    {
        get { return numberOfGasCan; }
        private set
        {
            numberOfGasCan = Mathf.Clamp(value, 0, maxGasCan); // Memastikan jumlah item tidak melebihi batas maksimum
            GasScoreText.text = numberOfGasCan.ToString();

            if (numberOfGasCan == maxGasCan && !gameEnded)
            {
                PlayerWins();
            }
        }
    }

    public void GasCanCollected()
    {
        Suara.Play();
        NumberOfGasCan++;
    }

    private void PlayerWins()
    {
        // Menampilkan notifikasi "Kamu menang"
        notificationText.text = "Kamu menang!";
        notificationText.gameObject.SetActive(true);

        // Menghentikan permainan
        Time.timeScale = 0f;
        gameEnded = true;
    }
}

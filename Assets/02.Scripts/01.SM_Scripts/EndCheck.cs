using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCheck : MonoBehaviour
{
    public GameObject ClearWindow;
    public TextMeshProUGUI ClearText;

    //private void Awake()
    //{
    //    ClearWindow.SetActive(false);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ClearWindow.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

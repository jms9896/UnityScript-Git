using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System;

public class TimeChecker : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    CultureInfo cultureInfo = new CultureInfo("ko-kr");

    private void OnEnable()
    {
        OnUpdateDateTime(DateTime.Now);

        AndroidHandler.OnUpdateDateTime += OnUpdateDateTime;
    }

    private void OnDisable() 
    {
        AndroidHandler.OnUpdateDateTime -= OnUpdateDateTime;
    }

    private void OnUpdateDateTime(DateTime now)
    {
        timeText.text = now.ToString("H:mm");
    }
}
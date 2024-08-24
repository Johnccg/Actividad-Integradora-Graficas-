using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    /// <summary>
    /// TextMesh que mostrará el tiemo
    /// </summary>
    public TextMeshProUGUI timeText;
    //Empieza a escuchar por el evento
    private void  OnEnable(){
        TimeManager.Tick += UpdateTime;
        TimeManager.OneHourChanges += UpdateTime;
    }
    //Deja de escuchar por el evento
     private void OnDisable()
    {
        TimeManager.Tick -= UpdateTime;
        TimeManager.OneHourChanges -= UpdateTime;
    }
    /// <summary>
    /// Escribe el tiempo actual en el TextMesh
    /// </summary>
    private void UpdateTime()
    {
        //La forma en la que obtiene el valor de las variables hace que siempre tengan 2 dígitos ej. 09:05
        timeText.text = $"Ticks: {TimeManager.tenSeconds:00}";
    }
}

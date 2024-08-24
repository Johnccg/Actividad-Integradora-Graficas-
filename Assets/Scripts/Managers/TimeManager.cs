using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// Llama un evento po cada tick
    /// </summary>
    public static Action Tick;
    /// <summary>
    /// Nos permitirá lanzar eventos y que sean recibidos por otros objetos
    /// </summary>
    public static Action OneHourChanges;
    /// <summary>
    /// Un entero que representa los minutos, puede ser accesado por cualquiera, pero solo puede ser modificado por TImeManager
    /// </summary>
    public static int tenSeconds{ get; private set; }
    /// <summary>
    /// Factor de vonversión entre el tiempo real en segundos y el tiempo de juego en minutos
    /// </summary>
    private float tickToRealTime = 0.1f;
    ///  <summary>
    /// El intervalo de tiempo antes de actualizar los valores
    /// </summary>
    private float timer;
    void Start()
    {
        tenSeconds = 0;
        timer = tickToRealTime;
    }
    void Update()
    {
        //Restamos al timer el tiempo entre cada frame
        timer -= Time.deltaTime;

        //Si es menor a 0 yasó medio segundo y hacemos los cálculos del tiempo y lanzamos los eventos
        if(timer <= 0){
            tenSeconds++;
            Tick?.Invoke();
            timer = tickToRealTime;
        }
    }
}

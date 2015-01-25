using UnityEngine;
using System;
using System.Collections;

public class Cronometro : MonoBehaviour 
{
    public delegate void EventHandler();

    //! Public Config
    public bool EnableLimitTiempo = false;
    public float Timepo= 60.0f; // 1 minuto
    public bool CuentraRegresiva = false;
    public bool start = false;
    public string format = "<b>{0}</b>:<b>{1:00}</b>";
    public GUIText text;

    //! Events
    public event EventHandler eventStart;
    public event EventHandler eventComplete;

    //! Private 
    private bool isPause = false;
    private float timeDelta = 0.0f;
    private float value = 0.0f;
    private TimeSpan time = TimeSpan.FromSeconds(0.0f);

    public TimeSpan CurrentTime
    {
        get { return time; }
    }

    public void StartTimer()
    {
        RestartTimer();
        start = true;  
        
        if( eventStart != null)
            eventStart();
    }

    public void StopTimer()
    {
        RestartTimer();
        start = false; 
    }

    public void RestartTimer()
    {
        timeDelta = 0.0f;

        if (CuentraRegresiva)
            value = Timepo;
    }

    public void togglePauseTimer()
    {
        isPause = !isPause;
    }

	void Start () 
    {
        text.text = string.Format(format, (int)time.TotalMinutes, time.Seconds);

        RestartTimer();
        if (start)
            StartTimer();
	}
	
	void Update () 
    {
        if (!isPause && start)
        {
            timeDelta += Time.deltaTime;

            time = TimeSpan.FromSeconds((CuentraRegresiva)?value-timeDelta:timeDelta);
            text.text = string.Format(format, (int)time.TotalMinutes, time.Seconds);

            if (EnableLimitTiempo && timeDelta > value)
            {
                start = false;
                if (eventComplete != null)
                    eventComplete();
            } 
        }
	}
}

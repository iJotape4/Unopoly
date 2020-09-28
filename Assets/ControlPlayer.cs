using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class ControlPlayer : MonoBehaviour
{
    public int Turno = 0;
    public int LImitedeTurno = 2;
    public static ControlPlayer control;
    public SimpleCameraController.CameraState camara;

    
   public BluePlayer Blue;

    void Start()
    {
        control = this.GetComponent<ControlPlayer>();
    }
    void Update()
    {
        if (Turno > 1)
        {
            Turno = 2;
        }

    }
}
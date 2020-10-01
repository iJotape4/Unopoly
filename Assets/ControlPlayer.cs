using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class ControlPlayer : MonoBehaviour
{
    public int Turno = 0;
    public static int LImitedeTurno = 2;
    public static ControlPlayer control;

    public static int[] TurnsList = new int[LImitedeTurno];

    void Start()
    {
        control = this.GetComponent<ControlPlayer>();
        for (int i=1; i<=LImitedeTurno; i++ )
        {
            TurnsList[i - 1] = i;
        }
    }
    void Update()
    {
       /* if (Turno > 1)
        {
            Turno = 2;
        }*/

    }

    public void NextTurno()
    {
        Turno = TurnsList[1];
    }
}
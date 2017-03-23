using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderFunction :Unit
{
   

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is PlayerController2)
        {
           unit.isLadder = true;
        }
       
        //else
       // {
       //     unit.isLadder = false;
      //  }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is PlayerController2)
        {
            unit.isLadder = false;
        }

    }
}
	
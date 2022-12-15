using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackMoleScore : BaseScore
{
    
    private void OnTriggerEnter(Collider other)
    {
        ChangeManagerScore();
    }
}

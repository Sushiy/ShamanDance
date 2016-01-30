using UnityEngine;
using System.Collections;
using System;

public class FireEnemy : IEnemy {

    public override void setElementType()
    {
        ElementType = ElementTypes.Fire;
    
    }

    public override void attackPlayer() {

        throwFireBall();
    }

    void throwFireBall() {
        Debug.Log("FireBall");

    }




}
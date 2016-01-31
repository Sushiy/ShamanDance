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
        waitTillNextThrow();
    }

    void throwFireBall() {

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Debug.Log("PlayerPos: " + playerPos.x + " , " + playerPos.y + " , " + playerPos.z);
        spellcaster.castSpell(SpellType.FIRE, playerPos);
        
        Debug.Log("FireBall");

    }

    void waitTillNextThrow() {

        float time = Time.time;

        float currTime = time;

        do { currTime += Time.deltaTime; } while (currTime <= (time + 60f));
    }

}





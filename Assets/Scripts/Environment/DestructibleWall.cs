using UnityEngine;
using System.Collections;
using System;

public class DestructibleWall : MonoBehaviour, ISpellTarget
{
    public void TakeSpell(SpellType spellType)
    {
        if(spellType == SpellType.FIRE)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Kaboom");
        }
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}

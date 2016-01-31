using UnityEngine;
using System.Collections;
using System;

public class DestructibleWall : MonoBehaviour, ISpellTarget
{
    [SerializeField]
    Sprite brokenSprite;
    [SerializeField]
    Sprite fineSprite;

    public void TakeSpell(SpellType spellType)
    {
        if(spellType == SpellType.FIRE)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            Invoke("BurnDown", 2f);
        }
    }

    void BurnDown()
    {
        GetComponent<SpriteRenderer>().sprite = brokenSprite;
        GetComponent<BoxCollider2D>().isTrigger = true;

        transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }
}

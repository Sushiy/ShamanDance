using UnityEngine;
using System.Collections;

public enum ElementTypes
{

    Fire, Sludge, Shadow
};

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SeekBehaviour))]
[RequireComponent(typeof(WanderBehaviour))]
[RequireComponent(typeof(Spellcaster))]
public abstract class IEnemy : MonoBehaviour {

    private int stamina; //health points
    private ElementTypes elementType;
    
    public int Stamina { get; set; }
    public ElementTypes ElementType {get; set; }

    protected SeekBehaviour seekBehaviour;
    protected Spellcaster spellcaster;
	// Use this for initialization
	void Start () {

        stamina = 100;
        setElementType();
        seekBehaviour = this.GetComponentInParent<SeekBehaviour>();
        spellcaster = this.GetComponentInParent<Spellcaster>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (seekBehaviour.hasReachedPlayer)
            attackPlayer();

	
	}

    public abstract void attackPlayer();
    public abstract void setElementType();
}

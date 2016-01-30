using UnityEngine;
using System.Collections;

public enum ElementTypes
{

    Fire, Sludge, Shadow
};

public abstract class IEnemy : MonoBehaviour {

    private int stamina; //health points
    private ElementTypes elementType;
    
    public int Stamina { get; set; }
    public ElementTypes ElementType {get; set; }

    protected SeekBehaviour seekBehaviour;
	// Use this for initialization
	void Start () {

        stamina = 100;
        setElementType();
        seekBehaviour = this.GetComponentInParent<SeekBehaviour>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (seekBehaviour.hasReachedPlayer)
            attackPlayer();

	
	}

    public abstract void attackPlayer();
    public abstract void setElementType();
}

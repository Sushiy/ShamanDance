using UnityEngine;
using System.Collections;

public enum ElementTypes{

    Water, Fire, Earth, Shadow
};

public abstract class IEnemy : MonoBehaviour {

    private int stamina; //health points
    private ElementTypes elementType;
    
    public int Stamina { get; set; }
    public ElementTypes ElementType {get; set; }

    // LookingAround Behaviour
    // SeekingBehaviour

    public IEnemy(ElementTypes type) {

        stamina = 100;
        elementType = type;

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void attackPlayer();
}

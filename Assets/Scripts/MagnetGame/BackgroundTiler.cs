using UnityEngine;
using System.Collections;

public class BackgroundTiler : MonoBehaviour
{
    [SerializeField]
    private Vector2 TopLeft;
    [SerializeField]
    private Vector2 BotRight;
    [SerializeField]
    private GameObject imagePrefab;

	// Use this for initialization
	void Start ()
    {
        Sprite image = imagePrefab.GetComponent<SpriteRenderer>().sprite;
        float spriteWidth = image.texture.width / image.pixelsPerUnit;
        float spriteCountX = (BotRight.x - TopLeft.x)/ spriteWidth;
        float spriteHeight = image.texture.width / image.pixelsPerUnit;
        float spriteCountY = (TopLeft.y - BotRight.y)/spriteHeight;
        for (int i = 0; i < spriteCountX; ++i)
        {
            for(int j = 0; j < spriteCountY; ++j)
            {
                SpawnSprite(TopLeft + new Vector2(i * spriteWidth + spriteWidth/2, -j * spriteHeight - spriteHeight/2));
            }
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(TopLeft, 1);
        Gizmos.DrawWireSphere(BotRight, 1);
        Gizmos.DrawLine(TopLeft, BotRight);
        //Gizmos.DrawWireCube(transform.position, (BotRight - TopLeft));
    }

    void SpawnSprite(Vector2 pos)
    {
        GameObject g = (GameObject)GameObject.Instantiate(imagePrefab, new Vector3(pos.x, pos.y, transform.position.z), Quaternion.identity);
        g.transform.SetParent(transform);
    }
    
}

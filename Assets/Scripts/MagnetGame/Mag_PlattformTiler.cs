using UnityEngine;
using System.Collections;

public class Mag_PlattformTiler : MonoBehaviour
{
    [SerializeField]
    float pixelWidth = 256;
    [SerializeField]
    float pixelsperunit = 100;

    [SerializeField]
    EndType leftEndType = EndType.Free;
    [SerializeField]
    EndType rightEndType = EndType.Free;

    [SerializeField]
    bool leftEnd;
    [SerializeField]
    bool rightEnd;

    [SerializeField]
    GameObject leftStand;
    [SerializeField]
    GameObject leftFree;
    [SerializeField]
    GameObject leftMount;
    [SerializeField]
    GameObject mid;
    [SerializeField]
    GameObject rightStand;
    [SerializeField]
    GameObject rightFree;
    [SerializeField]
    GameObject rightMount;
    GameObject left;
    GameObject right;

    private enum EndType {Stand, Free, Mount};



    float endOffset = 1f;

    
    // Use this for initialization
    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        left = leftStand;
        right = rightStand;

        ChooseEndTiles();
        GenerateTiles();       
    }

    void ChooseEndTiles()
    {
        switch (leftEndType)
        {
            case EndType.Free:
                left = leftFree;
                break;
            case EndType.Stand:
                left = leftStand;
                break;
            case EndType.Mount:
                left = leftMount;
                break;
        }
        switch (rightEndType)
        {
            case EndType.Free:
                right = rightFree;
                break;
            case EndType.Stand:
                right = rightStand;
                break;
            case EndType.Mount:
                right = rightMount;
                break;
        }
    }

    void GenerateTiles()
    {
        Quaternion q = transform.rotation;
        transform.rotation = Quaternion.identity;
        float spriteWidth = pixelWidth / pixelsperunit;
        BoxCollider2D b = GetComponent<BoxCollider2D>();
        float colliderWidth = b.bounds.max.x - b.bounds.min.x + 2 * endOffset;
        int count = Mathf.RoundToInt(colliderWidth / spriteWidth);
        for (int i = 0; i < count; ++i)
        {
            Vector2 pos = new Vector2(b.bounds.min.x - endOffset, (transform.position.y - 0.15f));
            pos.x += i * spriteWidth + spriteWidth / 2;
            if (i == 0 && leftEnd)
                SpawnPlatform(pos, left);
            else if (i == count - 1 && rightEnd)
                SpawnPlatform(pos, right);
            else
                SpawnPlatform(pos, mid);
        }
        transform.rotation = q;
    }

    void OnDrawGizmosSelected()
    {
        
        BoxCollider2D b = GetComponent<BoxCollider2D>();
        float spriteWidth = pixelWidth / pixelsperunit;
        float colliderWidth = b.bounds.max.x - b.bounds.min.x + 2 * endOffset;
        int count = Mathf.RoundToInt(colliderWidth / spriteWidth);
        for (int i = 0; i < count; ++i)
        {
            Vector2 pos = new Vector2(b.bounds.min.x - endOffset, b.bounds.min.y);
            pos.x += i * spriteWidth;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pos, 0.25f);
            Gizmos.color = Color.green;
            pos.y = b.bounds.max.y + 0.28f;
            pos.x += spriteWidth;
            if(i != count-1 || !rightEnd)
            Gizmos.DrawWireSphere(pos, 0.25f);

            if (i == 0 && leftEnd)
            {
                Gizmos.color = Color.red;
                pos.x -= 1.58f;
                Gizmos.DrawWireSphere(pos, 0.25f);
            }
            if (i == count - 2 && rightEnd)
            {
                
                Gizmos.color = Color.red;
                pos.x += 1.58f;
                Gizmos.DrawWireSphere(pos, 0.25f);
            }
            if (i == count - 1 && rightEnd)
            {

                Gizmos.color = Color.blue;
                pos.y = b.bounds.min.y;
                Gizmos.DrawWireSphere(pos, 0.25f);
            }

        }
    }

    void SpawnPlatform(Vector2 pos, GameObject platform)
    {
        GameObject g = (GameObject)GameObject.Instantiate(platform, pos, Quaternion.identity);
        g.transform.SetParent(transform);
    }
}

using UnityEngine;
using System.Collections;

public class Mag_SpikeTiler : MonoBehaviour
{

    [SerializeField]
    float pixelWidth = 512;
    [SerializeField]
    float pixelsperunit = 100;
    

    [SerializeField]
    bool leftEnd;
    [SerializeField]
    bool rightEnd;

    [SerializeField]
    GameObject left;
    [SerializeField]
    GameObject mid;
    [SerializeField]
    GameObject mid2;
    [SerializeField]
    GameObject right;

    float endOffset = 1f;

    float yoffset = 0.0f;

    // Use this for initialization
    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GenerateBoxes();
    }
    

    void OnDrawGizmosSelected()
    {

        BoxCollider2D b = GetComponent<BoxCollider2D>();
        float spriteWidth = pixelWidth / pixelsperunit;
        float colliderWidth = b.bounds.max.x - b.bounds.min.x + 2 * endOffset;
        int count = Mathf.RoundToInt(colliderWidth / spriteWidth);
        Debug.Log("spriteWidth: " + spriteWidth + " colliderWidth: " + colliderWidth + " count: " + count);
        for (int i = 0; i < count; ++i)
        {
            Vector2 pos = new Vector2(b.bounds.min.x - endOffset, b.bounds.min.y);
            pos.x += i * spriteWidth;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pos, 0.25f);
            Gizmos.color = Color.green;
            pos.y = b.bounds.max.y + 0.28f;
            pos.x += spriteWidth;
            if (i != count - 1 || !rightEnd)
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

    void GenerateBoxes()
    {
        Quaternion q = transform.rotation;
        transform.rotation = Quaternion.identity;
        float spriteWidth = pixelWidth / pixelsperunit;
        BoxCollider2D b = GetComponent<BoxCollider2D>();
        float colliderWidth = b.bounds.max.x - b.bounds.min.x + 2 * endOffset;
        int count = Mathf.RoundToInt(colliderWidth / spriteWidth);
        for (int i = 0; i < count; ++i)
        {
            Vector2 pos = new Vector2(b.bounds.min.x - endOffset, (b.bounds.max.y + yoffset));
            pos.x += i * spriteWidth + spriteWidth / 2;
            if (i == 0 && leftEnd)
                SpawnPlatform(pos, left);
            else if (i == count - 1 && rightEnd)
                SpawnPlatform(pos, right);
            else
                SpawnPlatform(pos, ChooseMid());
        }
        transform.rotation = q;
    }

    void SpawnPlatform(Vector2 pos, GameObject platform)
    {
        GameObject g = (GameObject)GameObject.Instantiate(platform, pos, Quaternion.identity);
        g.transform.SetParent(transform);
    }

    GameObject ChooseMid()
    {
        float r = Random.Range(0, 2);
        if (r == 0)
            return mid;
        else
            return mid2;
    }

}

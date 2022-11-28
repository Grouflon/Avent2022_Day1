using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite closedMouth;
    public Sprite openMouth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hoveringMushrooms > 0)   
        {
            spriteRenderer.sprite = openMouth;
        }
        else
        {
            spriteRenderer.sprite = closedMouth;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Mushroom>() != null)
        {
            ++m_hoveringMushrooms;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<Mushroom>() != null)
        {
            --m_hoveringMushrooms;
        }
    }

    int m_hoveringMushrooms;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public bool isHoveringMouth
    {
        get => m_hoveringMouths > 0;
    }

    public void resetHoveringMouths()
    {
        m_hoveringMouths = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Mouth>() != null)
        {
            ++m_hoveringMouths;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<Mouth>() != null)
        {
            --m_hoveringMouths;
        }
    }

    int m_hoveringMouths;
}

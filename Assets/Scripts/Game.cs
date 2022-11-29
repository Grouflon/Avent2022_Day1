using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Game : MonoBehaviour
{
    public Face[] faces;
    public Mouth[] mouths;
    public Nose[] noses;
    public Eye[] eyes;
    public Brow[] brows;
    public Hat[] hats;

    public Mushroom draggedMushroom
    {
        get => m_draggedMushroom;
    }

    // Start is called before the first frame update
    void Start()
    {
        randomizeFace();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector2.zero);
            if (hits.Length > 0)
            {

                Mushroom mushroom = hits[0].transform.GetComponent<Mushroom>();
                if (mushroom != null)
                {
                    onMushroomDragStart(mushroom);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (m_draggedMushroom != null)
            {
                onMushroomDragStop();
            }
        }

        // DRAG
        if (m_draggedMushroom != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(new Vector3(0.0f, 0.0f, -1.0f), Vector3.zero);
            float enter = 0.0f;
            bool result = p.Raycast(ray, out enter);
            Assert.IsTrue(result);
            Vector3 point = ray.GetPoint(enter);

            m_draggedMushroom.transform.position = point + m_draggedMushroomOffset;
        }
    }

    void onMushroomDragStart(Mushroom _mushroom)
    {
        Assert.IsNotNull(_mushroom);
        Assert.IsNull(m_draggedMushroom);

        m_draggedMushroom = _mushroom;
        m_draggedMushroomOrigin = _mushroom.transform.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(new Vector3(0.0f, 0.0f, -1.0f), Vector3.zero);
        float enter = 0.0f;
        bool result = p.Raycast(ray, out enter);
        Assert.IsTrue(result);
        Vector3 point = ray.GetPoint(enter);

        m_draggedMushroomOffset = m_draggedMushroomOrigin - point;

    }

    void onMushroomDragStop()
    {
        Assert.IsNotNull(m_draggedMushroom);

        m_draggedMushroom.transform.position = m_draggedMushroomOrigin;

        if (m_draggedMushroom.isHoveringMouth)
        {
            randomizeFace();
        }

        m_draggedMushroom.resetHoveringMouths();
        m_draggedMushroom = null;
    }

    void randomizeFace()
    {
        if (m_face != null)
        {
            GameObject.Destroy(m_face.gameObject);
            m_face = null;
        }

        Face facePrefab = faces[Random.Range(0, faces.Length)];
        m_face = Instantiate(facePrefab, Vector3.zero, Quaternion.identity);
        foreach (FaceSocket socket in m_face.sockets)
        {
            foreach (Transform child in socket.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            MonoBehaviour prefab = null;
            switch(socket.type)
            {
                case FaceSocketType.Mouth:
                {
                    prefab = mouths[Random.Range(0, mouths.Length)];
                }
                break;
                case FaceSocketType.Eye:
                {
                    prefab = eyes[Random.Range(0, eyes.Length)];
                }
                break;
                case FaceSocketType.Nose:
                {
                    prefab = noses[Random.Range(0, noses.Length)];
                }
                break;
                case FaceSocketType.Brow:
                {
                    prefab = brows[Random.Range(0, brows.Length)];
                }
                break;
                case FaceSocketType.Hat:
                {
                    prefab = hats[Random.Range(0, hats.Length)];
                }
                break;

                default: break;
            }
            MonoBehaviour instance = Instantiate(prefab, socket.transform);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;
        }
    }

    Face m_face;
    Mushroom m_draggedMushroom;
    Vector3 m_draggedMushroomOffset;
    Vector3 m_draggedMushroomOrigin;
}

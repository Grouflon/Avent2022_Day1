using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Eye : MonoBehaviour
{
    public CircleCollider2D pupilRange;
    public Transform pupil;

    public float maxLookAtRange = 500.0f;
    public float lookLerpRatio = .3f;

    // Start is called before the first frame update
    void Start()
    {
        m_game = FindObjectOfType<Game>();

        pupil.transform.position = pupilRange.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pupilCenter = pupilRange.transform.position + new Vector3(pupilRange.offset.x, pupilRange.offset.y, 0.0f);
        Vector3 pupilTargetPosition = pupilCenter;
        if (m_game.draggedMushroom)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(new Vector3(0.0f, 0.0f, -1.0f), Vector3.zero);
            float enter = 0.0f;
            bool result = p.Raycast(ray, out enter);
            Assert.IsTrue(result);
            Vector3 point = ray.GetPoint(enter);

            Vector3 centerToPoint = point - pupilCenter;
            centerToPoint.z = 0.0f;
            float distanceRatio = Mathf.Min(centerToPoint.magnitude, maxLookAtRange) / maxLookAtRange;
            pupilTargetPosition = pupilCenter + centerToPoint.normalized * distanceRatio * pupilRange.radius;
        }

	    pupil.transform.position = Vector3.Lerp(pupil.transform.position, pupilTargetPosition, (1.0f - Mathf.Pow(lookLerpRatio, Time.deltaTime)));
    }

    Game m_game;
}

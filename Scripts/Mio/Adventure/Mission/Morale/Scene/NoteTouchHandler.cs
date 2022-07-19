using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SCS.Mio.Mission.Morale
{
    public class NoteTouchHandler : MonoBehaviour
    {
        private Scene.MoraleMeter _moraleMeter;
        Vector3 touchPosWorld;

        private void Awake()
        {
            _moraleMeter = FindObjectOfType<Scene.MoraleMeter>();
        }
        private void FixedUpdate()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                ProcessTouch(touchPosWorld);

            }
#if UNITY_EDITOR
            if(Input.GetMouseButtonDown(0))
            {
                touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ProcessTouch(touchPosWorld);
            }
#endif
        }

        private void ProcessTouch(Vector3 touchPosition)
        {
            Vector2 touchPosWorld2D = new Vector2(touchPosition.x, touchPosition.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                Debug.Log("Touched " + touchedObject.transform.name);

                NoteBody noteBody = touchedObject.GetComponent<NoteBody>();
                if(noteBody != null)
                {
                    noteBody.Select();
                    ProcessMoraleQuality(noteBody.Modifier);
                }

            }
        }

        private void ProcessMoraleQuality(float modifier)
        {
            if (modifier > 0.8f)
            {
                _moraleMeter.Increment(Scene.MoraleQuality.Great);
                return;
            }

            if (modifier > 0.5f)
            {
                _moraleMeter.Increment(Scene.MoraleQuality.Good);
                return;
            }

            _moraleMeter.Increment(Scene.MoraleQuality.Ok);
        }

    }

}


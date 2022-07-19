using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public interface IUI_DamageDisplay
    {
        public void Init();
        public void SetDamage(Vector3 position, int damage);
    }
    public class UI_DamageDisplay : MonoBehaviour, IUI_DamageDisplay
    {
        #region private variables
        [SerializeField] private TMPro.TextMeshPro _text;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private WaitForSeconds _yieldReset;

        #endregion private variables

        #region  public variables
        #endregion public variables

        #region private functions
        private void Awake()
        {
#if UNITY_EDITOR
            if(_text == null)
            {
                Debug.Log("TextMeshPro component not found.");
            }
#endif

            _text.SetText("Hello World");
            _yieldReset = new WaitForSeconds(1f);
        }

        private IEnumerator ResetDamage()
        {
            yield return _yieldReset;
            gameObject.SetActive(false);
            //_rigidbody2D.isKinematic = true;
        }
        #endregion private functions

        #region public functions
        public void Init()
        {
            gameObject.SetActive(false);
        }    

        public void SetDamage(Vector3 position, int damage)
        {
            gameObject.SetActive(true);
            var direction = Random.Range(0, 2) == 0 ? 1 : -1;
            transform.position = position;
            _rigidbody2D.AddForce(new Vector2(direction * 70, 200.0f));
            _text.SetText("" + damage);
            StartCoroutine(ResetDamage());
        }
        #endregion public functions
    }
}
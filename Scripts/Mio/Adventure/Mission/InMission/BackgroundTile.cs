using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public enum MoveDirection
    {
        Up = -1,
        Down = 1,
        None = 0
    }
    public class BackgroundTile : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private BoxCollider2D _groundCollider;
        private float _groundHorizontalLength;
        private float _groundVerticalLength;
        private MoveDirection _direction = MoveDirection.Up;
        private Mission.IInMissionVelocityHandler _velocityHandler;
        private Mission.IInMissionEventDispatcher _dispatcher;

        private void Awake()
        {
            _velocityHandler = FindObjectOfType<Mission.InMissionVelocityHandler>();
            _dispatcher = FindObjectOfType<Mission.InMissionEventDispatcher>();

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _groundCollider = GetComponent<BoxCollider2D>();
            _groundHorizontalLength = _groundCollider.size.x;
            _groundVerticalLength = _groundCollider.size.y;

            //disable raycasts
            gameObject.layer = 2;
        }

        private void Start()
        {
            _groundHorizontalLength = _groundCollider.size.x;
        }

        private void FixedUpdate()
        {

            _rigidbody2D.velocity = new Vector2(0.0f, _velocityHandler.Velocity * (float)_direction);
            if (transform.position.y < -_groundVerticalLength)
            {
                ResetPosition();
            }
            
        }

        private void ResetPosition()
        {
            Vector2 groundOffset = new Vector2(0.0f, (_groundVerticalLength * 2.0f) + -0.2f );
            Vector2 newPosition = (Vector2)transform.position + groundOffset;
            transform.position = new Vector3(newPosition.x, newPosition.y, 0.0f);
        }

        public void DisableTiling()
        {
            _rigidbody2D.velocity = Vector2.zero;
            enabled = false;
        }

        public void EnableTiling()
        {
            enabled = true;
        }
    }

}
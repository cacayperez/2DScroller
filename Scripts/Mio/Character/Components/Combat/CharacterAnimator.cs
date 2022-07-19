using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SCS.Mio.Character
{
    public interface ICharacterAnimator
    {
        public void AnimateIdle();
        public void AnimateTakeDamage();
        public void AnimateAttack_Prepare();
        public void AnimateAttack_MovingToTarget();
        public void AnimateAttack_DealingDamage();
        public void AnimateAttack_ReturnToPosition();
    }

    public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
    {
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void AnimateAttack_DealingDamage()
        {
            _animator.SetTrigger("Attack_DealingDamage");
        }

        public void AnimateAttack_MovingToTarget()
        {
            _animator.SetTrigger("MoveToTarget");
        }

        public void AnimateAttack_Prepare()
        {
            _animator.SetTrigger("Attack_Prep");
        }

        public void AnimateAttack_ReturnToPosition()
        {
            _animator.SetTrigger("ReturnToPosition");
        }

        public void AnimateIdle()
        {
            _animator.SetTrigger("Idle");
        }

        public void AnimateTakeDamage()
        {
            
        }

    }
}


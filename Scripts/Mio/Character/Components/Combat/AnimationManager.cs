using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SCS.Mio.Character
{
    public enum AttackState
    {
        None,
        Preparing,
        MovingToTarget,
        DealingDamage,
        ReturningToPosition

    }

    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] protected float _attackLandDuration = 0.2f;
        [SerializeField] protected float _attackPrepDuration = 0.2f;
        protected WaitForSeconds _attackWait;
        private CharacterCombatEventDispatcher _eventDispatcher;
        private Vector3 _defaultPosition;
        private ICharacterAnimator _characterAnimator;
        private ICombatSystem _target;

        private void Awake()
        {
            _eventDispatcher = GetComponent<CharacterCombatEventDispatcher>();
            _eventDispatcher.SubscribeToOnAttack(HandleAttack);
            _eventDispatcher.Subscribe(CombatActionState.Idling, HandleIdle);
            _eventDispatcher.Subscribe(CombatActionState.ReceivingDamage, OnReceiveDamage);

            _characterAnimator = gameObject.AddComponent(typeof (CharacterAnimator)) as CharacterAnimator;

            _defaultPosition = transform.position;

        }

        private void HandleIdle()
        {
            _characterAnimator.AnimateIdle();
        }

        private void HandleAttack(ICombatSystem target)
        {
            if (target == null) return;
            _target = target;
            StartCoroutine(ExecuteAttack_Prep());
        }

       

        private IEnumerator ExecuteAttack_Prep()
        {
            // 1
            _characterAnimator.AnimateAttack_Prepare();
            yield return Helper.Yielder.Get(_attackPrepDuration);
            AttackTarget();
        }

        private void AttackTarget()
        {
            // 2
            if (_target.HitTarget == null) return;
            _characterAnimator.AnimateAttack_MovingToTarget();
            transform.DOMove(_target.HitTarget.position, 0.5f)
                 .OnComplete(() => {
                     StartCoroutine(ExecuteAttack_DealingDamage());
                 });
        }

        private IEnumerator ExecuteAttack_DealingDamage()
        {
            // 3
            _characterAnimator.AnimateAttack_DealingDamage();
            yield return Helper.Yielder.Get(_attackLandDuration);
            ReturnToPosition();
        }

        private void ReturnToPosition()
        {
            // 4
            _characterAnimator.AnimateAttack_ReturnToPosition();
            transform.DOMove(_defaultPosition, 0.5f)
                .OnComplete(OnAttackComplete);
        }


        private void OnAttackComplete()
        {
            // 5 
            //_characterAnimator.AnimateIdle();
            _eventDispatcher.RequestComplete(CombatActionState.Attacking);
            
        }

        private void OnReceiveDamage()
        {
            _characterAnimator.AnimateTakeDamage();
        }

    }

}
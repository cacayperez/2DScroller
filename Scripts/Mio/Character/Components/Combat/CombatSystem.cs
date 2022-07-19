using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SCS.Mio.Character
{
    public interface ICombatSystem
    {
        public bool IsTargettable { get; }
        public GameObject OwnerObject { get; }
        public Transform HitTarget { get; }
        public TurnHandler TurnHandler { get; }
        public Vector3 DefaultPosition { get; }
        public void BeginCombat();
        public void ReceiveDamage(int Damage);
        //public void ApplyDamage();
    }

    public class CombatSystem : ComponentBase, ICombatSystem
    {
        #region private variables

        #region serialized private fields

        [SerializeField] private Transform _hitTarget;
        /// <summary>
        /// Responsible for self managing wait and turn times
        /// </summary>
        [SerializeField] private TurnHandler _turnHandler;

        /// <summary>
        /// Responsible for communicating with other components
        /// </summary>
        [SerializeField] private CharacterCombatEventDispatcher _eventDispatcher;

        /// <summary>
        /// Responsible for making decisions during Turn
        /// </summary>
        [SerializeField] private AIScript _aIScript;

        /// <summary>
        /// Responsible for Selecting / Cycling thru targettable entities or objects
        /// </summary>
        [SerializeField] private TargetHandler _targetHandler;

        /// <summary>
        /// Responsible for handling damage and heals
        /// </summary>
        [SerializeField] private DamageHandler _damageHandler;

        /// <summary>
        /// Responsible for handling animation
        /// </summary>
        [SerializeField] private AnimationManager _animationHandler;

        [SerializeField] private int damage;

        #endregion serialized private fields

        private Vector3 _defaultPosition;
        private FCombatActionContext _applyDamageContext;
        private ISceneCombatEventDispatcher _sceneCombatDispatcher;
        #endregion private variables

        #region public variables
        public bool IsTargettable => throw new System.NotImplementedException();
        public GameObject OwnerObject { get { return gameObject; } }
        public TurnHandler TurnHandler { get { return _turnHandler; } }
        public Transform HitTarget { get { return _hitTarget; } }
        public Vector3 DefaultPosition { get { return _defaultPosition; } }
        #endregion public variables


        private void Awake()
        {
            _eventDispatcher = gameObject.GetComponent<CharacterCombatEventDispatcher>();
            _eventDispatcher ??= gameObject.AddComponent(typeof(CharacterCombatEventDispatcher)) as CharacterCombatEventDispatcher;

            _targetHandler = gameObject.GetComponent<TargetHandler>();
            _targetHandler ??= gameObject.AddComponent(typeof(TargetHandler)) as TargetHandler;

            _aIScript = gameObject.GetComponent(typeof(AIScript)) as AIScript;
            _aIScript ??= gameObject.AddComponent(typeof(AIScript)) as AIScript;

            _turnHandler = gameObject.GetComponent<TurnHandler>();
            _turnHandler ??= gameObject.AddComponent(typeof(TurnHandler)) as TurnHandler;

            _damageHandler = gameObject.GetComponent<DamageHandler>();
            _damageHandler ??= gameObject.AddComponent(typeof(DamageHandler)) as DamageHandler;
            _damageHandler.damage = damage;

            _animationHandler = gameObject.GetComponent<AnimationManager>();
            _animationHandler ??= gameObject.AddComponent(typeof(AnimationManager)) as AnimationManager;


            _eventDispatcher.Subscribe(CombatActionState.TakingDefeat, HandleTakeDefeat);
            _defaultPosition = transform.position;

            //_applyDamageContext = new CombatActionContext { actionType = CombatActionState.ApplyingDamage };

            _sceneCombatDispatcher = FindObjectOfType<SceneCombatEventDispatcher>();
        }

        private void HandleTakeDefeat()
        {
            var combatmanager = FindObjectOfType<SceneCombatTurnHandler>();
            combatmanager?.CharacterDefeated();
            Destroy(gameObject);
        }

        public void BeginCombat()
        {
            _turnHandler.StartCombat();
        }

        public void ReceiveDamage(int damage)
        {
            _eventDispatcher.Request(new FCombatActionContext { damage = damage, actionType = CombatActionState.ReceivingDamage });

            var message = new SceneCombatMessage { position = transform.position, damage = damage };
            _sceneCombatDispatcher.Send(SceneCombatEvent.TakingDamage, message);
        }

/*        public void ApplyDamage()
        {
            _eventDispatcher.Request(_applyDamageContext);
        }*/



    }

}
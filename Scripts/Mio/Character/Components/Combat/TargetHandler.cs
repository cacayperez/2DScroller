using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Character
{
    public interface ITargetHandler
    {
        public ICombatSystem CurrentTarget { get; }
    }
    public class TargetHandler : MonoBehaviour, ITargetHandler
    {
        private SceneCombatTurnHandler _combatManager;
        public ICombatSystem CurrentTarget => FindHostileTarget();
        public ICombatSystem Self;

        private void Awake()
        {
            Self = GetComponent<CombatSystem>();
            _combatManager = FindObjectOfType<SceneCombatTurnHandler>();
        }

        public ICombatSystem FindHostileTarget()
        {
            var targets = _combatManager.Targetables;

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] != Self) return targets[i];
            }

            return null;
        }


    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public GridManager gridManager;
        public EnemyTargetManager enemyTargetManager;
        public InteractableManager interactableManager;
        void Awake()
        {
            gridManager.Initialize();
            //enemyTargetManager.Initialize();
            interactableManager.Initialize();
        }
    }
}

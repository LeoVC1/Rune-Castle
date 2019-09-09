using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public GridManager gridManager;

        void Awake()
        {
            gridManager.Initialize();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class GameTest : MonoBehaviour
    {
        void Start()
        {
            Game.Do(new StartGame());
        }
    }
}

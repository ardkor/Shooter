using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace names_source
{
    public class Source : MonoBehaviour
    {
        public class Creation
        {
            public int health_points;
        }
        public class enemy : Creation
        {
            public int attack_points;
            public bool alive;
            public enemy(int health, int attack, bool living) { health_points = health; attack_points = attack; alive = living; }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaphysics
{
    public class DatabaseRef : MonoBehaviour
    {
        [SerializeField] private CardpoolDatabase[] _databases;

        public CardpoolDatabase[] Databases { get { return _databases; } }
    }
}

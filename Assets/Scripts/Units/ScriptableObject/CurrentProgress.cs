using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    [CreateAssetMenu(fileName = "CurrentProgress", menuName = "ScriptableObjects/CurrentProgress/Bar")]
    public class CurrentProgress : ScriptableObject
    {
        [SerializeField] private List<UnitAttributes> m_UnitsOfPlayer;
        [SerializeField] private List<UnitAttributes> m_UnitsOfEnemies;
        public List<UnitAttributes> UnitsOfPlayer
        {
            get { return m_UnitsOfPlayer; }
            set { m_UnitsOfPlayer = value; }
        }

        public List<UnitAttributes> UnitsOfEnemies
        {
            get { return m_UnitsOfEnemies; }
            set { m_UnitsOfEnemies = value; }
        }
    }
}

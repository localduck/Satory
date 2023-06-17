using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class RowOrder : MonoBehaviour
    {
        private HexBattleground[] m_AllHexesInRow;
        public HexBattleground[] AllHexesInRow
        {
            set { m_AllHexesInRow = value; }
            get { return m_AllHexesInRow;  }
        }
    }
}
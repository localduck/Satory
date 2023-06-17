using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private Canvas m_Canvas;
        private RowOrder[] m_ListOfRows;
        static public HexBattleground[,] m_HexesArray;
        private int m_RowsLangth;
        static public List<HexBattleground> activeHexList = new List<HexBattleground>();

        public HexBattleground[,] HexesArray => m_HexesArray;

        private void Awake()
        {
            m_ListOfRows = GetComponentsInChildren<RowOrder>();
            m_RowsLangth = m_ListOfRows.Length;
            for (int i = 0; i < m_RowsLangth; i++)
            {
                m_ListOfRows[i].AllHexesInRow = m_ListOfRows[i].GetComponentsInChildren<HexBattleground>();
            }
            SetHexesCoordinats();
            SetActiveHexes();
            Test();
        }

        private void Start()
        {
            /*AvalibleHexPos hero = FindObjectOfType<AvalibleHexPos>();
            INeighborFinder AdjacentFinder = new PositionForSky();
            HexBattleground startingHex = hero.GetComponentInParent<HexBattleground>();
            int stepLimit = BattleController.currentAtacker.Velocity;
            startingHex.SetAsCurrentHex();
            hero.GetAvailablePositions(hero.GetComponentInParent<HexBattleground>(), stepLimit, AdjacentFinder);*/
        }

        private void SetHexesCoordinats()
        {
            int heightIsCol = m_ListOfRows.Length;
            int widthIsRow = m_ListOfRows[0].AllHexesInRow.Length;
            m_HexesArray = new HexBattleground[widthIsRow, heightIsCol];
            for (int i = 0; i < heightIsCol; i++)
            {
                for(int j = 0; j < widthIsRow; j++)
                {
                    m_HexesArray[j, i] = m_ListOfRows[heightIsCol - i - 1].AllHexesInRow[widthIsRow - j - 1];
                    m_HexesArray[j, i].VerticalCoordinate = i + 1;
                    m_HexesArray[j, i].HorizontalCoordinate = j + 1;
                }
            }
        }

        private void SetActiveHexes()
        {
            Vector3 border2d = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Mathf.Abs(m_Canvas.transform.position.z)));
            foreach (HexBattleground hex in m_HexesArray)
            {
                if(Mathf.Abs(hex.transform.position.x) > Mathf.Abs(border2d.x) || Mathf.Abs(hex.transform.position.y) > Mathf.Abs(border2d.y))
                {
                    hex.SetInactivate();
                } else
                {
                    hex.SetActivate();
                    activeHexList.Add(hex);
                }
            }
        }

        private void Test()
        {
            //print();
        }
    }
}
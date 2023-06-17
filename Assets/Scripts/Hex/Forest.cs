using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class Forest : HexBattleground
    {
        // Start is called before the first frame update
        void Start()
        {
            VisualModel = transform.GetChild(0).GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

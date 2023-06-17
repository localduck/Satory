using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class Grass : HexBattleground
    {
        // Start is called before the first frame update
        private void Start()
        {
            VisualModel = transform.GetChild(0).GetComponent<Image>();
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage1
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            SoundManager.Instance.PlayBgm(EBgmTable.Stage1);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
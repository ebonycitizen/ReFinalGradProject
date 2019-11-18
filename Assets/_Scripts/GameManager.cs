using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{

    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            SoundManager.Instance.PlayBgm(EBgmTable.Default);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
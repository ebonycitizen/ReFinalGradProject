using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
public enum EBgmTable
{
    Default,
    Tutorial,
    Island,
    Ocean,
    Cave,
    TestCave,
    TestStream,
    FullBgm,
    LastBgm,
    Title,
    EndBgm,

}
public class BgmContainer : MonoBehaviour
{
    [Serializable]
    public class BgmItem
    {
        public EBgmTable BgmType;

        public AudioClip BgmClip;
    }

    [SerializeField]
    private List<BgmItem> m_bgmItems = new List<BgmItem>();

    public List<BgmItem> BGM
    {
        get
        {
            return m_bgmItems;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESeTable
{
    Sparkle_1/*キラキラの自身の音*/,
    Sparkle_2/*キラキラを使った音*/,


    Orac_1/*鳴き声*/,
    Orac_2/*喜び*/,
    Orac_3/*悲しい*/,
    Orac_4/*甘える*/,
    Orac_5/*ひるんだ時の声*/,
    Orac_6/*シャチが放つ超音波*/,
    Orac_7/*シャチの掛け声(了解!!)*/,

    Dolphin_1/*興味津々な声*/,
    Dolphin_2/*鼓舞のような喚き声*/,

    Penguin_1,
    Penguin_2,
    Penguin_Sing_1,
    Penguin_Sing_2,


    Tmp_PenguinSinging,

    JumpOutWater,
    JumpIntoWater,

    Crystal,

    Rock_1,
    Rock_2,

    Whale_1,
    Whale_2,
    Whale_3,
}
public class SeContainer : MonoBehaviour
{
    [Serializable]
    public class SeItem
    {
        public ESeTable SeType;

        public AudioClip SeClip;
    }

    [SerializeField]
    private List<SeItem> m_seItems = new List<SeItem>();

    public List<SeItem> SE
    {
        get
        {
            return m_seItems;
        }
    }
}

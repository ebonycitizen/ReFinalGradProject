using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedRandom
{
    float[] weights;
    float totalWeight = 0;

    float[] thresholds;
    int[] aliases;

    /// <summary>
     /// 重み付きランダム用オブジェクトを作成する
     /// </summary>
     /// <param name="weights">各要素がどれだけの重みを持っているかのリスト</param>
    public WeightedRandom(float[] weights)
    {
        ResetWeights(weights);
    }

    /// <summary>
     /// 重み付けを設定しなおす
     /// </summary>
     /// <param name="weights">各要素がどれだけの重みを持っているかのリスト</param>
    public void ResetWeights(float[] weights)
    {
        int length = (weights != null) ? weights.Length : 0;
        this.weights = new float[length];
        if (length != 0) System.Array.Copy(weights, this.weights, length);

        CreateAliasList();
    }

    // 抽選用の内部リストを構築する
    // 各要素を1ブロックとして扱う 各ブロックの重みは1になるように調整(正規化)する
    void CreateAliasList()
    {
        int length = weights.Length;

        thresholds = new float[length];
        aliases = new int[length];
        totalWeight = 0;

        // 重みの合計を算出
        for (int i = 0; i < length; ++i)
        {
            // 重みがマイナスだった場合は 0 にしておく
            weights[i] = Mathf.Max(weights[i], 0);
            totalWeight += weights[i];
        }

        // あとで重みの正規化を行うため割合を計算しておく
        float normalizeRatio = (totalWeight != 0) ? length / totalWeight : 0;

        Stack<int> small = new Stack<int>();
        Stack<int> large = new Stack<int>();
        for (int i = 0; i < length; i++)
        {
            // エイリアス初期化
            aliases[i] = i;

            // 重みを要素数で正規化
            float weight = weights[i];
            weight *= normalizeRatio;

            thresholds[i] = weight;

            // 重みがブロック内に収まるものとはみ出るものとで振り分けていく
            if (weight < 1f)
            {
                small.Push(i);
            }
            else
            {
                large.Push(i);
            }
        }

        // 重みの小さい要素に重みの大きい要素のはみ出た部分を移動させてブロックを埋めていく
        while (small.Count > 0 && large.Count > 0)
        {
            int s = small.Pop();
            int l = large.Peek();

            aliases[s] = l;
            thresholds[l] = thresholds[l] - (1f - thresholds[s]);

            if (thresholds[l] < 1f)
            {
                small.Push(l);
                large.Pop();
            }
        }
    }

    /// <summary>
     /// 各要素の重みを考慮しながらインデックスを1つ抽出する
     /// </summary>
    public int GetRandomIndex()
    {
        int length = weights.Length;

        float random = Random.value * length;
        int index = (int)random;
        if (index == length) index = length - 1; // Random.value = 1.0 だった場合
        float weight = random - index;

        // ランダムな値の小数部分(weight)がブロック内で設定された重みの基準値(thresholds)
        // を超えたらエイリアスを返す
        if (weight < thresholds[index])
        {
            return index;
        }
        return aliases[index];
    }
}

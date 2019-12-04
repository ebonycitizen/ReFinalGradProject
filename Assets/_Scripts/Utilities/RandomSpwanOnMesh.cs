using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class RandomSpwanOnMesh : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnObjs;
    [SerializeField]
    private int spawnNum = 10;

    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3[] normals;
    private int[] triangles;

    private WeightedRandom weightedRandom;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
            return;

        // メッシュの各情報
        vertices = mesh.vertices;
        triangles = mesh.triangles;
        normals = mesh.normals;
        int trisNum = triangles.Length / 3;

        // すべての三角形の面積を計算
        float[] areas = new float[trisNum];
        Vector3 p1, p2, p3;
        for (int i = 0; i < trisNum; i++)
        {
            p1 = vertices[triangles[i * 3]];
            p2 = vertices[triangles[i * 3 + 1]];
            p3 = vertices[triangles[i * 3 + 2]];
            areas[i] = Vector3.Cross(p1 - p2, p1 - p3).magnitude;
        }

        // ランダム抽選用オブジェクトを準備
        weightedRandom = new WeightedRandom(areas);

        SpawnAmount();
    }

    private void SpawnAmount()
    {
        int curNum = 0;

        while(curNum < spawnNum)
        {
            bool hasSpawn = Spawn();
            if (hasSpawn)
                curNum += 1;
        }
    }


    // ランダムな位置にオブジェクトを作成する
    private bool Spawn()
    {
        if (spawnObjs == null) return false;
        if (weightedRandom == null) return false;

        // 各三角形の面積を考慮しつつランダムなインデックスを取得
        int randomIndex = weightedRandom.GetRandomIndex();

        int i1 = triangles[randomIndex * 3];
        int i2 = triangles[randomIndex * 3 + 1];
        int i3 = triangles[randomIndex * 3 + 2];

        // 選択した三角形の内部にあるランダムな座標を計算
        Vector3 p1 = vertices[i1];
        Vector3 p2 = vertices[i2];
        Vector3 p3 = vertices[i3];
        Vector3 pos = RandomPointInsideTriangle(p1, p2, p3);
        pos = transform.TransformPoint(pos);

        // 選択した三角形の法線方向を計算
        Vector3 n1 = normals[i1];
        Vector3 n2 = normals[i2];
        Vector3 n3 = normals[i3];
        Vector3 normal = (n1 + n2 + n3).normalized;
        normal = transform.TransformDirection(normal);

        if (normal.y > 0.5f)
        {
            int rand = Random.Range(0, spawnObjs.Length);
            GameObject spawnObj = spawnObjs[rand];


            // 作成
            GameObject obj = Instantiate(spawnObj, pos, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
            obj.transform.localScale = (obj.transform.localScale / transform.localScale.x);
            return true;
        }
        return false;
    }

    // 三角形の内部にあるランダムな点を返す
    private Vector3 RandomPointInsideTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float a = Random.value;
        float b = Random.value;
        if (a + b > 1f)
        {
            a = 1f - a;
            b = 1f - b;
        }
        float c = 1f - a - b;
        return a * p1 + b * p2 + c * p3;
    }
}

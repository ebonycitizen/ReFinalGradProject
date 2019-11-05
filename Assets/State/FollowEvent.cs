using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject glitter;

    [SerializeField]
    private MyCinemachineDollyCart dollyCart;

    [SerializeField]
    private float delayTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        glitter.gameObject.SetActive(false);
    }

    public void StartEvent()
    {
        StartCoroutine("EventStart");
    }

    private IEnumerator EventStart()
    {
        yield return new WaitForSeconds(delayTime);
        glitter.gameObject.SetActive(true);
        yield return null;
    }
    public void EndEvent()
    {
        StartCoroutine("EventEnd");
    }
    private IEnumerator EventEnd()
    {
        float time = 5;
        while(time<25)
        {

            dollyCart.m_Speed = time;
            time += Time.deltaTime*2;
            yield return null;
        }

        dollyCart.m_Speed = 25f;

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDollySpeed : MonoBehaviour
{
    [SerializeField]
    private MyCinemachineDollyCart dollyCart;
    public float max=30;
    public float speed=3;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Dolly"))
        {
            StartCoroutine("ChangeSpeed");
        }
    }

    private IEnumerator ChangeSpeed()
    {
        dollyCart = GameObject.FindObjectOfType<MyCinemachineDollyCart>();
        float time = 0;
        float dollySpeed = dollyCart.m_Speed;
        while (time < 1)
        {

            dollyCart.m_Speed = Mathf.Lerp(dollySpeed, max, time);
            time += Time.deltaTime / speed;
            yield return null;
        }

        dollyCart.m_Speed = max;

        yield return null;
    }
}

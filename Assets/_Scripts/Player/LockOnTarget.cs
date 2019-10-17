using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HI5;
using Valve.VR;

public class LockOnTarget : MonoBehaviour
{
    public SteamVR_Action_Boolean GrabAction;

    [SerializeField]
    private bool useCamera;
    [SerializeField]
    private bool useHand;

    [SerializeField]
    private RayFromCamera rayCamera;
    [SerializeField]
    private Grab rightHand;
    [SerializeField]
    private Grab leftHand;

    [SerializeField]
    private GameObject cameraCursor;
    [SerializeField]
    private GameObject rightCursor;
    [SerializeField]
    private GameObject leftCursor;

    [SerializeField]
    private int lockNumMax = 4;//ロックオンできる最大数
    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private float atkSpeedRequire;

    [SerializeField]
    private GameObject lockOnCursorPrefab;

    private List<GameObject> lockOnTargets;
    private ThirdPersonAttack player;

    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private Transform missileInitPos;

    private bool canAtack=true;
    

    private void Awake()
    {
        lockOnTargets = new List<GameObject>();

        cameraCursor.SetActive(false);
        rightCursor.SetActive(false);
        leftCursor.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Object.FindObjectOfType<ThirdPersonAttack>();

        if (useCamera)
            cameraCursor.SetActive(true);
        if(useHand)
        {
            rightCursor.SetActive(true);
            leftCursor.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useHand)
            ShowCursor();

        LockOn();
        Attack();
    }

    private void ShowCursor()
    {
        rightCursor.transform.position = rightHand.GetPalmCenterPos() + rightHand.GetForward() * rightHand.GetRayLength();
        leftCursor.transform.position = leftHand.GetPalmCenterPos() + leftHand.GetForward() * leftHand.GetRayLength();
    }

    private void LockTarget(GameObject target)
    {
        if (target != null && !lockOnTargets.Contains(target))
        {
            lockOnTargets.Add(target);
            Instantiate(lockOnCursorPrefab, target.transform);
        }
    }

    private void LockOn()
    {
        if (lockOnTargets.Count >= lockNumMax)
            return;

        GameObject rightTarget = rightHand.LockOn(targetLayer);
        GameObject leftTarget = leftHand.LockOn(targetLayer);
        GameObject cameraTarget = rayCamera.LockOn(targetLayer);

        if (useHand)
        {
            LockTarget(rightTarget);
            LockTarget(leftTarget);
        }
        if(useCamera)
        {
            LockTarget(cameraTarget);
        }
    }

    private void Attack()
    {
        if (lockOnTargets.Count <= 0 || !canAtack)
            return;

        if (rightHand.GetVelocity().magnitude >= atkSpeedRequire ||
            leftHand.GetVelocity().magnitude >= atkSpeedRequire || GrabAction.stateDown)
        {
            canAtack = false;

            //HI5_Manager.EnableBothGlovesVibration(400, 400);
            player.Attack(0);

            //delete lockon mark
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("LockOn"))
                Destroy(obj);

            StartCoroutine("Blastoff");
        }
    }

    IEnumerator Blastoff()
    {
        int directionX = 1;

        foreach (GameObject obj in lockOnTargets)
        {
            if (obj == null)
                continue;

            

            GameObject missile = Instantiate(missilePrefab, missileInitPos);
            missile.GetComponent<Missile>().initMissile(obj, directionX);

            obj.GetComponent<Collider>().enabled = false;

            StartCoroutine("DeadEffect", obj);

            directionX *= -1;

            yield return new WaitForSeconds(0.1f);
        }

        lockOnTargets.Clear();
        canAtack = true;

        yield return null;
    }

    IEnumerator DeadEffect(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        
        Instantiate(obj.GetComponent<EnemyDeadEffect>().GetDeadEffect(), obj.transform);
        obj.transform.DOScale(Vector3.zero, 0.5f);
        Destroy(obj, 1);        
    }
}

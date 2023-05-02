using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshJump : MonoBehaviour
{
    [SerializeField]
    private float _jumpSpeed = 10.0f;
    [SerializeField]
    private float _gravity = -9.81f;
    private NavMeshAgent _navAgent;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(StartJumpCo());
    }

    IEnumerator StartJumpCo()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnJump());

            yield return StartCoroutine(JumpTo());
        }
    }

    public bool IsOnJump()
    {

        if (_navAgent.isOnOffMeshLink) //오프메시에 있고 점핑 데이터라면
        {
            OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;

            if (linkData.linkType == OffMeshLinkType.LinkTypeJumpAcross ||
                linkData.linkType == OffMeshLinkType.LinkTypeDropDown)
            {
                return true;
            }
        }
        return false;
    }




    IEnumerator JumpTo()
    {
        _navAgent.isStopped = true;

        OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / _jumpSpeed);
        float currentTime = 0;
        float percent = 0;

        float v0 = (end - start).y - _gravity; // y방향 초기 속도 

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;

            //시간 경과에 따라 위치를 바꿔준다
            Vector3 pos = Vector3.Lerp(start, end, percent);

            //포물선 운동 : 시작위치 + 초기속도 * 시간 + 중력 * 시간제곱
            pos.y = start.y + (v0 * percent) + (_gravity * percent * percent);
            transform.position = pos;

            yield return null;
        }

        _navAgent.CompleteOffMeshLink();

        _navAgent.isStopped = false;
    }
}

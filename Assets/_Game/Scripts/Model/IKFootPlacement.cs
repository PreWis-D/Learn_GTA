using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField][Range(0, 1f)] private float _distanceToGround = 0.1f;

    private Animator _animator;
    private readonly int _hashIsLeftFootIK = Animator.StringToHash("IKLeftFootWeight");
    private readonly int _hashIsRightFootIK = Animator.StringToHash("IKRightFootWeight");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_animator == null) return;

        SetFoot(_hashIsLeftFootIK, AvatarIKGoal.LeftFoot);
        SetFoot(_hashIsRightFootIK, AvatarIKGoal.RightFoot);
    }

    private void SetFoot(int animHash, AvatarIKGoal avatarIKGoal)
    {
        _animator.SetIKPositionWeight(avatarIKGoal, _animator.GetFloat(animHash));
        _animator.SetIKRotationWeight(avatarIKGoal, _animator.GetFloat(animHash));

        RaycastHit hit;
        Ray ray = new Ray(_animator.GetIKPosition(avatarIKGoal) + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out hit, _distanceToGround + 1f, _layerMask))
        {
            var ground = hit.transform.GetComponent<Ground>();
            if (ground)
            {
                Vector3 footPosition = hit.point;
                footPosition.y += _distanceToGround;
                _animator.SetIKPosition(avatarIKGoal, footPosition);
                _animator.SetIKRotation(avatarIKGoal, Quaternion.LookRotation(transform.forward, hit.normal));

            }
        }
    }
}
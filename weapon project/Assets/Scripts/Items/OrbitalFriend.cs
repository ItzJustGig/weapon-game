using System.Linq;
using UnityEngine;

public class OrbitalFriend : PassiveItem
{
    public GameObject friendGO;
    public float rotatingSpeed;

    public override void Initialize()
    {
        base.Initialize();
        friendGO = Instantiate(friendGO, owner.transform.position, owner.transform.rotation);
        friendGO.GetComponent<OrbitalFriendSummon>().Initialize(owner, rotatingSpeed);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Destroy(friendGO);
    }

}

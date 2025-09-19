using UnityEngine;

public class FourLegsMonster : Monster
{
    public override void WalkOnGround()
    {
        // 4족 보행으로 이동.
        Debug.Log("4족 보행!!");
    }
}

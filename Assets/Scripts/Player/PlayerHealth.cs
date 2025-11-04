using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerRespawn respawn;
    
    public int maxHP = 3;

    private int currentHP;

    private void Start()
    {
        InitHP();
    }

    void InitHP()
    {
        currentHP = maxHP;
        GameplayEvents.RaisePlayerChagendHP(currentHP);
    }
    
    public void ApplyDamage()
    {
        --currentHP;

        GameplayEvents.RaisePlayerChagendHP(currentHP);

        if(currentHP == 0)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.0f);

        if (respawn != null)
        {
            respawn.Respawn();
            InitHP();
        }
    }

    public bool IsPlayerAlive()
    {
        if(currentHP > 0)
        {
            return true;
        }

        return false;
    }
}

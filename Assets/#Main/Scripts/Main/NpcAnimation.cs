using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimation : MonoBehaviour
{
    public Animator anim;
    

    public void SetWalking(bool con)
    {
        anim.SetBool("IsWalking", con);
    }
}

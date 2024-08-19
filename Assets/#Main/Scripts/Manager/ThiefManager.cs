using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    public Transform posSpawn;
    public SendalMovement sendal;
    public Animator animIbu;
    public GameObject obj;
    public float timeDelay;
    void Start()
    {
        
    }

    public void ShootSendal(GameObject obj)
    {
        this.obj = obj;
        animIbu.SetTrigger("IsShoot");
        Invoke(nameof(Shoot), timeDelay);
    }

    private void Shoot()
    {
        sendal.gameObject.SetActive(true);
        sendal.MoveTo(posSpawn, obj);
        MusicManager.instance.PlaySFX(SFX.LEMPARSENDAL);
    }
}

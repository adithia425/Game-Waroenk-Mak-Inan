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

    public bool isShooting;
    void Start()
    {
        
    }

    public bool TriggerSkillKeamanan(GameObject obj)
    {

        if (sendal.gameObject.activeInHierarchy)
        {
            //Debug.Log("Sendal sedang aktif, skill pasif batal");
            return false;
        }
        int levelSkill = SaveManager.instance.GetLevelKeamanan();
        int chances = DatabaseManager.instance.GetEffectKeamanan(levelSkill);

        int valRandom = Random.Range(0, 100);
        
        if (valRandom <= chances)
        {
            ShootSendal(obj);
            return true;
        }
        return false;
    }

    public void ShootSendal(GameObject obj)
    {

        //Debug.Log("Shoot sendal");
        this.obj = obj;
        isShooting = true;


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

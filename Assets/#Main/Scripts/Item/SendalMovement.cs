using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendalMovement : MonoBehaviour
{
    public NpcController npc;
    public Transform target; // Titik tujuan
    public float speed; // Kecepatan gerakan
    public float rotationSpeed;
    private bool isMoving = true;

    public GameObject objectEffectLedak;

    public void MoveTo(Transform posSpawn, GameObject target)
    {
        npc = target.GetComponent<NpcController>();
        this.target = target.transform;
        transform.position = posSpawn.position;
        isMoving = true;
    }
    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        // Gerakkan objek ke arah titik tujuan
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Cek apakah objek sudah sampai di titik tujuan
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            Instantiate(objectEffectLedak, transform.position, Quaternion.identity);
            npc.ActionThieFailedDone();
            isMoving = false;
            gameObject.SetActive(false);
        }
    }
}

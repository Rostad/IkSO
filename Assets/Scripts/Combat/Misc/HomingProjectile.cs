using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HomingProjectile : MonoBehaviour
{
    public float speed;
    public float maxLifeTime;
    public GameObject target;
    //private ITargetable target;
    private GameObject spellVFX;

    public void Setup(GameObject spellVFX, GameObject target, float projectileSpeed)
    {
        this.spellVFX = Instantiate(spellVFX, transform.position, Quaternion.identity);
        this.spellVFX.transform.SetParent(transform);
        this.target = target;
        speed = projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            Destroy(this.gameObject);
        }
    }
}

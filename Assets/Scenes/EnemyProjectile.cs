using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField][Range(1, 30)] private float speed = 10f;
    [SerializeField][Range(1, 50)] private int damage = 1;
    [SerializeField][Range(0, 1)] private float projectileLifeTime;

    private void Update()
    {
        StartCoroutine(LifeTime());
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            BaseHealthSystem hs = other.gameObject.GetComponent<BaseHealthSystem>();
            if(hs != null)
            {
                hs.TakeDamage(damage);
            }
        }
    }

    private IEnumerator LifeTime(){
    yield return new WaitForSeconds(projectileLifeTime);
    Destroy(gameObject);
}

}

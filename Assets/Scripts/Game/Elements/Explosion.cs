using System.Collections;
using UnityEngine;
class Explosion : MonoBehaviour
{
    private float _timeToDie = 1.5f;
    private float _explosionRadius = 2.0f;
    private void Start()
    {
        float radius = GetComponent<CircleCollider2D>().radius;
        float coef = _explosionRadius / radius;
        transform.localScale = new Vector3(coef, coef, 1.0f);
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(_timeToDie);
        Destroy(gameObject);
    }
}

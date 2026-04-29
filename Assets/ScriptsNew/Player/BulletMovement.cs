using System.Collections;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private IEnumerator _corroutineMoving;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartMoving(Transform startPos, float distance, float height, float duration, GameObject player)
    {
        transform.position = startPos.position;

        float speed = distance / duration;
        Vector3 direction = startPos.forward;

        float time = 0f;
        while (time < duration)
        {
            transform.position += direction * speed * Time.deltaTime;

            float t = time / duration;
            float yOffset = height * t * (1f - t);

            Vector3 pos = transform.position;
            pos.y = startPos.position.y + yOffset;

            transform.position = pos;

            if (pos.y <= 0f)
                yield break;

            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    public void Shoot(Transform start, float distance, float height, float duration, GameObject player)
    {
        if (_corroutineMoving != null)
            StopCoroutine(_corroutineMoving);

        _corroutineMoving = StartMoving(start, distance, height, duration, player);
        StartCoroutine(_corroutineMoving);
    }
}
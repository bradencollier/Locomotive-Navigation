using UnityEngine;

public class Starfield : MonoBehaviour
{
    public GameObject starPrefab;
    public int starCount = 2000;
    public float radius = 500f;
    public int seed = 0;

    void Start()
    {
        Random.InitState(seed);
        GenerateStars();
    }

    void GenerateStars()
    {
        for (int i = 0; i < starCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * radius;

            GameObject star = Instantiate(
                starPrefab,
                transform.position + pos,
                Quaternion.identity,
                transform
            );
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetList : MonoBehaviour
{
    [SerializeField] List<Transform> targetTransforms = new List<Transform>();
    [SerializeField] List<Color> targetColors = new List<Color>();
    [SerializeField] float duration = 2f;
    [SerializeField] GameObject referenceObject;
    [SerializeField] AnimationCurve curve;
    [SerializeField] string sceneName;
    void Start()
    {
        StartCoroutine(CountOverTime());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Carga la escena especificada en el Inspector
            SceneManager.LoadScene(sceneName);
        }
    }
    IEnumerator CountOverTime()
    {
        int count = targetTransforms.Count;
        while (true)
        {
            
            for (int i = 0; i < count; i++)
            {
                int nextIndex = (i + 1) % count; // esto hace que cuanfo se quede el el ultimo vaya al primero
                float elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float normalizedElapsedTime = elapsedTime / duration;
                    Vector3 finalPosition = Vector3.Lerp(
                        targetTransforms[i].position,
                        targetTransforms[nextIndex].position,
                        curve.Evaluate(normalizedElapsedTime)
                    );
                    Quaternion finalRotation = Quaternion.Lerp(
                        targetTransforms[i].rotation,
                        targetTransforms[nextIndex].rotation,
                        curve.Evaluate(normalizedElapsedTime)
                    );
                    Vector3 finalScale = Vector3.Lerp(
                        targetTransforms[i].localScale,
                        targetTransforms[nextIndex].localScale,
                        curve.Evaluate(normalizedElapsedTime)
                    );
                    Color finalColor = Color.Lerp(
                        targetColors[i],
                        targetColors[nextIndex],
                        curve.Evaluate(normalizedElapsedTime)
                    );

                    referenceObject.transform.position = finalPosition;
                    referenceObject.transform.rotation = finalRotation;
                    referenceObject.transform.localScale = finalScale;
                    referenceObject.GetComponent<MeshRenderer>().material.color = finalColor;

                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}



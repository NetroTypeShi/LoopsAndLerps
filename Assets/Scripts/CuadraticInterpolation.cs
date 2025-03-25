using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CuadraticInterpolation : MonoBehaviour
{
    [SerializeField]  GameObject pointD;
    [SerializeField]  GameObject pointE;
    [SerializeField]  GameObject pointF;

     Transform whereIsD;
     Transform whereIsE;
     Transform whereIsF;

    [SerializeField]  Transform whereIsA;
    [SerializeField]  Transform whereIsB;
    [SerializeField]  Transform whereIsC;

    [SerializeField]  Color fromColorD = Color.white;
    [SerializeField]  Color toColorD = Color.blue;

    [SerializeField]  Color fromColorE = Color.white;
    [SerializeField]  Color toColorE = Color.red;

    [SerializeField]  Color fromColorF = Color.white;
    [SerializeField]  Color toColorF = Color.green;

    [SerializeField]  AnimationCurve curve;

    [SerializeField]  float duration = 2f;

    [SerializeField]  float speed = 2f;

    [SerializeField] string sceneName;

    private void Start()
    {
        whereIsD = Instantiate(pointD, whereIsA.position, Quaternion.identity).transform;
        whereIsE = Instantiate(pointE, whereIsC.position, Quaternion.identity).transform;
        whereIsF = Instantiate(pointF, whereIsA.position, Quaternion.identity).transform;

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
    private IEnumerator CountOverTime()
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedElapsedTime = elapsedTime / duration;
                float eval = curve.Evaluate(normalizedElapsedTime);

                
                Vector3 finalTransformD = Vector3.Lerp(whereIsA.position, whereIsC.position, eval);
                Vector3 finalTransformE = Vector3.Lerp(whereIsC.position, whereIsB.position, eval);
                Vector3 finalTransformF = Vector3.Lerp(whereIsD.position, whereIsE.position, eval);

                whereIsD.position = finalTransformD;
                whereIsE.position = finalTransformE;
                whereIsF.position = finalTransformF;

                
                pointD.transform.position = whereIsD.position;
                pointE.transform.position = whereIsE.position;
                pointF.transform.position = whereIsF.position;

                
                pointD.GetComponent<MeshRenderer>().sharedMaterial.color = Color.Lerp(fromColorD, toColorD, eval);
                pointE.GetComponent<MeshRenderer>().sharedMaterial.color = Color.Lerp(fromColorE, toColorE, eval);
                pointF.GetComponent<MeshRenderer>().sharedMaterial.color = Color.Lerp(fromColorF, toColorF, eval);

                yield return new WaitForEndOfFrame(); 
            }
        }
    }
}


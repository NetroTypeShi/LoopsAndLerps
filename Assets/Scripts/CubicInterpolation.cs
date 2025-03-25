using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubicInterpolation : MonoBehaviour
{
    [SerializeField] GameObject pointE;
    [SerializeField] GameObject pointF;
    [SerializeField] GameObject pointG;
    [SerializeField] GameObject pointH;
    [SerializeField] GameObject pointI;
    [SerializeField] GameObject objetive;

    [SerializeField] Transform whereIsA;
    [SerializeField] Transform whereIsB;
    [SerializeField] Transform whereIsC;
    [SerializeField] Transform whereIsD;

    Transform whereIsE;
    Transform whereIsF;
    Transform whereIsG;
    Transform whereIsH;
    Transform whereIsI;
    Transform whereIsObjetive;

    [SerializeField] Color fromColorObjetive = Color.white;
    [SerializeField] Color toColorObjetive = Color.red;

    [SerializeField] Color fromColorE = Color.white;
    [SerializeField] Color toColorE = Color.yellow;

    [SerializeField] Color fromColorF = Color.white;
    [SerializeField] Color toColorF = Color.cyan;

    [SerializeField] Color fromColorG = Color.white;
    [SerializeField] Color toColorG = Color.green;

    [SerializeField] Color fromColorH = Color.white;
    [SerializeField] Color toColorH = Color.magenta;

    [SerializeField] Color fromColorI = Color.white;
    [SerializeField] Color toColorI = Color.blue;

    MeshRenderer rendererObjetive;
    MeshRenderer rendererE;
    MeshRenderer rendererF;
    MeshRenderer rendererG;
    MeshRenderer rendererH;
    MeshRenderer rendererI;

    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration = 2f;
    [SerializeField] float speed = 2f;

    [SerializeField] string sceneName;
    void Start()
    {
        whereIsE = Instantiate(pointE, whereIsA.position, Quaternion.identity).transform;
        whereIsF = Instantiate(pointF, whereIsD.position, Quaternion.identity).transform;
        whereIsG = Instantiate(pointG, whereIsC.position, Quaternion.identity).transform;
        whereIsH = Instantiate(pointH, whereIsE.position, Quaternion.identity).transform;
        whereIsI = Instantiate(pointI, whereIsG.position, Quaternion.identity).transform;
        whereIsObjetive = Instantiate(objetive, whereIsA.position, Quaternion.identity).transform;

        rendererObjetive = objetive.GetComponent<MeshRenderer>();
        rendererE = pointE.GetComponent<MeshRenderer>();
        rendererF = pointF.GetComponent<MeshRenderer>();
        rendererG = pointG.GetComponent<MeshRenderer>();
        rendererH = pointH.GetComponent<MeshRenderer>();
        rendererI = pointI.GetComponent<MeshRenderer>();

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
        float normalizedElapsedTime;
        float elapsedTime = 0f;

        Vector3 finalTransformE;
        Vector3 finalTransformF;
        Vector3 finalTransformG;
        Vector3 finalTransformH;
        Vector3 finalTransformI;
        Vector3 finalTransformObjetive;

        while (true)
        {
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                normalizedElapsedTime = elapsedTime / duration;
                float eval = curve.Evaluate(normalizedElapsedTime);

                finalTransformE = Vector3.Lerp(whereIsA.position, whereIsC.position, eval);
                finalTransformF = Vector3.Lerp(whereIsC.position, whereIsD.position, eval);
                finalTransformG = Vector3.Lerp(whereIsD.position, whereIsB.position, eval);
                finalTransformH = Vector3.Lerp(whereIsE.position, whereIsF.position, eval);
                finalTransformI = Vector3.Lerp(whereIsG.position, whereIsG.position, eval);
                finalTransformObjetive = Vector3.Lerp(whereIsH.position, whereIsI.position, eval);

                Color finalColorObjetive = Color.Lerp(fromColorObjetive, toColorObjetive, eval);
                Color finalColorE = Color.Lerp(fromColorE, toColorE, eval);
                Color finalColorF = Color.Lerp(fromColorF, toColorF, eval);
                Color finalColorG = Color.Lerp(fromColorG, toColorG, eval);
                Color finalColorH = Color.Lerp(fromColorH, toColorH, eval);
                Color finalColorI = Color.Lerp(fromColorI, toColorI, eval);

                whereIsE.position = finalTransformE;
                whereIsF.position = finalTransformF;
                whereIsG.position = finalTransformG;
                whereIsH.position = finalTransformH;
                whereIsI.position = finalTransformI;
                whereIsObjetive.position = finalTransformObjetive;

                pointE.transform.position = whereIsE.position;
                pointF.transform.position = whereIsF.position;
                pointG.transform.position = whereIsG.position;
                pointH.transform.position = whereIsH.position;
                pointI.transform.position = whereIsI.position;
                objetive.transform.position = whereIsObjetive.position;

                rendererObjetive.sharedMaterial.color = finalColorObjetive;
                rendererE.sharedMaterial.color = finalColorE;
                rendererF.sharedMaterial.color = finalColorF;
                rendererG.sharedMaterial.color = finalColorG;
                rendererH.sharedMaterial.color = finalColorH;
                rendererI.sharedMaterial.color = finalColorI;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}

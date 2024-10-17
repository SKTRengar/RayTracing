using System.Collections;
using System.Collections.Generic;
using UnityEngine;






//[ExecuteInEditMode]

public class CloudGenerator : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    static int clip_id = Shader.PropertyToID("_Cutoff");

    [SerializeField, Range(0, 100)]
    float pow = 3f;
    [SerializeField, Range(0, 1)]
    float Alpha_clip = 0.1f;
    [SerializeField, Range(0, 500)]
    float scale = 10f;
    Transform prefab;
    public int instanceCount = 100;
    public float Height = 100;
    
    public Material instanceMat;
    public Mesh instanceMesh;
    Matrix4x4[] matrices;
    Vector4[] colors;
    MaterialPropertyBlock block;
    float[] edgeFade;
    float Edgesize = 2f;
    float[] clip;
    
    //  MaterialPropertyBlock block;
    // LightShadowCasterMode castShadows;
    public float High;

    void BuildMatrixAndBlock()
    {
        block = new MaterialPropertyBlock();

        float Count = instanceCount;
        Vector4[] colors = new Vector4[instanceCount];
        matrices = new Matrix4x4[instanceCount];
        float[] offsets = new float[instanceCount];
        float[] clip = new float[instanceCount];
       // float[] edgeFade = new float[300];
        

        for (int i = 0; i < instanceCount; i++)
        {
            colors[i] =Color.white * i / Count ;// new Color(Random.value, Random.value, Random.value, 1f);//Color.white * i / Count;
            matrices[i] = Matrix4x4.TRS(
                Random.insideUnitSphere * 10f, Quaternion.identity, Vector3.one
            );

            offsets[i] = i * High / Count;
            clip[i] = Mathf.Pow(i /Count * 2 - 1/2 *High + 0.1f, pow)+Alpha_clip;// 4 * Mathf.Pow(i/Count, 2) + 4 * High * (i/Count) - High;//Mathf.Pow(i / Count * 2 - 1 ,pow);
            matrices[i] = Matrix4x4.TRS(new Vector3(0 ,offsets[i]+ Height , 0), Quaternion.identity, Vector3.one*scale);
            //for (int j = 0;j < scale; j++)
            //{
             //   edgeFade[j] = 1 - j / Edgesize;

           // }


        }
        

        Debug.Log(clip);

        block.SetVectorArray(baseColorId, colors);
        block.SetFloatArray(clip_id,clip);
        block.SetFloatArray("_offset", offsets);
        block.SetFloat("_CloudScale", scale);
        block.SetFloat("_Edgesize", Edgesize);
        //block.SetFloatArray("_edgeFade", edgeFade);

    }
    // Start is called before the first frame update
    void Start()
    {
        BuildMatrixAndBlock();

    }

    // Update is called once per frame
    void Update()
    {
       if (block == null)
        {
            block = new MaterialPropertyBlock();
            // block.SetVectorArray(baseColorId, colors);
            // block.SetFloatArray(clip_id, clip);
        }
        BuildMatrixAndBlock();
        var support = SystemInfo.supportsInstancing;
        Debug.Log("Instance rendering" + support);
        // 打印每个云朵的生成位置
        for (int i = 0; i < instanceCount; i++)
        {
            Debug.Log($"Cloud {i} Position: {matrices[i].m03}, {matrices[i].m13}, {matrices[i].m23}");
        }
        Graphics.DrawMeshInstanced(instanceMesh, 0, instanceMat, matrices, instanceCount,block,UnityEngine.Rendering.ShadowCastingMode.On, false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPRO : MonoBehaviour
{
    private TMP_Text _tmpText;
    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _tmpText.ForceMeshUpdate();
        TMP_TextInfo textInfo = _tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible == false) continue;

            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            int vIndex = charInfo.vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                Vector3 orgin = vertices[vIndex + j];
                vertices[vIndex + j] = orgin + new Vector3(0, Mathf.Sign(Time.deltaTime * 10f + orgin.x), 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;

            _tmpText.UpdateGeometry(meshInfo.mesh, 1);
        }
    }
}

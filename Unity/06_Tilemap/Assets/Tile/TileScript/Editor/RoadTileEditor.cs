using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(RoadTile))]
public class RoadTileEditor : Editor
{
    private RoadTile roadTile;

    private void OnEnable()
    {
        roadTile = target as RoadTile;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (roadTile != null)
        {
            if (roadTile.preview != null)
            {
                EditorGUILayout.LabelField("Preview Image");
                Texture2D previewTexture = AssetPreview.GetAssetPreview(roadTile.preview);
                GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), previewTexture);
            }

            if (roadTile.sprites != null)
            {
                EditorGUILayout.LabelField("Sprites Preview Image");
                GUILayout.BeginHorizontal();
                for (int i = 0; i < roadTile.sprites.Length; i++)
                {
                    Texture2D spritesTexture = AssetPreview.GetAssetPreview(roadTile.sprites[i]);
                    GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
                    GUI.DrawTexture(GUILayoutUtility.GetLastRect(), spritesTexture);
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif

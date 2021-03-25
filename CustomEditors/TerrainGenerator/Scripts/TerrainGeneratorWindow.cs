#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CustomEditors.TerrainGenerator.Scripts
{
    public class TerrainGeneratorWindow : EditorWindow
    {
        private Object _terrain;
        private bool isGenerate = false;
        
        
        [MenuItem("Window/TerrainGenerator/New Window")]
        static void Init()
        {
            TerrainGeneratorWindow window =
                (TerrainGeneratorWindow) EditorWindow.GetWindow(typeof(TerrainGeneratorWindow), false, "Terrain Generator");
            window.Show();
        }
        
        void OnGUI()
        {
            _terrain = EditorGUILayout.ObjectField("Terrain", _terrain, typeof(Object), true);
            isGenerate = GUILayout.Button("Generate");
        }

        private void Update()
        {
            if (isGenerate) Debug.Log("Clicked");
        }
    }
}
#endif
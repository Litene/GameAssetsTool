using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using System.IO;

namespace GameAssets { // remove case sensetivity
    public static class Assets { // should pooling be a part of this?
        private static Database _dataBase;
        //private static List<BackupString> _backUp = new List<BackupString>();
        //private static string _path() => string.Join(Application.persistentDataPath, "/backUp.json");

        // public static Database GetDatabase() { // this is actually 
        //     return _dataBase;
        // }
        // private static Assets _instance;
        // public static Assets I { // dont destroy on load? also should it really be a sceneobject? singleton?
        //     get {
        //         _instance ??= FindInScene();
        //         return _instance ??= GenerateSingleton();
        //     }
        // }
        // private static Assets FindInScene() => FindObjectOfType<Assets>();
        // private static Assets GenerateSingleton() {
        //     GameObject gameManagerObject = new GameObject("Assets");
        //     return gameManagerObject.AddComponent<Assets>();
        // }

        //delete entry

        //delete all entrys

        //edit entry

        //search methods for project

        //method for syncing files both ways. 

        //add entry

        /// <summary>
        /// Returns first item with key <param name="key"></param>> and returns it type <param name="T"></param>
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetAsset<T>(string key) where T : Object { // might need to object instead. 
            if (!_dataBase) {
                _dataBase = CreateAsset();
            }

            try {
                return _dataBase.GetAssetFromDictionary(key) as T;
            }
            catch (Exception e) {
                Debug.LogError($"Asset with key: {key} does not exist within database");
                throw;
            }
        }

        /// <summary>
        /// Returns first item in database with type <param name="T"></param>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetAsset<T>() where T : Object {
            if (!_dataBase) _dataBase = CreateAsset();
            try {
                return _dataBase.GetAssetFromDictionary<T>();
            }
            catch (Exception e) {
                Debug.LogError($"Asset with type does not exist within database");
                throw;
            }
        }

        /// <summary>
        /// Returns first item with key <param name="key"></param>> and returns it as
        /// <typeparam name="Object"></typeparam> might need a cast>
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Object GetAsset(string key) {
            if (!_dataBase) _dataBase = CreateAsset();
            try {
                return _dataBase.GetAssetFromDictionary(key);
            }
            catch (Exception e) {
                Debug.LogError($"Asset with key: {key} does not exist within database");
                throw;
            }
        }

        // should not be accessed through code afterwards
        internal static void AddAsset(string key, Object addedObject) { // needs to get path.
            EditorUtility.SetDirty(_dataBase);
            // _backUp.Add(new BackupString(addedObject.name, key));
            // if (File.Exists(_path())) {
            //     string jsonFile = JsonUtility.ToJson(_backUp);
            //     File.WriteAllText(_path(),jsonFile);
            // }
            _dataBase.AddAssetToDictionary(key, addedObject);
        }

        private static Database CreateAsset() {
            Debug.Log("this shouldn't be called");
            Database newDataBase = ScriptableObject.CreateInstance<Database>();
            AssetDatabase.CreateAsset(newDataBase, "Assets/Resources/Databases/Database.asset");
            return newDataBase;
        }

        static Assets() {
            _dataBase = Resources.Load("Databases/Database") as Database;
            if (!_dataBase) _dataBase = CreateAsset();
        }

        

        

        // [Serializable] private class BackupString {
        //     public string SceneName;
        //     public string Location;
        //     public string Name;
        //     public string Key;
        //     
        //     public BackupString(string name, string key, string location = null) {
        //         Location = location;
        //         Name = name;
        //         Key = name;
        //     }
        // }
    }
}

public class Database : ScriptableObject { // maybe not a dictionary?
    private Dictionary<string, ObjectWrapper<Object>> _iDataBase; // add to asset, change to list to serialize list
    public void AddAssetToDictionary(string key, Object addedObject) {
        if (_iDataBase == null) _iDataBase = new Dictionary<string, ObjectWrapper<Object>>();
        _iDataBase.Add(key, new ObjectWrapper<Object>(addedObject));
    }

    public Database() {
        // called every time anything within it called
        Debug.Log("yo");
    } // this should set / search when created?

    [CanBeNull] public Object GetAssetFromDictionary(string key) {
        Debug.Log("we do be in here");
        _iDataBase.TryGetValue(key, out ObjectWrapper<Object> dBaseItem);
        // foreach (var VARIABLE in COLLECTION) {
        //     _iDataBase.get
        // }
        Debug.Log(dBaseItem);
        return dBaseItem == null ? null : dBaseItem.Reference;
    }

    [CanBeNull] public T GetAssetFromDictionary<T>() where T : Object =>
        (from value in _iDataBase.Values
            where typeof(T) == value.GetType()
            select value.Reference as T).FirstOrDefault();
}

public class ObjectWrapper<TR> : Object where TR : Object { // object wrapper is needed for constructors.
    public readonly TR Reference;

    //private readonly Editor _editor;
    public ObjectWrapper(TR objectReference) {
        Reference = objectReference;
        //_editor = Editor.CreateEditor(objectReference);
    }

    public void Draw(float t) {
        // if (!_editor) {
        //     _editor.DrawDefaultInspector();
        // }
    }

}
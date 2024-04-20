using System.Collections.Generic;
using UnityEngine;

public class BaseObjectManager : MonoBehaviour{
    // Attributes
    protected string ManagerName = "Manager";
    
    // Functions
    public virtual void LogObjects(){
        WorldObject[] worldObjects = GetComponentsInChildren<WorldObject>();
        string idArray = "[";
        for (int i = 0; i < worldObjects.Length; i++){
            idArray += worldObjects[i].Id;
            if(i != worldObjects.Length - 1) idArray += ",";
        }
        idArray += "]";
        
        Debug.Log(string.Format("Object ids in {0}: {1}", ManagerName, idArray));
    }

    public virtual WorldObject[] GetWorldObjects(){
        return GetComponentsInChildren<WorldObject>();
    }

    public virtual WorldObject GetWorldObject(string id){
        WorldObject[] worldObjects = GetComponentsInChildren<WorldObject>();
        for (int i = 0; i < worldObjects.Length; i++){
            if(worldObjects[i].Id == id) return worldObjects[i];
        }
        return null;
    }

    public virtual void RemoveWorldObject(string id){
        WorldObject worldObject = GetWorldObject(id);
        Destroy(worldObject.gameObject);
    }
}
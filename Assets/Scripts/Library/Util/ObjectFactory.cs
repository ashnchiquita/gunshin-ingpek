using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class ObjectFactory{
    public static GameObject CreateObject(
        GameObject gameObject,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        GameObject returnObject = parent == null? GameObject.Instantiate(gameObject) : GameObject.Instantiate(gameObject, parent);
        if(position != null) returnObject.transform.position = position.Value;
        if(rotation != null) returnObject.transform.rotation = rotation.Value;
        if(scale != null) returnObject.transform.localScale = Vector3.Scale(returnObject.transform.localScale, scale.Value);
        if(returnObject.TryGetComponent<Renderer>(out var renderer)) renderer.sortingOrder = renderingOrder;
        returnObject.name = objectName;

        return returnObject;
    }

    public static GameObject CreateObject(
        string prefabPath,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
        if(prefabObject == null) Debug.LogError("Prefab not found: " + prefabPath);
        return CreateObject(prefabObject, parent, position, scale, rotation, renderingOrder, objectName);
    }

    public static GameObject CreateAttackObject(
        string prefabPath,
        float damage,
        float knockbackPower,
        AttackObjectType type,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
        if(prefabObject == null) Debug.LogError("Prefab not found: " + prefabPath);
        if(!prefabObject.TryGetComponent<IAttack>(out var attackObject)) Debug.LogError("Loaded prefab is not an IAttack: " + prefabPath);

        attackObject.Damage = damage;
        attackObject.KnockbackPower = knockbackPower;

        switch (type){
            case AttackObjectType.PLAYER:
                prefabObject.layer = LayerMask.NameToLayer(GameEnvironmentConfig.LAYER_PLAYER_HITBOX);
                attackObject.Damage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].PlayerDamageMultiplier;
                break;
            case AttackObjectType.ENEMY:
                prefabObject.layer = LayerMask.NameToLayer(GameEnvironmentConfig.LAYER_ENEMY_HITBOX);
                attackObject.Damage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].EnemyDamageMultiplier;
                break;
            default:
                Debug.LogError("Invalid AttackObjectType set, please refer to enum AttackObjectType for valid types");
                break;
        }

        return CreateObject(prefabObject, parent, position, scale, rotation, renderingOrder, objectName);
    }

    public static void Destroy(GameObject gameObject, float delay = 0){
        if(gameObject == null){
            Debug.LogError("Tried to destroy a null gameObject");
            return;
        }
        GameController.instance.StartCoroutine(DestroyWithDelay(gameObject, delay));
    }

    private static IEnumerator DestroyWithDelay(GameObject gameObject, float delay){
        yield return new WaitForSeconds(delay);
        GameObject.Destroy(gameObject);
    }
}

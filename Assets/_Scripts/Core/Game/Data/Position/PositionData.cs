using System;
using UnityEngine;

namespace _Scripts.Core.Game.Data.Position
{
    [Serializable]
    public class PositionData
    {
        public int level = 1;
        public Vector3 point = new(0, 0, 0);


        public void Load(string json)
        {
            PositionData data = JsonUtility.FromJson<PositionData>(json);
            if (data != null)
            {
                level = data.level;
                point = data.point;    
            }
            
        }

        public string SaveToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
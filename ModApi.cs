using UnityEngine;

namespace Operation_Gensis_v1
{
    public class ModApi : IModApi
    {
        public void InitMod(Mod modInstance)
        {
            Debug.Log(
                "[Operation Genesis] ModApi.InitMod()"
            );

            Class1.Initialize();
        }
    }
}
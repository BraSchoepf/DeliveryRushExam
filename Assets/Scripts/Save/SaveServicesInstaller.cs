using UnityEngine;

namespace DeliveryRushExam.Save
{
    public class SaveServicesInstaller : MonoBehaviour
    {
        public enum SaveMode
        {
            Local,
            Cloud
        }

        [SerializeField] private SaveMode saveMode = SaveMode.Local;

        private void Awake()
        {
            ISaveService service = saveMode == SaveMode.Local
                ? (ISaveService)new LocalSaveService()
                : new UgsCloudSaveService();

            ServiceLocator.Register<ISaveService>(service);
        }
    }
}

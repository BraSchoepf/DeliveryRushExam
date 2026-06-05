using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryRushExam.Data;
using UnityEngine;

#if DELIVERY_RUSH_UGS
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
#endif

namespace DeliveryRushExam.Save
{
    public class UgsCloudSaveService : ISaveService
    {
        private const string ProgressKey = "delivery_rush_progress";

        public async Task<PlayerProgressData> LoadAsync()
        {
#if DELIVERY_RUSH_UGS
            var keys = new HashSet<string> { ProgressKey };
            var result = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            if (result.TryGetValue(ProgressKey, out var item))
            {
                string json = item.Value.GetAsString();
                return JsonUtility.FromJson<PlayerProgressData>(json) ?? new PlayerProgressData();
            }

            return new PlayerProgressData();
#else
            Debug.LogWarning("UGS Cloud Save no está habilitado.");
            await Task.Yield();
            return new PlayerProgressData();
#endif
        }

        public async Task SaveAsync(PlayerProgressData progressData)
        {
#if DELIVERY_RUSH_UGS
            progressData.TouchSaveDate();
            string json = JsonUtility.ToJson(progressData);
            var data = new Dictionary<string, object> { { ProgressKey, json } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
#else
            Debug.LogWarning("UGS Cloud Save no está habilitado.");
            await Task.Yield();
#endif
        }
    }
}

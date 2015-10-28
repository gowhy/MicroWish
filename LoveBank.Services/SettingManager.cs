using LoveBank.Cache;
using LoveBank.Common;
using LoveBank.Common.Config;

namespace LoveBank.Services
{
    public class SettingManager
    {
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <typeparam name="T">������Ϣ�����ͣ��̳���SettingBase</typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : class, IConfig,new()
        {
            var cacheKey = CacheKeys.SETTINGS_MESSAGE.FormatWith(typeof(T).Name);

            var cache = LoveBankCache.GetCacheService();

            var setting = cache.RetrieveObject(cacheKey) as T;

            if (setting == null) {

                var manager = new DefaultConfigManager(Provider);

                setting = manager.LoadConfig<T>() ?? new T();

                cache.AddObject(cacheKey, setting);
            }

            return setting;
        }

        /// <summary>
        /// ����������Ϣ
        /// <param name="config">��������ö���</param>
        /// </summary>
        public static void Save(IConfig config)
        {
            var cacheKey = CacheKeys.SETTINGS_MESSAGE.FormatWith(config.GetType().Name);

            var cache = LoveBankCache.GetCacheService();

            var manager = new DefaultConfigManager(Provider);

            manager.SaveConfig(config);

            cache.RemoveObject(cacheKey);
        }


        private static IConfigProvider Provider
        {
            get
            {
                return IoC.Resolve<IConfigProvider>();
            }
        }
    }
}

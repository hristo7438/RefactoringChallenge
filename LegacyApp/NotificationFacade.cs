using System;
using System.Linq;
using System.Reflection;

namespace LegacyApp
{
	public class NotificationFacade
	{
		private readonly string _serviceType;

		public NotificationFacade(string serviceType)
		{
			_serviceType = serviceType;
		}

		public INotificationService GetNotificationProvider()
		{
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => typeof(INotificationService).IsAssignableFrom(p) && p.IsInterface == false).ToList();

			var type = types.FirstOrDefault(x => x.GetCustomAttribute<NotificationProviderTypeAttribute>()?.ProviderType == _serviceType);

			if (type == null)
				throw new NotSupportedException($"Notification provider of type '{_serviceType}' is currently unsupported!");

			return (INotificationService)Activator.CreateInstance(type);
		}
	}
}
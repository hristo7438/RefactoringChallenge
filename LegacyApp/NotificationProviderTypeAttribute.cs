using System;

namespace LegacyApp
{
	public class NotificationProviderTypeAttribute : Attribute
	{
		public NotificationProviderTypeAttribute(string providerType)
		{
			ProviderType = providerType;
		}

		public string ProviderType { get; private set; }
	}
}
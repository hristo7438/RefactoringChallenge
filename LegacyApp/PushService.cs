namespace LegacyApp
{
	[NotificationProviderType("Push")]
	public class PushService : INotificationService
	{
		public bool Send(string message)
		{
			return true;
		}
	}
}
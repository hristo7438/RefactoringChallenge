namespace LegacyApp
{
	[NotificationProviderType("Email")]
	public class EmailServiceWrapper : INotificationService
	{
		public bool Send(string message)
			=> EmailService.Send(message);
	}
}
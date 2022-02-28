namespace LegacyApp
{
	[NotificationProviderType("Sms")]
	public class SmsServiceWrapper : INotificationService
	{
		private readonly SmsService _smsService;

		public SmsServiceWrapper()
		{
			_smsService = new SmsService();
		}

		public bool Send(string message)
			=> _smsService.Send(message);
	}
}
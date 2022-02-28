using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyApp
{
	public class SignupService
	{
		public SignupServiceResult AddUser(string firname, string surname, string email, string phone,
		   DateTime dateOfBirth, int clientId, string notifications)
		{
			if (IsValidUser(firname, surname, phone, email, dateOfBirth) == false)
				return new SignupServiceResult();

			List<string> notificationsList = notifications.Split("|").Where(x => string.IsNullOrWhiteSpace(x) == false).ToList();
			var notificationsResult = new Dictionary<string, bool>();

			foreach (string notificationType in notificationsList)
			{
				NotificationFacade notificationFacade = new NotificationFacade(notificationType);
				INotificationService notificationService = notificationFacade.GetNotificationProvider();
				notificationsResult[notificationType] = notificationService.Send("Welcome to e-Boks");
			}

			User user = new User
			{
				Id = clientId,
				DateOfBirth = dateOfBirth,
				EmailAddress = email,
				Firstname = firname,
				Surname = surname,
				Notifications = notifications
			};

			UserDataAccess.AddUser(user);

			return new SignupServiceResult
			{
				IsSuccess = true,
				Notifications = notificationsResult
			};
		}

		private bool IsValidUser(string firstName, string surName, string phone,
			string email, DateTime dateOfBirth)
			=>
				string.IsNullOrEmpty(firstName) == false
				&& string.IsNullOrEmpty(surName) == false
				&& string.IsNullOrEmpty(phone) == false
				&& email.Contains("@")
				&& email.Contains(".")
				&& dateOfBirth.AddYears(18) < DateTime.Now;
	}
}
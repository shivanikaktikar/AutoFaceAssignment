
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace AutoFacExample1
{
	public interface IMobileService
    {
		void Execute();
    }

	public class SMSService : IMobileService
    {
		public void Execute()
        {
			Console.WriteLine("SMS service is executing.");
        }
    }

	public interface IMailService
    {
		void Execute();
    }

	public class EmailService : IMailService
    {
		public void Execute()
        {
			Console.WriteLine("Email service is executing.");
        }
    }

	public class NotificationSender
    {
		public IMobileService _mobileService = null;
		public IMailService _mailService = null;

		//Injection through constructor
		public NotificationSender(IMobileService tmpservice)
        {
			_mobileService = tmpservice;
        }

		//Injection through property
		public IMailService SetMailService
        {
			set { _mailService = value; }
        }

		public void SendNotification()
        {
			_mobileService.Execute();
			_mailService.Execute();
        }
    }
		class Program
	{
		static void Main(string[] args)
		{
			var builder = new ContainerBuilder();       //Created the builder with which components/services are registerd.

			//Register types that expose interfaces.
			builder.RegisterType<SMSService>().As<IMobileService>();		
			builder.RegisterType<EmailService>().As<IMailService>();

			var container = builder.Build();		//Created container using Build method

			//Will resolve the component
			container.Resolve<IMobileService>().Execute();
			container.Resolve<IMailService>().Execute();
			Console.ReadLine();
		}
	}
}

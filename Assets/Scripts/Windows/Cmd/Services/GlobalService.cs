using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Cmd.Commands;

namespace Windows.Cmd.Services
{
	public class GlobalService : ConsoleService
	{
		public override string Name => null;

		public ConsoleService CurrentService { get; private set; }

		private readonly Dictionary<string, ConsoleService> _services;
		public IReadOnlyDictionary<string, ConsoleService> Services => _services;

		public GlobalService(Console console) : base(console)
		{
			_services = GetServices();
		}

		public bool TryRunService(string serviceName)
		{
			serviceName = serviceName.ToLower();
			if (!_services.ContainsKey(serviceName))
				return false;
			CurrentService = _services[serviceName];
			return true;
		}

		public void StopService() =>
			CurrentService = null;

		public override IConsoleCommand GetCommandByName(string name) =>
			base.GetCommandByName(name) ?? CurrentService?.GetCommandByName(name);

		private Dictionary<string, ConsoleService> GetServices()
		{
			var baseServiceType = typeof(ConsoleService);
			return baseServiceType.Assembly.ExportedTypes
				.Where(baseServiceType.IsAssignableFrom)
				.Where(type => type != baseServiceType && type != GetType())
				.Select(t => (ConsoleService) Activator.CreateInstance(t, (object) Console))
				.ToDictionary(newService => newService.Name.ToLower(), newService => newService);
		}
	}
}
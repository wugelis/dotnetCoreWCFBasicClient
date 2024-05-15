<Query Kind="Program">
  <NuGetReference Version="4.8.1">System.ServiceModel.Duplex</NuGetReference>
  <NuGetReference Version="4.8.1">System.ServiceModel.Http</NuGetReference>
  <NuGetReference Version="4.8.1">System.ServiceModel.NetTcp</NuGetReference>
  <NuGetReference Version="4.8.1">System.ServiceModel.Security</NuGetReference>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>System.ServiceModel.Description</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <UseNoncollectibleLoadContext>true</UseNoncollectibleLoadContext>
</Query>

async Task Main()
{
	try
	{
		var clientType = typeof(MyService1); // 您的 WCF 用戶端類別
		var endpoint = new EndpointAddress("http://localhost:58835/MyService.svc");
		var binding = new BasicHttpBinding();

		// 建立 WCF 客戶端實例
		var clientInstance = new MyService1(binding, endpoint);
		//var clientInstance = Activator.CreateInstance(clientType, binding, endpoint);

		// 確保 clientInstance 正確創建
		if (clientInstance == null)
		{
			Console.WriteLine("Failed to create WCF client instance.");
			return;
		}

		// 嘗試呼叫 WCF 服務方法
		var getAllAccountsMethod = clientType.GetMethod("GetAllAccountsAsync");
		if (getAllAccountsMethod != null)
		{
			var resultTask = (Task<Account[]>)getAllAccountsMethod.Invoke(clientInstance, null);
			var result = await resultTask;
			Console.WriteLine("Service call succeeded, result: " + result.Length);
		}
		else
		{
			Console.WriteLine("GetAllAccountsAsync method not found.");
		}
	}
	catch (TargetInvocationException tie)
	{
		Console.WriteLine("TargetInvocationException: " + tie.InnerException?.Message);
	}
	catch (Exception ex)
	{
		Console.WriteLine("Exception: " + ex.Message);
	}

	// 強制進行垃圾回收
	GC.Collect();
	GC.WaitForPendingFinalizers();

	Console.WriteLine("Program completed.");
}

// WCF 用戶端類別的定義
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
public partial class MyService1 : ClientBase<IMyService>, IMyService
{
	static partial void ConfigureEndpoint(ServiceEndpoint serviceEndpoint, ClientCredentials clientCredentials);

	public MyService1() { }
	public MyService1(string endpointConfigurationName) :
		base(endpointConfigurationName)
	{
		this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IMyService.ToString();
		ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
	}

	public MyService1(string endpointConfigurationName, string remoteAddress) :
		base(endpointConfigurationName, remoteAddress)
	{
		ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
	}

	public MyService1(string endpointConfigurationName, EndpointAddress remoteAddress) :
		base(endpointConfigurationName, remoteAddress)
	{
		ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
	}

	public MyService1(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) :
		base(binding, remoteAddress)
	{
		ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
	}

	public MyService1(EndpointConfiguration endpointConfiguration) :
		base(GetBindingForEndpoint(endpointConfiguration), GetEndpointAddress(endpointConfiguration))
	{
		this.Endpoint.Name = endpointConfiguration.ToString();
		ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
	}

	public async Task<Account[]> GetAllAccountsAsync()
	{
		return await base.Channel.GetAllAccountsAsync();
	}

	private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
	{
		if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMyService))
		{
			var result = new BasicHttpBinding();
			result.MaxBufferSize = int.MaxValue;
			result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
			result.MaxReceivedMessageSize = int.MaxValue;
			result.AllowCookies = true;
			return result;
		}
		throw new InvalidOperationException($"找不到名為 '{endpointConfiguration}' 的端點。");
	}

	private static EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
	{
		if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMyService))
		{
			return new EndpointAddress("http://localhost:58835/MyService.svc");
		}
		throw new InvalidOperationException($"找不到名為 '{endpointConfiguration}' 的端點。");
	}

	public enum EndpointConfiguration
	{
		BasicHttpBinding_IMyService,
	}
}

[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "MyServiceReference.IMyService")]
public interface IMyService
{
	[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyService/GetAllAccounts", ReplyAction = "http://tempuri.org/IMyService/GetAllAccountsResponse")]
	System.Threading.Tasks.Task<Account[]> GetAllAccountsAsync();
}
[DataContractAttribute]
public class Account
{
	[DataMember]
	public string Name { get; set; }
	[DataMember]
	public int Id { get; set; }
}

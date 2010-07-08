namespace Castle.Facilities.NHibernateIntegration.Fluent.Impl
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	using Castle.Core.Configuration;
	using Castle.Facilities.NHibernateIntegration.Builders;
	using Castle.Facilities.NHibernateIntegration.Fluent.Interfaces;

	///<summary>
	///</summary>
	public class BasicConfigurationBuilderConfiguration:IConfigurationBuilderConfiguration<DefaultConfigurationBuilder>
	{
		private string connectionConnectionString;
		private Type connectionDriver;
		private Type connectionProvider;
		private Type proxyFactoryClass;
		private Type dialect;
		private List<Assembly> assemblies;


		///<summary>
		///</summary>
		///<param name="conn"></param>
		///<returns></returns>
		public BasicConfigurationBuilderConfiguration ConnectionString(string conn)
		{
			this.connectionConnectionString = conn;
			return this;
		}


		///<summary>
		///</summary>
		///<param name="connectionDriver"></param>
		///<returns></returns>
		public BasicConfigurationBuilderConfiguration ConnectionDriver(Type connectionDriver)
		{
			this.connectionDriver = connectionDriver;
			return this;
		}

		///<summary>
		///</summary>
		///<param name="connectionProvider"></param>
		///<returns></returns>
		public BasicConfigurationBuilderConfiguration ConnectionProvider(Type connectionProvider)
		{
			this.connectionProvider = connectionProvider;
			return this;
		}

		///<summary>
		///</summary>
		///<param name="proxyFactory"></param>
		///<returns></returns>
		public BasicConfigurationBuilderConfiguration ProxyFactory(Type proxyFactory)
		{
			this.proxyFactoryClass = proxyFactory;
			return this;
		}

		///<summary>
		///</summary>
		///<param name="dialect"></param>
		///<returns></returns>
		public BasicConfigurationBuilderConfiguration Dialect(Type dialect)
		{
			this.dialect = dialect;
			return this;
		}

		///<summary>
		///</summary>
		///<param name="assemblies"></param>
		///<returns></returns>
		public BasicConfigurationBuilderConfiguration Assemblies(params Assembly[] assemblies)
		{
			this.assemblies.AddRange(assemblies);
			return this;
		}
		#region IConfigurationBuilderConfiguration<DefaultConfigurationBuilder> Members

		///<summary>
		///</summary>
		public BasicConfigurationBuilderConfiguration()
		{
			this.assemblies = new List<Assembly>(4);
			this.connectionProvider = typeof(NHibernate.Connection.DriverConnectionProvider);
			this.connectionDriver = typeof(NHibernate.Driver.SqlClientDriver);
			this.dialect = typeof(NHibernate.Dialect.MsSql2008Dialect);
		}
		/// <summary>
		/// Builds the factory configuration for the facility
		/// </summary>
		/// <returns></returns>
		public IConfiguration Build(IConfiguration configuration)
		{
			var settingsNode=new MutableConfiguration("settings");
			settingsNode.CreateChild("item").Attribute("key", "connection.provider").Value=connectionProvider.FullName;
			settingsNode.CreateChild("item").Attribute("key", "connection.driver_class").Value=connectionDriver.FullName;
			settingsNode.CreateChild("item").Attribute("key", "connection.connection_string").Value = connectionConnectionString;

			settingsNode.CreateChild("item").Attribute("key", "dialect").Value = dialect.FullName;
			settingsNode.CreateChild("item").Attribute("key", "proxyfactory.factory_class").Value = proxyFactoryClass.AssemblyQualifiedName;
			var assembliesNode = new MutableConfiguration("assemblies");
			foreach (var asm in assemblies)
			{
				assembliesNode.CreateChild("assembly").Value = asm.FullName;
			}
			configuration.Children.Add(settingsNode);
			configuration.Children.Add(assembliesNode);
			return configuration;
		}

		#endregion
	}
}

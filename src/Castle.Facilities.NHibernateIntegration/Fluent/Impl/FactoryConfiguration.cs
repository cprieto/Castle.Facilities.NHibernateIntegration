namespace Castle.Facilities.NHibernateIntegration.Fluent.Impl
{
	using System;

	using Castle.Core.Configuration;
	using Castle.Facilities.NHibernateIntegration.Fluent.Interfaces;

	/// <summary>
	/// Fluent interface for factory configurations
	/// </summary>
	public class FactoryConfiguration: IFactoryConfiguration
	{
		private string alias;
		private string id;
		private string configurationPersisterKey;
		private string configurationBuilderKey;
		private IConfigurationBuilderConfiguration configurationBuilderConfiguration;

		#region IFactoryConfiguration Members
		/// <summary>
		/// Sets the alias for the factory
		/// </summary>
		/// <param name="alias">The alias for the factory</param>
		/// <returns></returns>
		public IFactoryConfiguration Alias(string alias)
		{
			this.alias = alias;
			return this;
		}

		/// <summary>
		/// Sets the id for the factory
		/// </summary>
		/// <param name="id">The id of the factory</param>
		/// <returns></returns>
		public IFactoryConfiguration Id(string id)
		{
			this.id = id;
			return this;
		}

		/// <summary>
		/// Component with <paramref name="key"/> is used to persist the configuration
		/// </summary>
		/// <param name="key">The key for the persister component</param>
		/// <returns></returns>
		public IFactoryConfiguration UsingConfigurationPersister(string key)
		{
			this.configurationPersisterKey = key;
			return this;
		}

		/// <summary>
		/// Configuration builder for the factory
		/// </summary>
		/// <returns></returns>
		public IFactoryConfiguration UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(string key, Action<TConfigurationBuilderConfiguration> configurationBuilderConfiguration) where TConfigurationBuilderConfiguration : IConfigurationBuilderConfiguration<TConfigurationBuilder>, new() where TConfigurationBuilder : IConfigurationBuilder
		{
			this.configurationBuilderKey = key;
			this.configurationBuilderConfiguration = new TConfigurationBuilderConfiguration();
			configurationBuilderConfiguration((TConfigurationBuilderConfiguration)this.configurationBuilderConfiguration);
			return this;
		}


		IFactoryConfiguration IFactoryConfiguration.UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(string key, Action<TConfigurationBuilderConfiguration> configurationBuilderConfiguration)
		{
			return UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(key, configurationBuilderConfiguration);
		}

		IFactoryConfiguration IFactoryConfiguration.UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(Action<TConfigurationBuilderConfiguration> configurationBuilderConfiguration)
		{
			return UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(configurationBuilderConfiguration);
		}



		/// <summary>
		/// Configuration builder for the factory
		/// </summary>
		/// <returns></returns>
		public IFactoryConfiguration UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(Action<TConfigurationBuilderConfiguration> configurationBuilderConfiguration) 
			where TConfigurationBuilderConfiguration : IConfigurationBuilderConfiguration<TConfigurationBuilder>, new()
			where TConfigurationBuilder : IConfigurationBuilder
		{
			this.configurationBuilderConfiguration = new TConfigurationBuilderConfiguration();
			configurationBuilderConfiguration((TConfigurationBuilderConfiguration)this.configurationBuilderConfiguration);
			return this;
		}


		#endregion

		#region IFactoryConfiguration Members

		/// <summary>
		/// Builds the factory configuration for the facility
		/// </summary>
		/// <returns></returns>
		public IConfiguration Build()
		{
			var factoryConfiguration = new MutableConfiguration("factory");
			factoryConfiguration.Attribute("id", this.id);
			factoryConfiguration.Attribute("alias", this.alias);
			factoryConfiguration.Attribute(NHibernateFacility.ConfigurationBuilderKeyConfigurationKey, configurationBuilderKey);
			this.configurationBuilderConfiguration.Build(factoryConfiguration);
			return factoryConfiguration;
		}

		#endregion
	}
}

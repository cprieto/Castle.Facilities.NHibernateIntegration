namespace Castle.Facilities.NHibernateIntegration.Fluent.Interfaces
{
	using System;

	using Castle.Core.Configuration;

	using NHibernate;

	/// <summary>
	/// Interface for the individual factory configuration
	/// </summary>
	public interface IFactoryConfiguration
	{
		/// <summary>
		/// Sets the alias for the <see cref="ISessionFactory"/>
		/// </summary>
		/// <param name="alias">Alias for the factory</param>
		/// <returns></returns>
		IFactoryConfiguration Alias(string alias);

		/// <summary>
		/// Sets the id for the <see cref="ISessionFactory"/>
		/// </summary>
		/// <param name="id">The id of the factory</param>
		/// <returns></returns>
		IFactoryConfiguration Id(string id);

		/// <summary>
		/// Persister specific configuration for the factory.
		/// </summary>
		/// <returns></returns>
		IFactoryConfiguration UsingConfigurationPersister(string key);

		/// <summary>
		/// Configuration builder for the factory
		/// </summary>
		/// <returns></returns>
		IFactoryConfiguration UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(string key,
																												   Action<TConfigurationBuilderConfiguration>
		                                                                                                           	configurationBuilderConfiguration)
			where TConfigurationBuilderConfiguration : IConfigurationBuilderConfiguration<TConfigurationBuilder>, new()
			where TConfigurationBuilder : IConfigurationBuilder;

		/// <summary>
		/// Configuration builder for the factory
		/// </summary>
		/// <returns></returns>
		IFactoryConfiguration UsingConfigurationBuilder<TConfigurationBuilderConfiguration, TConfigurationBuilder>(
			Action<TConfigurationBuilderConfiguration> configurationBuilderConfiguration)
			where TConfigurationBuilderConfiguration : IConfigurationBuilderConfiguration<TConfigurationBuilder>, new()
			where TConfigurationBuilder : IConfigurationBuilder;

		/// <summary>
		/// Builds the factory configuration for the facility
		/// </summary>
		/// <returns></returns>
		IConfiguration Build();
	}
}

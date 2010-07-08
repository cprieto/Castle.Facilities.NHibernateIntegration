namespace Castle.Facilities.NHibernateIntegration.Fluent.Interfaces
{
	using System;

	using Castle.Core.Configuration;

	/// <summary>
	/// Interface for <see cref="NHibernateFacility"/> configuration
	/// </summary>
	public interface INHFacilityConfiguration
	{
		/// <summary>
		/// Sets the key for the facility
		/// </summary>
		/// <param name="id">Key to be used for the facility</param>
		/// <returns></returns>
		INHFacilityConfiguration Id(string id);

		/// <summary>
		/// Sets the default <see cref="IConfigurationPersister"/> for the facility
		/// </summary>
		/// <returns></returns>
		INHFacilityConfiguration DefaultConfigurationPersister(string key);


		/// <summary>
		/// Adds new factory to the facility.
		/// </summary>
		/// <param name="customConfiguration"></param>
		/// <returns></returns>
		INHFacilityConfiguration AddFactory(Action<IFactoryConfiguration> customConfiguration);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		IConfiguration Build();
	}
}

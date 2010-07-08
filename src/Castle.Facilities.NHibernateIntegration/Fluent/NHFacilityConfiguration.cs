namespace Castle.Facilities.NHibernateIntegration.Fluent
{
	using System;
	using System.Collections.Generic;

	using Castle.Core.Configuration;
	using Castle.Facilities.NHibernateIntegration.Fluent.Impl;
	using Castle.Facilities.NHibernateIntegration.Fluent.Interfaces;

	///<summary>
	///</summary>
	public class NHFacilityConfiguration:INHFacilityConfiguration
	{
		private string id;
		private string defaultConfigurationPersister;
		private readonly List<IFactoryConfiguration> factoryConfigurations;

		///<summary>
		///</summary>
		public NHFacilityConfiguration()
		{
			this.factoryConfigurations=new List<IFactoryConfiguration>();
		}

		/// <summary>
		/// Sets the key for the facility
		/// </summary>
		/// <param name="id">Key to be used for the facility</param>
		/// <returns></returns>
		public INHFacilityConfiguration Id(string id)
		{
			this.id = id;
			return this;
		}

		/// <summary>
		/// Sets the default <see cref="IConfigurationPersister"/> for the facility
		/// </summary>
		/// <returns></returns>
		public INHFacilityConfiguration DefaultConfigurationPersister(string key)
		{
			this.defaultConfigurationPersister = key;
			return this;
		}

		/// <summary>
		/// Adds new factory to the facility.
		/// </summary>
		/// <param name="customConfiguration"></param>
		/// <returns></returns>
		public INHFacilityConfiguration AddFactory(Action<IFactoryConfiguration> customConfiguration)
		{
			var factoryConfig=new FactoryConfiguration();
			customConfiguration(factoryConfig);
			this.factoryConfigurations.Add(factoryConfig);
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IConfiguration Build()
		{
			var facilityConfiguraiton = new MutableConfiguration("facility");
			facilityConfiguraiton.Attribute("id", this.id);
			facilityConfiguraiton.Attribute("type", typeof(NHibernateFacility).FullName);
			foreach (var factoryConfiguration in factoryConfigurations)
			{
				var config=factoryConfiguration.Build();
				facilityConfiguraiton.Children.Add(config);
			}

			return facilityConfiguraiton;
		}
	}
}

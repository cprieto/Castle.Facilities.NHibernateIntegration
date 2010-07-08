namespace Castle.Facilities.NHibernateIntegration.Fluent
{
	using System;

	using Castle.Facilities.NHibernateIntegration.Fluent.Interfaces;
	using Castle.MicroKernel;
	using Castle.MicroKernel.Registration;

	///<summary>
	/// Fluent Registration unit for NHibernate Facility
	///</summary>
	public class NHFacilityRegistration : IRegistration
	{
		private readonly INHFacilityConfiguration configuration;

		/// <summary>
		/// Initializes the registration unit
		/// </summary>
		/// <param name="configuration">The facility configuration</param>
		public NHFacilityRegistration(Action<INHFacilityConfiguration> configuration)
		{
			var nhFacilityConfiguration = new NHFacilityConfiguration();
			configuration(nhFacilityConfiguration);
			this.configuration = nhFacilityConfiguration;
		}

		/// <summary>
		/// Registers the facility to the container
		/// </summary>
		/// <param name="kernel"></param>
		public void Register(IKernel kernel)
		{
			var facilityConfiguration = configuration.Build();
			string facilityId = facilityConfiguration.Attributes["id"];
			kernel.ConfigurationStore.AddFacilityConfiguration(facilityId, facilityConfiguration);
			kernel.AddFacility<NHibernateFacility>(facilityId);
		}
	}
}

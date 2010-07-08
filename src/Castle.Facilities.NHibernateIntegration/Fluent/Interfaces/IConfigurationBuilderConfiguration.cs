namespace Castle.Facilities.NHibernateIntegration.Fluent.Interfaces
{
	using Castle.Core.Configuration;

	///<summary>
	///</summary>
	public interface IConfigurationBuilderConfiguration
	{

		/// <summary>
		/// Builds the factory configuration for the facility
		/// </summary>
		/// <returns></returns>
		IConfiguration Build(IConfiguration configuration);
	}

	///<summary>
	///</summary>
	public interface IConfigurationBuilderConfiguration<TConfigurationBuilder> : IConfigurationBuilderConfiguration
	{

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castle.Facilities.NHibernateIntegration.Tests.Fluent
{
	using Castle.Facilities.NHibernateIntegration.Builders;
	using Castle.Facilities.NHibernateIntegration.Fluent;
	using Castle.Facilities.NHibernateIntegration.Fluent.Impl;
	using Castle.Facilities.NHibernateIntegration.Fluent.Interfaces;
	using Castle.Windsor;

	using NHibernate.Dialect;

	using NUnit.Framework;
	[TestFixture]
	public class FluentTests
	{
		[Test]
		public void Does_work()
		{
			var container = new WindsorContainer();
			var nhfacilityRegistration = new NHFacilityRegistration(conf =>
			                                                        conf
			                                                        	.Id("FacilityId")
			                                                        	.DefaultConfigurationPersister("mykey")
			                                                        	.AddFactory(x => x
			                                                        	                 	.Alias("myAlias")
			                                                        	                 	.Id("myId")
			                                                        	                 	.UsingConfigurationPersister("persister")
			                                                        	                 	.UsingConfigurationBuilder<BasicConfigurationBuilderConfiguration,DefaultConfigurationBuilder>
			                                                        	                 	(
			                                                        	                 		y =>
			                                                        	                 		y.ConnectionDriver(
			                                                        	                 			typeof(NHibernate.Driver.SqlClientDriver
			                                                        	                 				))
			                                                        	                 			.ConnectionString("localhost")
			                                                        	                 			.Dialect(typeof(MsSql2000Dialect))
																									.ProxyFactory(typeof(NHibernate.ByteCode.Castle.ProxyFactoryFactory))
																									.Assemblies(this.GetType().Assembly)

			                                                        	                 	)));
			container.Register(nhfacilityRegistration);
		}
	}
}

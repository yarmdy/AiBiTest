
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using AiBi.Test.Dal.Model;
using Autofac;
using Autofac.Integration.Mvc;
using IContainer = Autofac.IContainer;

namespace AiBi.Test.Bll
{
    public static  class AutofacExt
    {
        private static IContainer _container;

        public static void InitAutofac()
        {
            var builder = new ContainerBuilder();

            //注册Entity Framework数据库上下文
            builder.RegisterType(typeof(TestContext)).As(typeof(TestContext)).PropertiesAutowired().InstancePerRequest();

            

            //注册app层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerRequest();

            // 注册controller，使用属性注入
            builder.RegisterControllers(Assembly.GetCallingAssembly()).PropertiesAutowired().InstancePerRequest();


            builder.RegisterModelBinders(Assembly.GetCallingAssembly()).InstancePerRequest();
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // 注册所有的Attribute
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            _container = builder.Build();

            //Set the MVC DependencyResolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));

        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
            //   return (T)DependencyResolver.Current.GetService(typeof(T));
        }
        public static T GetService<T>()
        {
            return (T)DependencyResolver.Current.GetService(typeof(T));
        }
    }
}
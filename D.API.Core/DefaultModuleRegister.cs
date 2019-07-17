using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Mvc;

namespace D.API.Core
{
    public class DefaultModuleRegister: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册当前程序集中以"Service"结尾的类，暴露其实现的接口，生命周期为PerLifetimeScope
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(s=>s.Name.EndsWith("Service")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(s => s.Name.EndsWith("Controller")).PropertiesAutowired();
            builder.RegisterAssemblyTypes(GetAssembly("D.Service.Core")).Where(s => s.Name.EndsWith("Service")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            //builder.RegisterTypes(Assembly.GetExecutingAssembly().GetExportedTypes().Where(s => typeof(ControllerBase).IsAssignableFrom(s)).ToArray()).PropertiesAutowired();

            //base.Load(builder);
        }

        /// <summary>
        /// 根据程序集名称加载程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private Assembly GetAssembly(string assemblyName)
        {
            var assemblyPath = AppContext.BaseDirectory + $"{assemblyName}.dll";
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            return assembly;
        }
    }
}

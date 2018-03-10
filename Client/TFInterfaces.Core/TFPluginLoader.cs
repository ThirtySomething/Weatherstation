using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace net.derpaul.tf
{
    /// <summary>
    /// Generic plugin loader for plugins of given type
    /// </summary>
    /// <typeparam name="PluginType"></typeparam>
    public static class TFPluginLoader<PluginType>
    {
        /// <summary>
        /// Load plugins from given path
        /// </summary>
        /// <param name="pluginPath">Path to plugins</param>
        /// <returns></returns>
        public static ICollection<PluginType> TFPluginsLoad(string pluginPath)
        {
            ICollection<Assembly> assemblyList = GetPluginAssemblyList(pluginPath);
            ICollection<Type> pluginTypeList = GetPluginTypeList(assemblyList);
            ICollection<PluginType> pluginInstanceList = GetPluginInstanceList(pluginTypeList);

            return pluginInstanceList;
        }

        /// <summary>
        /// Read assemblies from given path
        /// </summary>
        /// <param name="pluginFolder">Path to search for assemblies</param>
        /// <returns>Collection of assembly objects</returns>
        private static ICollection<Assembly> GetPluginAssemblyList(string pluginFolder)
        {
            if (!Directory.Exists(pluginFolder))
            {
                return null;
            }

            string[] pluginFileList = Directory.GetFiles(pluginFolder, "*.dll");
            ICollection<Assembly> assemblyList = new List<Assembly>();
            foreach (string pluginFile in pluginFileList)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(pluginFile);
                assemblyList.Add(assembly);
            }

            return assemblyList;
        }

        /// <summary>
        /// Check assemblies for required type
        /// </summary>
        /// <param name="assemblyList">Collection of assemblies</param>
        /// <returns>Collection of plugins of type T</returns>
        private static ICollection<Type> GetPluginTypeList(ICollection<Assembly> assemblyList)
        {
            if (assemblyList == null)
            {
                return null;
            }
            Type pluginType = typeof(PluginType);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblyList)
            {
                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(pluginType.FullName) != null)
                            {
                                pluginTypes.Add(type);
                            }
                        }
                    }
                }
            }
            return pluginTypes;
        }

        /// <summary>
        /// Instantiate objects from given plugin list
        /// </summary>
        /// <param name="pluginTypeList">Collection of plugins of type T</param>
        /// <returns>Collection of plugin objects</returns>
        private static ICollection<PluginType> GetPluginInstanceList(ICollection<Type> pluginTypeList)
        {
            if (pluginTypeList == null)
            {
                return null;
            }

            ICollection<PluginType> pluginInstanceList = new List<PluginType>();
            foreach (Type pluginType in pluginTypeList)
            {
                PluginType pluginInstance = (PluginType)Activator.CreateInstance(pluginType);
                pluginInstanceList.Add(pluginInstance);
            }
            return pluginInstanceList;
        }
    }
}
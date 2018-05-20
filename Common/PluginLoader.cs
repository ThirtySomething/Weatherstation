using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace net.derpaul.tf
{
    /// <summary>
    /// Generic plugin loader for plugins of given type
    /// </summary>
    /// <typeparam name="PluginType"></typeparam>
    public static class PluginLoader<PluginType>
    {
        /// <summary>
        /// Load plugins from given path
        /// </summary>
        /// <param name="pluginPath">Path to plugins</param>
        /// <param name="productName">Product name of plugins</param>
        /// <returns></returns>
        public static List<PluginType> TFPluginsLoad(string pluginPath, string productName)
        {
            List<Assembly> assemblyList = GetPluginAssemblyList(pluginPath, productName);
            List<Type> pluginTypeList = GetPluginTypeList(assemblyList);
            List<PluginType> pluginInstanceList = GetPluginInstanceList(pluginTypeList);

            return pluginInstanceList;
        }

        /// <summary>
        /// Read assemblies from given path
        /// </summary>
        /// <param name="pluginFolder">Path to search for assemblies</param>
        /// <param name="productName">Product name of plugins</param>
        /// <returns>Collection of assembly objects</returns>
        private static List<Assembly> GetPluginAssemblyList(string pluginFolder, string productName)
        {
            List<Assembly> assemblyList = new List<Assembly>();

            if (Directory.Exists(pluginFolder))
            {
                string[] pluginFileList = Directory.GetFiles(pluginFolder, "*.dll");
                foreach (string pluginFile in pluginFileList)
                {
                    var name = FileVersionInfo.GetVersionInfo(pluginFile).ProductName;
                    if (name != null && name.Contains(productName))
                    {
                        Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(pluginFile);
                        assemblyList.Add(assembly);
                    }
                }
            }

            return assemblyList;
        }

        /// <summary>
        /// Check assemblies for required type
        /// </summary>
        /// <param name="assemblyList">Collection of assemblies</param>
        /// <returns>Collection of plugins of type T</returns>
        private static List<Type> GetPluginTypeList(List<Assembly> assemblyList)
        {
            List<Type> pluginTypes = new List<Type>();

            if (assemblyList != null)
            {
                Type pluginType = typeof(PluginType);
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
        private static List<PluginType> GetPluginInstanceList(List<Type> pluginTypeList)
        {
            List<PluginType> pluginInstanceList = new List<PluginType>();

            if (pluginInstanceList != null)
            {
                foreach (Type pluginType in pluginTypeList)
                {
                    PluginType pluginInstance = (PluginType)Activator.CreateInstance(pluginType);
                    pluginInstanceList.Add(pluginInstance);
                }
            }
            return pluginInstanceList;
        }
    }
}